using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speedRate;

    void Update()
    {
        if (!GameManager.instance.isLive) return;

        float totalSpeed = GameManager.instance.globalSpeed * speedRate * Time.deltaTime;
        transform.Translate(totalSpeed * Vector2.left);
    }
}
