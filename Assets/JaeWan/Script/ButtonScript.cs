using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void Ingame() 
    {
        SceneManager.LoadScene(1);
    }
    public void Title() 
    {
        SceneManager.LoadScene(0);
    }
}
