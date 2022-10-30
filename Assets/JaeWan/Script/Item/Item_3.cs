using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_3 : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 100f)]
    float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed * -1, 0, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Get().Player_HP_Plus();
        Debug.Log("æ∆¿Ã≈€ 1");
    }
}
