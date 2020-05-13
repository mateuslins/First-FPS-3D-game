using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;

    private UIManager _uiManager;

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

                if (Input.GetKeyDown(KeyCode.E))
                {
                    player.CollectCoin();
                    AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
                    _uiManager.ShowInteractText(false);
                    _uiManager.ShowCoinImage();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _uiManager.ShowInteractText(false);
        }
    }
}
