using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData : MonoBehaviour
{
    public int coin = 0; // 코인

    public int clearStage; // 클리어한 스테이지
    public List<int> clearStar = new List<int>(); // 클리어 스테이지 별 개수

    public int SelectedStage; // 선택한 스테이지

    private void Awake()
    {
        clearStar.Add(0);
    }

    public void StageClear(int stageLevel, int star)
    {
        if(clearStar[stageLevel] < star)
        clearStar[stageLevel] = star; // 별 개수 갱신

        if (stageLevel == clearStage && star >= 1) // 현재스테이지가 최고스테이지면
        {
            clearStage++;
            clearStar.Add(0); // 다음 스테이지 추가
        }
    }

    public void GetCoin(int coin)
    {
        this.coin += coin;
    }

    public void GetInfo(int level, out bool canSelect, out int star)
    {
        if(level <= clearStage)
        {
            canSelect = true;
            star = clearStar[level];
        }
        else
        {
            canSelect = false;
            star = 0;
        }
    }
}
