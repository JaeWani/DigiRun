using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waste_paper : MonoBehaviour
{
    [Header(" 회전 속도 조전")]
    [SerializeField] [Range(-100, 100f)] float RotateSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0,0,RotateSpeed));
    }
}
