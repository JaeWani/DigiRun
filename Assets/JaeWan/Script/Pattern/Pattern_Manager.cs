using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern_Manager : MonoBehaviour
{
    GameObject me;
    private void Start()
    {
        me = GetComponent<GameObject>();
    }
  //  private void OnCollisionEnter2D(Collision2D collision)
   // {
     //   if (collision.gameObject.tag == "Wall") 
     //   {
           // int index = GameManager.Get().Pattern_Index;
          //  GameManager.Get().Pattern[index].transform.position = new Vector3(0,0,0);
          //  Debug.Log("¾¾¹ß");
           // GameManager.Get().Random_Pattern();
           // GameManager.Get().Pattern[index].SetActive(false);
    //    }
  //  }

}
