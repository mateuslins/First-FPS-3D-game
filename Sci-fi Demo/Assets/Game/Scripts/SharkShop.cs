using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;

    private UIManager _uiManager;

    private bool isMoneyText = false;

    private void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                _uiManager.ShowInteractText(true);

                if (Input.GetKeyDown(KeyCode.E) && isMoneyText == false)
                {
                    bool successBuy = player.BuyWeapon();
                    if (successBuy == true)
                    {
                        _uiManager.ShowCoinImage(false);
                        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1.5f);
                    }
                    else
                    {
                        _uiManager.ShowMoneyText(true);
                        isMoneyText = true;
                        StartCoroutine(MoneyTextRoutine());
                    }
                }
            }
        }
    }

    IEnumerator MoneyTextRoutine()
    {
        yield return new WaitForSeconds(2f);
        _uiManager.ShowMoneyText(false);
        isMoneyText = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.ShowInteractText(false);
        }
    }
}
