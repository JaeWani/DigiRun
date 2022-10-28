using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPhone_Ghost : MonoBehaviour
{
    [Header("휴대폰 귀신 피격 씬")]
    [SerializeField] GameObject Monster_10_Hit_Image;
    [SerializeField] SpriteRenderer Monster_10_Hit_Sprite;
    [SerializeField] Animator Monster_10_Anim;
    void Start()
    {
        Monster_10_Hit_Image.SetActive(false);
    }
    IEnumerator Monster_10_Hit_FadeOut() 
    {
        yield return new WaitForSecondsRealtime(1);
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = Monster_10_Hit_Sprite.color;
            c.a = f;
            Monster_10_Hit_Sprite.color = c;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private void Hit() 
    {

        Monster_10_Hit_Image.SetActive(true);
        Monster_10_Anim.SetBool("IsFade", true);
        // 이 부분에 사운드 넣기
        StartCoroutine(Monster_10_Hit_FadeOut());
        Monster_10_Anim.SetBool("IsFade", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            Hit();
        }
    }
}
