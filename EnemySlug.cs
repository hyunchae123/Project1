using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlug : MonoBehaviour
{
    [SerializeField] Vector3[] positions;

    SpriteRenderer sprite;
    LayerMask layer;
    PlayerController player;

    Animator anim;

    Vector3 originPosition; //게임 시작시 원점
    int index;              //position index
    Vector3 destination => positions[index];

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }
    }

    void Start()
    {
        //이동관련
        originPosition = transform.position;
        index = 0;

        //충돌관련
        layer = 1 << LayerMask.NameToLayer("Player");
        anim = GetComponent<Animator>();

        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GameManager.Instance.isPause == true)
        {
            anim.speed = 0.0f;
            return;
        }
        else
        {
            anim.speed = 1.0f;
        }

        //충돌관련
        if (player != null)
        {
            player.rigid.velocity = new Vector2(player.rigid.velocity.x, 0f);
            player.rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            player.anim.SetTrigger("onJump");
            player.jumpcount++;
            player = null;
            anim.SetTrigger("onDeath");
        }

        //이동관련
        transform.position = Vector3.MoveTowards(transform.position, positions[index], 1 * Time.deltaTime);  //일단 position0으로 움직이고

        if (transform.position == positions[0])
        {
            sprite.flipX = true;

            index += 1;
        }
        else if (transform.position == positions[1])
        {
            sprite.flipX = false;

            index -= 1;
        }
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
}
