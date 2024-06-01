using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStar2 : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;

    PlayerController player;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }

    }

    void Update()
    {
        if (GameManager.Instance.isPause == true)
        {
            anim.speed = 0.0f;
            rigid.velocity = Vector2.zero;
            return;
        }
        else
        {
            anim.speed = 1.0f;
        }

        if (player != null)
        {
            ItemManager.Instance.StarPlus();
            Destroy(gameObject);
        }
    }
}
