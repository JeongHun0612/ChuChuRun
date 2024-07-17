using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    const float ORIGIN_SPEED = 4f;
    const int RANK_CNT = 5;

    [Header("# GameData")]
    public static int playerId = -1;
    public static int coin = -1;
    public static RankData[] rankDatas;

    [Header("# Game Control")]
    public bool isLive;
    public float globalSpeed;
    public float score;

    [Header("# GameOjbect")]
    public Player player;
    public GameObject uiGameOver;
    public Scroller[] scrollers;

    private void Awake()
    {
        instance = this;
        InitPlayerPrefsData();
    }

    private void Start()
    {
        isLive = false;
        globalSpeed = ORIGIN_SPEED;

        SoundManager.instance.PlayBGM(SoundManager.BGM.Title);

        if (playerId == -1) playerId = PlayerPrefs.GetInt("PlayerId");
        if (coin == -1) coin = PlayerPrefs.GetInt("Coin");
        LoadRankData(ref rankDatas);
    }

    private void Update()
    {
        if (isLive)
        {
            score += Time.deltaTime * 2f;
            globalSpeed = ORIGIN_SPEED + score * 0.01f;
        }
    }

    public void GameStart()
    {
        isLive = true;
        player.gameObject.SetActive(true);

        SoundManager.instance.PlayBGM(SoundManager.BGM.Run);
    }

    public void GameOver()
    {
        isLive = false;
        uiGameOver.SetActive(true);

        // 현재 랭크 데이터 업데이트
        UpdateRankData(new RankData { playerId = GameManager.playerId, score = this.score });

        // PlayerPrefs 저장
        SavePlayerPrefs();
    }

    public void GameReStart()
    {
        SoundManager.instance.PlaySFX(SoundManager.SFX.Click);

        // 데이터 리셋
        score = 0f;
        player.OnReset();

        foreach (Scroller scroller in scrollers)
        {
            scroller.OnReset();
        }

        // 게임 시작
        GameStart();
    }

    public void MoveTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadRankData(ref RankData[] rankDatas)
    {
        if (rankDatas != null) return;

        rankDatas = new RankData[RANK_CNT];

        for (int index = 0; index < rankDatas.Length; index++)
        {
            rankDatas[index].playerId = PlayerPrefs.GetInt("Rank" + index + "_Id");
            rankDatas[index].score = PlayerPrefs.GetFloat("Rank" + index + "_Score");
        }
    }

    private void SaveRankData(int rank, int playerId, float score)
    {
        PlayerPrefs.SetInt("Rank" + rank + "_Id", playerId);
        PlayerPrefs.SetFloat("Rank" + rank + "_Score", score);
    }

    private void UpdateRankData(RankData rankData)
    {
        for (int i = 0; i < rankDatas.Length; i++)
        {
            if (rankData.score < rankDatas[i].score) continue;

            int saveIndex = i;

            // 랭킹 뒤로 밀기
            for (int j = rankDatas.Length - 2; j >= saveIndex; j--)
            {
                rankDatas[j + 1] = rankDatas[j];
            }

            rankDatas[saveIndex] = rankData;
            break;
        }
    }

    private void InitPlayerPrefsData()
    {
        if (!PlayerPrefs.HasKey("PlayerId"))
        {
            PlayerPrefs.SetInt("PlayerId", 0);
        }

        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
        }

        if (!PlayerPrefs.HasKey("Rank0_Id"))
        {
            for (int index = 0; index < RANK_CNT; index++)
            {
                SaveRankData(index, -1, 0f);
            }
        }
    }

    private void OnApplicationQuit()
    {
        SavePlayerPrefs();

        PlayerPrefs.Save();
    }

    private void SavePlayerPrefs()
    {
        for (int index = 0; index < rankDatas.Length; index++)
        {
            SaveRankData(index, rankDatas[index].playerId, rankDatas[index].score);
        }

        PlayerPrefs.SetInt("PlayerId", playerId);
        PlayerPrefs.SetInt("Coin", coin);
    }
}

public struct RankData
{
    public int playerId;
    public float score;
}