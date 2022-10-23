using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : MonoBehaviour
{
    [SerializeField]
    float speed = 0.001f ;

    [SerializeField]
    float DelTime = 5;

    private void Start()
    {
        StartCoroutine(DestroyObj());
    }
    void Update()
    {
        transform.position += new Vector3(speed * -1, 0, 0);
    }
    IEnumerator DestroyObj() 
    {
        yield return new WaitForSecondsRealtime(DelTime);
        Destroy(gameObject);
    }
}
