using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float speedRate;
    public bool isTitleMove;

    private void Update()
    {
        if (!GameManager.instance.isLive && !isTitleMove) return;

        // ������Ʈ �̵�
        float totalSpeed = GameManager.instance.globalSpeed * speedRate * Time.deltaTime;
        transform.Translate(totalSpeed * Vector2.left);
    }

    public void OnReset()
    {
        transform.position = new Vector2(0f, transform.position.y);

        for (int index = 0; index < transform.childCount; index++)
        {
            // ��ġ �� ����
            transform.GetChild(index).GetComponent<RePosition>().OnResetPos();

            // ��ֹ� ����
            ChangeObject changeObject = transform.GetChild(index).GetComponent<ChangeObject>();
            if (changeObject)
            {
                changeObject.Change();
            }
        }
    }
}
