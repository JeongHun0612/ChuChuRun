using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    private Text coinText;

    private void Awake()
    {
        coinText = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        coinText.text = GameManager.coin.ToString();
    }
}
