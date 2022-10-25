using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    MeshRenderer rd;

    private float offset;
    [SerializeField] private float speed;
    void Start()
    {
        rd = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        rd.material.mainTextureOffset = new Vector2(offset , 0);
    }
}
