using UnityEngine;

public class Player : MonoBehaviour
{
    public enum State { Stand, Run, Jump, Hit };

    public RuntimeAnimatorController[] runtimeAnim;

    public float startJumpPower;
    public float jumpPower;

    private bool isGround;
    private bool isJumpKey;

    private Rigidbody2D rigid;
    private Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!GameManager.instance.isLive) return;

        if (Input.GetButtonDown("Jump") && isGround)
        {
            rigid.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
            SoundManager.instance.PlaySFX(SoundManager.SFX.Jump);
        }

        isJumpKey = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        if (isJumpKey && !isGround)
        {
            jumpPower = Mathf.Lerp(jumpPower, 0f, 0.1f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private void OnEnable()
    {
        // 선택한 캐릭터의 따라 애니메이션 변경
        anim.runtimeAnimatorController = runtimeAnim[GameManager.playerId];
        ChangeAnim(State.Jump);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isGround) return;

        isGround = true;
        jumpPower = 1f;

        ChangeAnim(State.Run);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
        ChangeAnim(State.Jump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인 Trigger
        if (collision.CompareTag("Coin"))
        {
            SoundManager.instance.PlaySFX(SoundManager.SFX.Coin);

            collision.gameObject.SetActive(false);
            GameManager.coin++;

            return;
        }

        rigid.simulated = false;
        ChangeAnim(State.Hit);

        SoundManager.instance.PlaySFX(SoundManager.SFX.Hit);

        GameManager.instance.GameOver();
    }

    private void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state);
    }

    public void OnReset()
    {
        rigid.simulated = true;
        transform.position = new Vector2(transform.position.x, 1f);

        gameObject.SetActive(false);
    }
}
