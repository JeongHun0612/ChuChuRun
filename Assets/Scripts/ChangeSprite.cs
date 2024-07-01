using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Change();
    }

    public void Change()
    {
        int ran = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[ran];
    }
}
