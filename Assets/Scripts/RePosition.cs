using UnityEngine;
using UnityEngine.Events;

public class RePosition : MonoBehaviour
{
    public UnityEvent onMove;

    private Vector2 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private void LateUpdate()
    {
        if (transform.position.x > -17.4f) return;

        // 특정 좌표값을 넘어서면 포지션 재할당
        transform.Translate(34.8f, 0, 0, Space.Self);

        if (onMove != null)
        {
            onMove.Invoke();
        }
    }

    public void OnResetPos()
    {
        transform.position = initPos;
    }
}
