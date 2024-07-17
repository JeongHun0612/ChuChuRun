using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    private Image frameImage;

    public int playerId;
    public SelectCharacter[] chars;
    public Image charImage;
    public Animator charAnim;

    private void Awake()
    {
        frameImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        ChangeSelect(playerId == GameManager.playerId);
    }

    public void OnSelect()
    {
        GameManager.playerId = playerId;

        for (int index = 0; index < chars.Length; index++)
        {
            chars[index].ChangeSelect(chars[index] == this);
        }
    }

    private void ChangeSelect(bool isSelect)
    {
        charAnim.SetBool("IsSelect", isSelect);
        frameImage.color = (isSelect) ? Color.white : new Color(0.6f, 0.6f, 0.6f, 1f);
        charImage.color = (isSelect) ? Color.white : new Color(0.6f, 0.6f, 0.6f, 1f);
    }
}
