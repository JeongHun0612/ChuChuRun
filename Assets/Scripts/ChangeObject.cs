using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public GameObject coin;
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
        // 랜덤 오브젝트 변환
        int ran = Random.Range(0, objs.Length);

        for (int index = 0; index < objs.Length; index++)
        {
            transform.GetChild(index).gameObject.SetActive(ran == index);
        }


        // 랜덤 확률로 코인 생성
        float coinRan = Random.Range(0f, 1f);
        coin.SetActive(coinRan <= coinPerRate);
    }
}
