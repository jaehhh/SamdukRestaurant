using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stage 정보 UI 생성하는 스크립트

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject stageInfoUIPrefab;
    [SerializeField] private GameObject canvas;

    public void CreateUI(int level, int star, Transform target)
    {
        GameObject clone = Instantiate(stageInfoUIPrefab);

        clone.GetComponent<StageUI>().Level = level;
        clone.GetComponent<StageUI>().Star = star;

        clone.transform.SetParent(canvas.transform);
        clone.transform.position = target.position + Vector3.down * 1.5f;
    }
}
