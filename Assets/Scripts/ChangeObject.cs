using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject[] coins;
    public float coinPerRate;

    private GameObject[] objs;

    private void Awake()
    {
        objs = new GameObject[transform.childCount];

        for (int index = 0; index < objs.Length; index++)
        {
            objs[index] = transform.GetChild(index).gameObject;
        }
    }

    private void Start()
    {
        Change();
    }

    public void Change()
    {
        // ���� ������Ʈ ��ȯ
        int ran = Random.Range(0, objs.Length);

        for (int index = 0; index < objs.Length; index++)
        {
            transform.GetChild(index).gameObject.SetActive(ran == index);
        }


        // ���� Ȯ���� ���� ����
        float coinRan = Random.Range(0f, 1f);
        int ranIdx = Random.Range(0, coins.Length);

        for (int index = 0; index < coins.Length; index++)
        {
            coins[index].SetActive(ranIdx == index && coinRan <= coinPerRate);
        }
    }
}
