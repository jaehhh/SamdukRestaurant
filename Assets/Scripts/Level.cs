using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public class LevelTable
    {
        public int spawnTime; // 손님 스폰 주기
        public int[] needFood; // 손님이 원하게 되는 음식, 레시피 코드
        public FoodType[] useFood; // 스테이지에서 사용하게 될 재료, 프리팹등록된 재료와 FoodType 비교
        public int needCoin; // 목표 금액
    }

    public List<LevelTable> levelTable = new List<LevelTable>();

    private void Awake()
    {
        SetLevel();
    }


    // 작성되는 테이블
    private void SetLevel()
    {
        { // # lv0
            {
                int spawnTime = 9;
                int[] needFood = { 1 };
                FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.chocolate, FoodType.pepperPowder };
                int needCoin = 1000;

                Set(spawnTime, needFood, useFood, needCoin);
            }
        }

        {
            // # lv1

            int spawnTime = 9;
            int[] needFood = { 1, 2 };
            FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.makaCookie, FoodType.chocolate, FoodType.pepperPowder };
            int needCoin = 1000;

            Set(spawnTime, needFood, useFood, needCoin);
        }

        {
            // # lv2

            int spawnTime = 9;
            int[] needFood = { 1, 2, 3};
            FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.makaCookie, FoodType.chocolate, FoodType.pepperPowder, FoodType.salt };
            int needCoin = 1500;

            Set(spawnTime, needFood, useFood, needCoin);
        }

        {
            // # lv3

            int spawnTime = 8;
            int[] needFood = { 1, 2, 3, 4 };
            FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.makaCookie, FoodType.chocolate, FoodType.pepperPowder, FoodType.salt };
            int needCoin = 1500;

            Set(spawnTime, needFood, useFood, needCoin);
        }

        {
            // # lv4

            int spawnTime = 8;
            int[] needFood = { 1, 2, 3, 4 };
            FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.makaCookie, FoodType.chocolate, FoodType.pepperPowder, FoodType.salt };
            int needCoin = 1550;

            Set(spawnTime, needFood, useFood, needCoin);
        }

        {
            // # lv5

            int spawnTime = 8;
            int[] needFood = { 1, 2, 3, 4 };
            FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.makaCookie, FoodType.chocolate, FoodType.pepperPowder, FoodType.salt };
            int needCoin = 1550;

            Set(spawnTime, needFood, useFood, needCoin);
        }

        {
            // # lv6

            int spawnTime = 7;
            int[] needFood = { 1, 2, 3, 4 };
            FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.makaCookie, FoodType.chocolate, FoodType.pepperPowder, FoodType.salt };
            int needCoin = 2000;

            Set(spawnTime, needFood, useFood, needCoin);
        }

        {
            // # lv7

            int spawnTime = 7;
            int[] needFood = { 1, 2, 3, 4 };
            FoodType[] useFood = { FoodType.dish, FoodType.noodle, FoodType.makaCookie, FoodType.chocolate, FoodType.pepperPowder, FoodType.salt };
            int needCoin = 2500;

            Set(spawnTime, needFood, useFood, needCoin);
        }
    }

    private void Set(int spawnTime, int[] needFood, FoodType[] useFood, int needCoin)
    {
        levelTable.Add(new LevelTable
        {
            spawnTime = spawnTime,
            needFood = needFood,
            useFood = useFood,
            needCoin = needCoin
        });
    }
}
