using UnityEngine;
using TMPro;

public class Stage : MonoBehaviour
{
    private UserData userData;
    private MenuUIController menuUIController;

    public int Level;
    [HideInInspector] public bool CanSelect = false;

    [HideInInspector] public int ClearStar;

    [SerializeField] private Sprite clearStageImage;

    [SerializeField] private GameObject textPrefab;
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        userData = GameObject.FindGameObjectWithTag("DD").GetComponent<UserData>();
        menuUIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<MenuUIController>();
    }

    private void Start()
    {
        GameObject clone = Instantiate(textPrefab);
        clone.transform.SetParent(canvas.transform);
        clone.transform.position = transform.position + Vector3.down;
        clone.GetComponent<TextMeshProUGUI>().text = $"1 - {Level +1}"; // 임시 text "1 - value"

        userData.GetInfo(Level, out CanSelect, out ClearStar); // 유저의 정보를 읽고 클리어 여부와 별 개수 가져옴

        if (CanSelect)
        {
            if(Level != userData.clearStage)
            GetComponent<SpriteRenderer>().sprite = clearStageImage;

            menuUIController.CreateUI(Level, ClearStar, this.transform);
        }
    }
}
