using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] public bool isGrounded;

    const int MAX_JUMP_COUNT = 1;

    public int jumpcount;
    public bool isDuck;
    bool isLock;
    public bool isOnLadder;
    
    public Animator anim;
    public Rigidbody2D rigid;
    SpriteRenderer sprite;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(GameManager.Instance.isPause == true)
        {
            anim.speed = 0.0f;
            return;
        }
        else
        {
            anim.speed = 1.0f;
        }

        if(isOnLadder == false && isLock == false)
        {
            Movement();
        }

        CheckGround();
        Jump();
        Duck();

        anim.SetBool("isMove", rigid.velocity.x != 0);      //rigid.velocity.x�� 0���� �ƴ����� �Ź� üũ�������
        anim.SetBool("isDuck", isDuck);
        anim.SetFloat("VelocityY", rigid.velocity.y);       //�ִϸ��̼� �Ķ���� float�� velocityY�� player�� y�ӵ��� ��� �־������
    }

    public void Movement()
    {
        float AxisX = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(AxisX * moveSpeed, rigid.velocity.y);

        if (AxisX != 0)
        {
            sprite.flipX = (AxisX == -1);
        }

    }

    public void CheckGround()
    {
        int groundLayer = 1 << LayerMask.NameToLayer("Ground");

        Collider2D target = Physics2D.OverlapCircle(transform.position, 0.05f, groundLayer);
        //player�� pivot���� 0.1ũ���� ���� ����� �� ���̶� �浹�� collider�� layer�� groundLayer�� ������ ������ ����
        isGrounded = target != null;

        if(isGrounded == true)
        {
            anim.SetBool("isGround", true);
        }
        else
        {
            anim.SetBool("isGround", false);
        }
    }

    public void Jump()
    {
        if (isGrounded == true && rigid.velocity.y <= 0)
        {
            jumpcount = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Z) && jumpcount < MAX_JUMP_COUNT && isOnLadder == false)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("onJump");
            jumpcount++;
        }
    }

    public void Duck()
    {
        if (Input.GetKey(KeyCode.X) && isGrounded == true)
        {
            isDuck = true;
            rigid.velocity = Vector2.zero;
        }
        else
        {
            isDuck = false;
        }
    }

    public void Climb()
    {
        float AxisY = Input.GetAxisRaw("Vertical");
        if (AxisY != 0)
        {
            anim.speed = 1.0f;
            rigid.velocity = new Vector2(0f, AxisY * moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.speed = 0.0f;
        }
    }

    public void onHurt()
    {
        if (isDuck == true)
            return;

        Vector2 dir;

        if (rigid.velocity.x != 0)
        {
            dir = Vector2.up + (rigid.velocity.x < 0 ? Vector2.right : Vector2.left);
        }
        else
        {
            dir = Vector2.left + Vector2.up;
        }

        dir.Normalize();
        rigid.velocity = Vector2.zero;
        rigid.AddForce(dir * 4f, ForceMode2D.Impulse);
        anim.SetTrigger("onHurt");
        ItemManager.Instance.CarrotMinus();
        jumpcount++;
        isLock = true;
        isGrounded = false;

        StartCoroutine(IEHurt());

    }

    IEnumerator IEHurt()
    {
        while(true)
        {
            if (rigid.velocity.y <= 0f && isGrounded)   //�������鼭 ���� �����ϸ�
            {
                break;                                  //while�� �հ� ������ Coroutine ����
            }
            
            yield return null;
        }

        isLock = false;
    }
}
