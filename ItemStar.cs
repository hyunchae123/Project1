using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStar : MonoBehaviour
{
    Animator anim;

    PlayerController player;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }

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
            ItemManager.Instance.StarPlus();
            Destroy(gameObject);
        }
    }
}
