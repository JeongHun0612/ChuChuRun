using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour
{
    public Sprite[] iconSprites;

    private RankData[] rankDatas;

    void Awake()
    {
        rankDatas = DataManager.instance.LoadRankData();
    }

    private void OnEnable()
    {
        for (int index = 0; index < rankDatas.Length; index++)
        {
            int playerId = rankDatas[index].playerId;
            float score = rankDatas[index].score;

            transform.GetChild(index).GetComponentInChildren<Image>().sprite = iconSprites[playerId];
            transform.GetChild(index).GetComponentInChildren<Text>().text = score.ToString("F0");
        }
    }
}
