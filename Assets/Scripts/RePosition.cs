using UnityEngine;
using UnityEngine.Events;

public class RePosition : MonoBehaviour
{
    public UnityEvent onMove;

    private void LateUpdate()
    {
        if (transform.position.x > -17.7f) return;

        transform.Translate(35.4f, 0, 0, Space.Self);

        if (onMove != null)
        {
            onMove.Invoke();
        }
    }
}
