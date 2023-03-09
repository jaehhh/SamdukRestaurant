using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeInput : MonoBehaviour
{
    [SerializeField] private User user;
    [SerializeField] private ButtonController buttonController;

    private bool isActive = false;

    private void Update()
    {
        if(isActiveAndEnabled == true && isActive == false)
        {
            isActive = true;

            user.CanControll = false;
            user.CurrentPanel = this.gameObject;

            buttonController.extendedEvent.AddListener(LayOn);
        }
    }

    private void LayOn()
    {
        user.CanControll = true;
        isActive = false;
    }
}
