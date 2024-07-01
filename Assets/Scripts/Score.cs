using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public bool isHighScore;

    private float highScore;
    private Text uiText;

    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    void Start()
    {
        if (isHighScore)
        {
            highScore = PlayerPrefs.GetFloat("Score");
            uiText.text = highScore.ToString("F0");
        }
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;

        if (isHighScore && GameManager.instance.score < highScore)
            return;

        uiText.text = GameManager.instance.score.ToString("F0");
    }
}
