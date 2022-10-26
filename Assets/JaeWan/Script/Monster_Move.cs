using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
            GameManager.Get().Player_Hit();
    }
}

