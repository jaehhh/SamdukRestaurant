using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 조작을 담당하는 스크립트

public class User : MonoBehaviour
{
    private GameObject DDObject;

    [SerializeField] private GameObject panelStart;
    [SerializeField] private GameObject panelCanSelect;
    public GameObject PanelCanSelect { get => panelCanSelect; }
    [SerializeField] private TextMeshProUGUI panelCanSelectText;
    public TextMeshProUGUI PanelCanSelectText { get => panelCanSelectText; }
    private GameObject currentPanel; // 켜져있는 패널
    public GameObject CurrentPanel { set => currentPanel = value; }

    [SerializeField] private ButtonController buttonController;
    [SerializeField] private MenuPlayer player;

    Vector3 mousePosition; // 마우스위치
    RaycastHit2D hit; // 선택한 물체

    [SerializeField] private KeyCode[] keyMove;
    private KeyCode[] enter = { KeyCode.Space, KeyCode.Return };
    private KeyCode esc = KeyCode.Escape;

    private bool canControll = true;
    public bool CanControll { set => canControll = value; }

    private void Awake()
    {
        DDObject = GameObject.FindGameObjectWithTag("DD");
    }

    private void Update()
    {
        if (canControll == false) // UI 팝업 상태
        {
            if (Input.GetKeyDown(esc))
            {
                currentPanel.GetComponent<PanelFastKey>().FastKey(esc);
            }
            else
            {
                for (int i = 0; i < enter.Length; i++)
                {
                    if (Input.GetKeyDown(enter[i]))
                        currentPanel.GetComponent<PanelFastKey>().FastKey(enter[i]);
                }
            }
        }

        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                MouseControll();
            }

            for (int i = 0; i < keyMove.Length; i++)
            {
                if (Input.GetKeyDown(keyMove[i]))
                {
                    KeyBoardControll(keyMove[i]);
                    break;
                }
            }
        }
    }

    private void MouseControll()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hit = Physics2D.Raycast(mousePosition, transform.forward, Mathf.Infinity); // 광선쏘고 닿은 물체 저장

        if (hit.collider != null)
        {
            Stage stage = hit.collider.GetComponent<Stage>();

            if (stage.CanSelect == false)
            {
                StopCoroutine("TemporaryPopup");
                panelCanSelectText.text = "선택할 수 없는 스테이지예요";
                StartCoroutine("TemporaryPopup", panelCanSelect);
            }
            else
            {
                DDObject.GetComponentInChildren<UserData>().SelectedStage = stage.Level;
                player.MoveWithMouse(stage); // 선택한 스테이지로 캐릭터 이동

                panelStart.SetActive(true); // 패널 등장
                currentPanel = panelStart;


                // 패널 바깥쪽 클릭 비활성화
                canControll = false;
                buttonController.extendedEvent.AddListener(LayOn);
            }

            buttonController.audioSource.Play();
        }
    }

    private void KeyBoardControll(KeyCode key)
    {
        if(key == KeyCode.LeftArrow || key == KeyCode.A) // 좌측이동
        {
            player.MoveWithKeyBoard(-1);
        }
        else if(key == KeyCode.RightArrow || key == KeyCode.D) // 우측이동
        {
            player.MoveWithKeyBoard(1);
        }
        else // 스테이지 선택
        {
            if(player.Stage[player.currentStage].GetComponent<Stage>().CanSelect)
            {
                DDObject.GetComponentInChildren<UserData>().SelectedStage = player.currentStage;
                panelStart.SetActive(true);
                currentPanel = panelStart;

                // 외부 클릭 비활성화
                canControll = false;
                buttonController.extendedEvent.AddListener(LayOn);
            }
            else
            {
                StopCoroutine("TemporaryPopup");
                panelCanSelectText.text = "선택할 수 없는 스테이지예요";
                StartCoroutine("TemporaryPopup", panelCanSelect);
            }

            buttonController.audioSource.Play();
        }
    }

    public IEnumerator TemporaryPopup(GameObject target)
    {
        panelCanSelect.SetActive(true);

        yield return new WaitForSeconds(1f);

        panelCanSelect.SetActive(false);
        // buttonController.extendedEvent.AddListener(LayOn);
    }

    private void LayOn()
    {
        this.canControll = true;
    }
}
