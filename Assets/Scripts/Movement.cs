using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector3 direction)
    {
        rigid.velocity = direction * moveSpeed;
    }
}