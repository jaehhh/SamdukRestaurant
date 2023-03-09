using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private User user;

    public void ButtonStore()
    {
        user.StopCoroutine("TemporaryPopup");
        user.PanelCanSelectText.text = "응 그런 거 없어~";
        user.StartCoroutine("TemporaryPopup", user.PanelCanSelect);
    }
}
