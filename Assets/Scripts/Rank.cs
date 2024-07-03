using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public Sprite[] iconSprites;

    private void OnEnable()
    {
        RankData[] rankDatas = DataManager.instance.rankDatas;

        for (int index = 0; index < rankDatas.Length; index++)
        {
            int playerId = rankDatas[index].playerId + 1;
            float score = rankDatas[index].score;

            transform.GetChild(index).GetComponentInChildren<Image>().sprite = iconSprites[playerId];
            transform.GetChild(index).GetComponentInChildren<Text>().text = score.ToString("F0");
        }
    }
}
