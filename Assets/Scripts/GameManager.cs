using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    const float ORIGIN_SPEED = 3f;

    [Header("# Game Control")]
    public bool isLive;
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

        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetFloat("Score", 0);
        }
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

        float highScore = PlayerPrefs.GetFloat("Score");
        PlayerPrefs.SetFloat("Score", Mathf.Max(highScore, score));
    }
}
