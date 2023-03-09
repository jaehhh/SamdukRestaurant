using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpFadeEffect : MonoBehaviour
{
    private SpriteRenderer mySprite;

    [SerializeField] private float fadeTime;
    [SerializeField] private float MoveSpeed;

    private void Awake()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(Fade(1,0));
        StartCoroutine("MoveUp");
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = mySprite.color;
            color.a = Mathf.Lerp(start, end, percent);
            mySprite.color = color;

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator MoveUp()
    {
        float moveY = Time.deltaTime * MoveSpeed;

        while (true)
        {
            Vector3 pos = new Vector3(0, moveY, 0);
            transform.position += pos;

            yield return null;
        }
    }
}
