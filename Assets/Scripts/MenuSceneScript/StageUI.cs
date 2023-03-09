using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private Image[] starImage;
    [SerializeField] private Sprite fullOfStarSprite;

    [HideInInspector] public int Level;
    [HideInInspector] public int Star;

    private void Start()
    {
        // 별 이미지 교체
        for(int i = 0; i < Star; i++)
        {
            starImage[i].sprite = fullOfStarSprite;
        }
    }
}