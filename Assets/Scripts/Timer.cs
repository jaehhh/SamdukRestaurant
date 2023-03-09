using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private OrderController orderController;

    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private int time = 120;
    private int min, sec;
    private float currentTime;

    public void StartTimer()
    {
        orderController = GetComponent<OrderController>();

        StartCoroutine("StageTimer");
    }

    private void SetText()
    {
        min = time / 60;
        sec = time % 60;

        if (sec < 10)
            textTime.text = $"{min} : 0{sec}";
        else
            textTime.text = $"{min} : {sec}";
    }

    private IEnumerator StageTimer()
    {
        while (time > 0)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 1)
            {
                time--;

                currentTime--;

                SetText();
            }

            yield return null;
        }

        GetComponent<StageController>().StopGame();
        orderController.DestroyAllOreder();
    }
}
