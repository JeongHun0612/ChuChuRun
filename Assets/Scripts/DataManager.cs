using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public RankData[] rankDatas;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rankDatas = LoadRankData();
    }

    public void SaveRankData()
    {
        string filePath = Application.dataPath + "/RankData.json";

        // JSON ����ȭ
        string jsonData = JsonHelper.ToJson(rankDatas, true);
        // ���� ����
        File.WriteAllText(filePath, jsonData);
    }

    public void UpdateRankData(RankData rankData)
    {
        if (rankDatas == null)
        {
            rankDatas = new RankData[]
            {
                new RankData {playerId = 0, score = 0f},
                new RankData {playerId = 0, score = 0f},
                new RankData {playerId = 0, score = 0f},
                new RankData {playerId = 0, score = 0f},
                new RankData {playerId = 0, score = 0f},
            };
        }

        for (int index = 0; index < rankDatas.Length; index++)
        {
            if (rankData.score > rankDatas[index].score)
            {
                int saveIndex = index;

                Debug.Log("SaveIndex : " + saveIndex);

                // ��ŷ �ڷ� �б�
                //for (int j = rankDatas.Length - 1; j > saveIndex; j--)
                //{
                //    rankDatas[j + 1] = rankDatas[j];
                //}

                //rankDatas[saveIndex] = rankData;
                break;
            }
        }

        // ��ũ������ ���
        for (int i = 0; i < rankDatas.Length; i++)
        {
            Debug.Log("PlayerId : " + rankDatas[i].playerId + "  |  " + "Score : " + rankDatas[i].score);
        }
    }

    public RankData[] LoadRankData()
    {
        string filePath = Application.dataPath + "/RankData.json";

        if (!File.Exists(filePath))
        {
            Debug.Log("File Load Fail!");
            return null;
        }

        // JSON ������ �ҷ�����
        string jsonData = File.ReadAllText(filePath);

        // JSON�� ��ü �迭�� ������ȭ
        return JsonHelper.FromJson<RankData>(jsonData);
    }

    private void OnApplicationQuit()
    {
        SaveRankData();
    }
}


[System.Serializable]
public class RankData
{
    public int playerId;
    public float score;
}
