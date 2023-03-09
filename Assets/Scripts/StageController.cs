using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageController : MonoBehaviour
{
    private GameObject[] stageFood;// 스테이지에 배치될 재료
    private Recipe recipe;

    // 레벨테이블에서 가져올 것
    private GameObject DD;
    private Level level;
    private int spawnTime; // 손님 스폰 주기
    private int[] needFood; // 손님이 원하게 되는 음식, 레시피 코드
    private FoodType[] useFood; // 스테이지에서 사용하게 될 재료, 프리팹등록된 재료와 FoodType 비교
    private int needCoin; // 목표 금액

    // 스테이지 시작시 레벨 적용 대상
    [SerializeField] private Cooker_Material[] cooker; // 씬에있는 재료박스
    private CoinController coinController;
    private SpawnNPC spawner;

    // 시작하고 가동시키는 것
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI startCountText;

    [SerializeField] private GameObject panelGameOver;
    public bool isGameOver = false;

    public int selectedLevel;

    private void Awake()
    {
        // 스테이지레벨 세팅
        DD = GameObject.FindGameObjectWithTag("DD");
        selectedLevel = DD.GetComponent<UserData>().SelectedStage;

        // 수치 세팅
        level = DD.GetComponentInChildren<Level>();
        spawnTime = level.levelTable[selectedLevel].spawnTime;
        needFood = level.levelTable[selectedLevel].needFood;
        useFood = level.levelTable[selectedLevel].useFood;
        needCoin = level.levelTable[selectedLevel].needCoin;

        // 스테이지에 놓을 재료 선택
        recipe = DD.GetComponentInChildren<Recipe>();
        CompareFood();
        for (int i = 0; i < useFood.Length; i++)
        {
            cooker[i].foodPrefab = stageFood[i];
        }

        // 코인
        coinController = GetComponent<CoinController>();
        coinController.needCoin = needCoin;

        // 스포너
        spawner = GetComponent<SpawnNPC>();
        spawner.spawnTime = spawnTime;
        spawner.needFood = needFood;

        StartCoroutine("StartCount");

        // 브금 교체
        DD.GetComponent<BGMController>().ChangeClip();
    }

    private IEnumerator StartCount()
    {
        int count = 3;
        float time = 0;

        startCountText.text = count.ToString();

        while (count > 0)
        {
            time += Time.deltaTime;

            if (time > 1)
            {
                time--;
                count--;

                startCountText.text = count.ToString();
            }

            yield return null;
        }

        startCountText.gameObject.SetActive(false);

        StartGame();
    }

    private void StartGame()
    {
        playerController.CanMove = true;
        spawner.StartSpawn();
        GetComponent<Timer>().StartTimer();
    }

    private void CompareFood()
    {
        stageFood = new GameObject[useFood.Length]; // 배열 길이

        for (int i = 0; i < useFood.Length; i++)
        {
            for (int j = 0; j < recipe.allMatFood.Length; j++)
            {
                // 레벨재료와 모든재료의 foodType을 비교한다
                if(useFood[i] == recipe.allMatFood[j].GetComponent<FoodState>().foodType)
                {
                    stageFood[i] = recipe.allMatFood[j];
                }
            } 
        }    
    }

    public void StopGame()
    {
        playerController.CanMove = false;
        spawner.StopSpawn();

        // 클리어 별 개수
        int star = 0;
        int currentCoin = coinController.CurrentCoin;
        float percent = (float)currentCoin / (float)needCoin;

        if (percent >= 1.75) star = 3;
        else if (percent >= 1.25f) star = 2;
        else if (percent >= 1f) star = 1;
        else star = 0;

        DD.GetComponent<UserData>().StageClear(selectedLevel, star);
        DD.GetComponent<UserData>().GetCoin(currentCoin);

        // 종료 패널
        panelGameOver.SetActive(true);
        panelGameOver.GetComponent<PanelGameOver>().Set(currentCoin, needCoin, star, selectedLevel);

        isGameOver = true;
    }
}
