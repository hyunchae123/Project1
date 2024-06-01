using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ladder : MonoBehaviour
{
    PlayerController player;
    float AxisY;
    float AxisX;

    [SerializeField] bool inTrigger;
    [SerializeField] Collider2D Ground;
    [SerializeField] Transform pivotX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            inTrigger = true;

            if (player.jumpcount == 1 && Input.GetKey(KeyCode.UpArrow))
            {
                player.isOnLadder = true;
                player.rigid.velocity = new Vector2(0f, AxisY * player.moveSpeed);
                player.rigid.gravityScale = 0;
                player.transform.position = new Vector2(pivotX.position.x, player.transform.position.y);
                player.anim.SetTrigger("onClimb");
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKey(KeyCode.DownArrow))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), Ground, true);
        }

        if(Input.GetKeyDown(KeyCode.Z) && AxisX != 0)
        {
            inTrigger = false;
            player.isOnLadder = false;
            player.anim.SetTrigger("onJump");
            player.anim.speed = 1.0f;
            player.rigid.gravityScale = 1;
            player.rigid.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
            player.jumpcount++;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
            player.isOnLadder = false;
            player.anim.speed = 1.0f;
            player.rigid.gravityScale = 1;
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), Ground, false);
        }
    }

    public void Update()
    {
        AxisY = Input.GetAxisRaw("Vertical");
        AxisX = Input.GetAxisRaw("Horizontal");

        if (player != null) 
        {
            //사다리타기 Start
            StartLadder();

            //사다리타기 End
            EndLadder();

            //사다리타기 Ing
            IngLadder();
        }
    }

    private void StartLadder()
    {
        if ((inTrigger == true && AxisY != 0 && player.isGrounded == true) || (inTrigger == true && AxisY == 0 && player.isGrounded == false && player.jumpcount == 0)) 
        {
            player.isOnLadder = true;
            player.rigid.velocity = new Vector2(0f, AxisY * player.moveSpeed);
            player.rigid.gravityScale = 0;
            player.transform.position = new Vector2(pivotX.position.x, player.transform.position.y);
            player.anim.SetTrigger("onClimb");
        }
    }

    private void EndLadder()
    {
        if(player.isGrounded == true && AxisY == 0)
        {
            player.isOnLadder = false;
            player.anim.speed = 1.0f;
            player.rigid.gravityScale = 1;
        }
    }


    private void IngLadder()
    {
        if (AxisY == 0 && player.isOnLadder == true && inTrigger == true && player.isGrounded == false)
        {
            player.rigid.velocity = Vector2.zero;
            player.anim.speed = 0.0f;
            player.rigid.gravityScale = 0;
            player.transform.position = new Vector2(pivotX.position.x, player.transform.position.y);
        }

        if (AxisY != 0 && player.isOnLadder == true && inTrigger == true && player.isGrounded == false)
        {
            player.rigid.velocity = new Vector2(0f, AxisY * player.moveSpeed);
            player.anim.speed = 1.0f;
            player.rigid.gravityScale = 0;
            player.transform.position = new Vector2(pivotX.position.x, player.transform.position.y);
        }

    }



}
