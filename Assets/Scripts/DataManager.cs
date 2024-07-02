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

        // JSON 직렬화
        string jsonData = JsonHelper.ToJson(rankDatas, true);
        // 파일 저장
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

                // 랭킹 뒤로 밀기
                //for (int j = rankDatas.Length - 1; j > saveIndex; j--)
                //{
                //    rankDatas[j + 1] = rankDatas[j];
                //}

                //rankDatas[saveIndex] = rankData;
                break;
            }
        }

        // 랭크데이터 출력
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
