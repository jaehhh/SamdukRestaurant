using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float XMin, XMax, YMin, YMax;

    float x, y;

    private void Update()
    {
        x = player.position.x;
        x = Clamp(x, XMin, XMax);

        y = player.position.y;
        y = Clamp(y, YMin, YMax);

        this.transform.position = new Vector3(x, y, -10);
    }

    private float Clamp(float value, float min, float max)
    {
        value = Mathf.Clamp(value, min, max);

        return value;
    }
}
