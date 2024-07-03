using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text uiText;

    private void Awake()
    {
        uiText = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive) return;

        uiText.text = string.Format("SCORE : {0}", GameManager.instance.score.ToString("F0"));
    }
}
