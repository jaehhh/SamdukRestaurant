using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OrderUI : MonoBehaviour
{
    [SerializeField] private Image mainFood;
    [SerializeField] private Image[] matFood;
    [SerializeField] private TextMeshProUGUI timeText;
    public int orderNumber;

    private int time;
    private float currentTime = 0;


    public void Setup(Sprite main,Sprite[] mat,int waitTime,int orderNum)
    {
        mainFood.sprite = main;

        for (int i = 0; i < matFood.Length; i++)
        {
            if (mat[i] != null)
            {
                matFood[i].sprite = mat[i];
            }
        }
           

        time = waitTime;

        orderNumber = orderNum;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 1)
        {
            currentTime --;
            time--;
        }

        timeText.text = $"{time}Sec";
    }
}
