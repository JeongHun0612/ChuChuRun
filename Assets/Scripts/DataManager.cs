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

        // JSON 직렬화
        string jsonData = JsonHelper.ToJson(rankDatas, true);

        // 파일 저장
        File.WriteAllText(filePath, jsonData);
    }

    public void UpdateRankData(RankData rankData)
    {
        for (int index = 0; index < rankDatas.Length; index++)
        {
            if (rankData.score > rankDatas[index].score)
            {
                int saveIndex = index;

                // 랭킹 뒤로 밀기
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

        // JSON 데이터 불러오기
        string jsonData = File.ReadAllText(filePath);

        // JSON을 객체 배열로 역직렬화
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
