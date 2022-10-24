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
            Debug.Log("¾Æ¾Æ¾Ó");
            Destroy(gameObject);
        }
    }
}

