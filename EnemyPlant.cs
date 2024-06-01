using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : MonoBehaviour
{
    LayerMask layer;
    PlayerController player;

    Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }
    }


    void Start()
    {
        layer = 1 << LayerMask.NameToLayer("Player");
        anim = GetComponent<Animator>();
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

        if (player != null)
        {
            player.rigid.velocity = new Vector2(player.rigid.velocity.x, 0f);
            player.rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            player.anim.SetTrigger("onJump");
            player.jumpcount++;
            player = null;
            anim.SetTrigger("onDeath");
        }
        else
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, 2f, layer);

            if (collider != null)
            {
                anim.SetTrigger("onAttack");
            }
            else
            {
                anim.SetTrigger("onIdle");
            }
        }
        
    }

    public void EnemyDeath()
    {
        Destroy(gameObject);
    }

}
