using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Move : MonoBehaviour
{
    [SerializeField] [Range (0f,100f)] 
    float speed;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += new Vector3(speed * -1, 0, 0);
    }
}
