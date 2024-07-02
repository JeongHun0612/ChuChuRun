using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    const float ORIGIN_SPEED = 3f;

    [Header("# Game Control")]
    public bool isLive;
    public int playerId;
    public float globalSpeed;
    public float score;

    [Header("# GameOjbect")]
    public Player player;
    public GameManager uiScore;
    public GameObject uiGameOver;


    private void Awake()
    {
        instance = this;

        isLive = false;

        InitPlayerPrefs();

        playerId = PlayerPrefs.GetInt("PlayerId");
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

        SoundManager.instance.PlaySFX(SoundManager.SFX.Click);

        SoundManager.instance.PlayBGM(SoundManager.BGM.Run);
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        isLive = false;
        uiGameOver.SetActive(true);

        Debug.Log(score);

        float highScore = PlayerPrefs.GetFloat("Score");
        PlayerPrefs.SetFloat("Score", Mathf.Max(highScore, score));


        // 현재 랭크 데이터 업데이트
        RankData rankData = new RankData { playerId = this.playerId, score = this.score };
        DataManager.instance.UpdateRankData(rankData);
    }

    public void SelectCharcter()
    {
        PlayerPrefs.SetInt("PlayerId", playerId);
    }

    private void InitPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetFloat("Score", 0);
        }

        if (!PlayerPrefs.HasKey("PlayerId"))
        {
            PlayerPrefs.SetInt("PlayerId", 0);
        }
    }
}
