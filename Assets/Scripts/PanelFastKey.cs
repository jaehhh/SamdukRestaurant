using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelFastKey : MonoBehaviour
{
    [SerializeField] private Button[] button;
    // [0] : 긍정버튼 할당키 {space, enter}
    // [1] : 부정버튼 할당키 {escape}

    public void FastKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Space:
                {
                    button[0].onClick.Invoke();
                    break;
                }
            case KeyCode.Return:
                {
                    button[0].onClick.Invoke();
                    break;
                }
            case KeyCode.Escape:
                {
                    button[1].onClick.Invoke();
                    break;
                }

        }
    }
}
