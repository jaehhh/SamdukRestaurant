using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    [HideInInspector] public int needCoin;
    private int currentCoin = 0;
    public int CurrentCoin { get => currentCoin;}

    private void Start()
    {
        SetText();

    }

    public void GetCoin(int price)
    {
        currentCoin += price;

        SetText();
    }

    private void SetText()
    {
        textCoin.text = $"{currentCoin}/{needCoin}";
    }
}
