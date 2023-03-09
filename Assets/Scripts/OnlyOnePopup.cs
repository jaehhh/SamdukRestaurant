using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOnePopup : MonoBehaviour
{
    [SerializeField] private GameObject panelMaker;
    [SerializeField] private GameObject panelDefault;
    [SerializeField] private GameObject panelReset;

    private GameObject nowPanel;
    private GameObject beforePanel;

    public void MakerPanelOnButton()
    {
        panelMaker.SetActive(true);
        panelDefault.SetActive(false);

        nowPanel = panelMaker;
        beforePanel = panelDefault;

        GetComponent<AudioSource>().Play();
    }

    public void ResetPanelOnButton()
    {
        panelReset.SetActive(true);
        panelDefault.SetActive(false);

        nowPanel = panelReset;
        beforePanel = panelDefault;

        GetComponent<AudioSource>().Play();
    }

    public void ClosePanel()
    {
        nowPanel.SetActive(false);
        beforePanel.SetActive(true);

        GetComponent<AudioSource>().Play();
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#else
      Application.Quit();

#endif
    }
}
