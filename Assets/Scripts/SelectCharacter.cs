using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{
    public int playerId;
    public SelectCharacter[] chars;

    private Image image;
    private Animator anim;

    private void Awake()
    {
        image = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        ChangeSelect(playerId == GameManager.instance.playerId);
    }

    public void OnSelect()
    {
        GameManager.instance.playerId = playerId;

        for (int index = 0; index < chars.Length; index++)
        {
            chars[index].ChangeSelect(chars[index] == this);
        }
    }

    private void ChangeSelect(bool isSelect)
    {
        anim.SetBool("IsSelect", isSelect);
        image.color = (isSelect) ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
    }
}
