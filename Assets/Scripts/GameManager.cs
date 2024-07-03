using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    const float ORIGIN_SPEED = 4f;

    [Header("# Game Control")]
    public bool isLive;
    public int playerId;
    public float globalSpeed;
    public float score;
    public int coin;
    public Scroller[] scrollers;

    [Header("# GameOjbect")]
    public Player player;
    public GameObject uiGameOver;

    private void Awake()
    {
        instance = this;
        InitPlayerPrefsData();
    }

    private void Start()
    {
        isLive = false;
        SoundManager.instance.PlayBGM(SoundManager.BGM.Title);

        playerId = PlayerPrefs.GetInt("PlayerId");
        coin = PlayerPrefs.GetInt("Coin");
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
        SoundManager.instance.PlaySFX(SoundManager.SFX.Click);

        isLive = true;
        player.gameObject.SetActive(true);

        SoundManager.instance.PlayBGM(SoundManager.BGM.Run);
    }

    public void GameOver()
    {
        isLive = false;
        uiGameOver.SetActive(true);

        // 현재 랭크 데이터 업데이트
        RankData rankData = new RankData { playerId = this.playerId, score = this.score };
        DataManager.instance.UpdateRankData(rankData);
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
        PlayerPrefs.SetInt("PlayerId", playerId);
        PlayerPrefs.SetInt("Coin", coin);

        SceneManager.LoadScene(0);
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
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("PlayerId", playerId);
        PlayerPrefs.SetInt("Coin", coin);
    }
}
