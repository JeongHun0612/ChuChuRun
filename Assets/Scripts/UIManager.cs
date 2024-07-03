using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject selectCharUI;

    public void ShowSelectChar(bool isShow)
    {
        selectCharUI.SetActive(isShow);

        if (!isShow)
        {
            PlayerPrefs.SetInt("PlayerId", GameManager.instance.playerId);
        }
    }
}
