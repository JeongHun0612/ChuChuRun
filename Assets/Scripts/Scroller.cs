using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speedRate;
    public bool isTest;

    private void Update()
    {
        if (!GameManager.instance.isLive && !isTest) return;

        // 오브젝트 이동
        float totalSpeed = GameManager.instance.globalSpeed * speedRate * Time.deltaTime;
        transform.Translate(totalSpeed * Vector2.left);
    }

    public void OnReset()
    {
        transform.position = new Vector2(0f, transform.position.y);

        for (int index = 0; index < transform.childCount; index++)
        {
            transform.GetChild(index).GetComponent<RePosition>().OnResetPos();
        }
    }
}
