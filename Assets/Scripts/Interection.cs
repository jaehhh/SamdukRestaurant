using UnityEngine;

public class Interection : MonoBehaviour
{
    public virtual void Interect(GameObject user, FoodState food) { }

    public virtual void StopInterection(GameObject user) { }

    public virtual void SetUser(GameObject user) { }

    public virtual void CheckHandling(bool isHandling) { }
}
