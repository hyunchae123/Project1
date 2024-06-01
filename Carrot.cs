using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    PlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerController>();
        }

    }

    void Update()
    {
        if (player != null)
        {
            ItemManager.Instance.CarrotPlus();
            Destroy(gameObject);
        }
    }
}
