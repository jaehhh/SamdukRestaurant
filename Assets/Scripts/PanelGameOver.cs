using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textStage;

    [SerializeField] private Image[] star;
    [SerializeField] private Sprite clearStarSprite;

    public void Set(int currentCoin, int needCoin, int star, int stage)
    {
        textCoin.text = $"{currentCoin}/{needCoin}";
        textStage.text =$"STAGE 1-{stage+1}";

        for(int i = 0; i < star; i ++)
        {
            this.star[i].sprite = clearStarSprite;
        }
    }
}
