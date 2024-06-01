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

    Vector3 originPosition; //���� ���۽� ����
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
        //�̵�����
        originPosition = transform.position;
        index = 0;

        //�浹����
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

        //�浹����
        if (player != null)
        {
            player.rigid.velocity = new Vector2(player.rigid.velocity.x, 0f);
            player.rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            player.anim.SetTrigger("onJump");
            player.jumpcount++;
            player = null;
            anim.SetTrigger("onDeath");
        }

        //�̵�����
        transform.position = Vector3.MoveTowards(transform.position, positions[index], 1 * Time.deltaTime);  //�ϴ� position0���� �����̰�

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
