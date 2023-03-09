using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;

    private void Start()
    {
        textCoin.text = GameObject.FindGameObjectWithTag("DD").GetComponent<UserData>().coin.ToString();
    }
}
