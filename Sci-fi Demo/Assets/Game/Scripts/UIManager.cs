using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private GameObject _interact;
    [SerializeField]
    private GameObject _moneyText;
    [SerializeField]
    private GameObject _coinImage;

    public void UpdateAmmo(int count, int max)
    {
        _ammoText.text = count + " / " + max;
    }

    public void ShowInteractText(bool state)
    {
        _interact.SetActive(state);
    }

    public void ShowMoneyText(bool state)
    {
        _moneyText.SetActive(state);
    }

    public void ShowCoinImage(bool state)
    {
        _coinImage.SetActive(state);
    }
}
