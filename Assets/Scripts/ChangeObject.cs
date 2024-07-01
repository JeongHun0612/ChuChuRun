using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    private GameObject[] objs;

    private void Awake()
    {
        objs = new GameObject[transform.childCount];

        for (int index = 0; index < objs.Length; index++)
        {
            objs[index] = transform.GetChild(index).gameObject;
        }
    }

    public void Change()
    {
        int ran = Random.Range(0, objs.Length);

        for (int index = 0; index < objs.Length; index++)
        {
            transform.GetChild(index).gameObject.SetActive(ran == index);
        }
    }
}
