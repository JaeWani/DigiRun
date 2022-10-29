using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    SoundManager sm;

    private void Start()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void SoundTest()
    {
        sm.clickSoundPlay();
    }

    public void Ingame() 
    {
        sm.clickSoundPlay();
        SceneManager.LoadScene(1);
    }
    public void Title() 
    {
        sm.clickSoundPlay();
        SceneManager.LoadScene(0);
    }
}
