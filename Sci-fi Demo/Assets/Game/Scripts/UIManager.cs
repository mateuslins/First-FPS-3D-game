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
    private GameObject _coinImage;

    public void UpdateAmmo(int count, int max)
    {
        _ammoText.text = count + " / " + max;
    }

    public void ShowInteractText(bool state)
    {
        _interact.SetActive(state);
    }

    public void ShowCoinImage()
    {
        _coinImage.SetActive(true);
    }
}
