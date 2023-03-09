using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy dontDestroy;

    private void Awake()
    {
        if (dontDestroy == null)
        {
            dontDestroy = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(dontDestroy != this)
            Destroy(gameObject);
        }
    }
}
