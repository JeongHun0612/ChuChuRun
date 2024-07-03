using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public RankData[] rankDatas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rankDatas = LoadRankData();

        if (rankDatas == null)
        {
            rankDatas = new RankData[]
            {
                new RankData {playerId = -1, score = 0f},
                new RankData {playerId = -1, score = 0f},
                new RankData {playerId = -1, score = 0f},
                new RankData {playerId = -1, score = 0f},
                new RankData {playerId = -1, score = 0f},
            };
        }
    }

    // RankData ========================================================================================================================

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
        for (int index = 0; index < rankDatas.Length; index++)
        {
            if (rankData.score > rankDatas[index].score)
            {
                int saveIndex = index;

                // ��ŷ �ڷ� �б�
                for (int j = rankDatas.Length - 2; j >= saveIndex; j--)
                {
                    rankDatas[j + 1] = rankDatas[j];
                }

                rankDatas[saveIndex] = rankData;
                break;
            }
        }
    }

    public RankData[] LoadRankData()
    {
        string filePath = Application.dataPath + "/RankData.json";

        if (!File.Exists(filePath))
        {
            Debug.Log("RankData.json Load Fail!");
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
