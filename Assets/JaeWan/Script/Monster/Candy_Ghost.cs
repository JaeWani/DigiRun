using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy_Ghost : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Get().StartCandy();
        }
    }
}
