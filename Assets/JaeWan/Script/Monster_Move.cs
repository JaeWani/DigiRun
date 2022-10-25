using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : MonoBehaviour
{
    [SerializeField]
    float speed = 0.001f ;
    void Update()
    {
        transform.position += new Vector3(speed * -1, 0, 0);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player") 
        {
            GameManager.Get().Player_Hit();
            Destroy(gameObject);
        }

    }
}

