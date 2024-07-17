using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public Sprite[] iconSprites;

    private void OnEnable()
    {
        for (int index = 0; index < GameManager.rankDatas.Length; index++)
        {
            int playerId = GameManager.rankDatas[index].playerId + 1;
            float score = GameManager.rankDatas[index].score;

            transform.GetChild(index).GetComponentInChildren<Image>().sprite = iconSprites[playerId];
            transform.GetChild(index).GetComponentInChildren<Text>().text = score.ToString("F0");
        }
    }
}
