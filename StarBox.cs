using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBox : MonoBehaviour
{
    [SerializeField] GameObject StarPrefabs;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("onOpen");
            GameObject star1 = Instantiate(StarPrefabs, transform.position, Quaternion.identity);
            GameObject star2 = Instantiate(StarPrefabs, transform.position, Quaternion.identity);
            GameObject star3 = Instantiate(StarPrefabs, transform.position, Quaternion.identity);
            GameObject star4 = Instantiate(StarPrefabs, transform.position, Quaternion.identity);
            GameObject star5 = Instantiate(StarPrefabs, transform.position, Quaternion.identity);

            

        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
