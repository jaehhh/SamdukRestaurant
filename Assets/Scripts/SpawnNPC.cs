using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    // 스폰 빈도
    // 스폰량
    // 특수개체 스폰 빈도

    [SerializeField] private GameObject NPCPrefab;
    [SerializeField] private Transform spawnPoint;

    public int spawnTime;
    public int[] needFood;

    public void StartSpawn()
    {
        StartCoroutine("Spawn");
    }

    public void StopSpawn()
    {
        StopCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            GameObject clone = Instantiate(NPCPrefab);

            clone.transform.position = spawnPoint.position;

            int random = Random.Range(0, needFood.Length);
            clone.GetComponent<NPCState>().needFood = needFood[random];

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
