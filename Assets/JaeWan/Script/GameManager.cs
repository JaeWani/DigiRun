using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public class SaveData
{
    public int highest_score;
}
public class GameManager : MonoBehaviour
{
    public bool IsGameOver;

    [SerializeField] List<GameObject> Monster_5 = new List<GameObject>();

    private static GameManager _instance { get; set; } = null;
    public static GameManager Get() 
    {
        if (_instance == null)
            Debug.LogError($"{nameof(GameManager)}'s instance is null");

        return _instance;
    }

     private void Awake() 
    {
        if (_instance == null)
            _instance = this;

        IsGameOver = false;
        P_Collider = P.GetComponent<BoxCollider2D>();
        P_Anim = P.GetComponent<Animator>();
    }
    private void Start()
    {
        
        Debug.Log(P_LineIndex);
        //StartCoroutine(SpawnMonster());
        StartCoroutine(ScorePlus());
        Save();
        GameOver_Panel.SetActive(false) ;
    }
    [ContextMenu("To Json Data")]
    void Save() 
    {
        SaveData saveData = new SaveData();

        if (Score > saveData.highest_score)
            saveData.highest_score = Score;

        string json = JsonUtility.ToJson(saveData);

        string flieName = "DB";
        string path = Path.Combine(Application.dataPath, flieName + ".Json");

        File.WriteAllText(path, json);
    }
    
    [SerializeField] public int P_HP = 3;


    [SerializeField] GameObject GameOver_Panel;
    [SerializeField] Text GameOver_Score;
    [SerializeField] Text GameOver_Highest_Score;
    //스코어 텍스트 만들다가 껐다.

    [SerializeField]
    Text Score_Text;
    public int Score = 0;

    [SerializeField] public GameObject[] HP_UI = new GameObject[3];

    // 라인  ( 줄 )
    [SerializeField] 
    public Transform[] Line = new Transform[4];

    //몬스터 프리펩
    [SerializeField]
    public GameObject[] Monster = new GameObject[5];

    // 플레이어 
    [SerializeField] GameObject P;
    [SerializeField] Animator P_Anim;
    [SerializeField] BoxCollider2D P_Collider;

    //플레이어 라인 인덱스 번호
     [SerializeField] int P_LineIndex = 1;
     [SerializeField] int M_LineIndex;
     [SerializeField] int MonsterIndex;
     [SerializeField] int MonsterIndex2;
     [SerializeField] int TwoLine;

     [SerializeField] float MonsterSpawnTime = 3;
    //플레이어 라인 변경
    void ChangeLine()
    {
        if (IsGameOver == false) {
            if (P_LineIndex < 0)
                P_LineIndex = 0;
            else if (P_LineIndex > 3)
                P_LineIndex = 3;

            P.transform.position = new Vector3(-6, Line[P_LineIndex].position.y, 0);
            if (Input.GetKeyDown(KeyCode.UpArrow))
                P_LineIndex--;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                P_LineIndex++;
        }
    }

    //몬스터 스폰 코루틴
    IEnumerator SpawnMonster()
    {
        M_LineIndex = Random.Range(0, 4);
        MonsterIndex = Random.Range(0, 5);
        MonsterIndex2 = Random.Range(0,5);
        yield return new WaitForSecondsRealtime(MonsterSpawnTime);
        int rand = Random.Range(0, 2);
        TwoLine = Random.Range(0, 4);
        Vector3 position = new Vector3(10, Line[M_LineIndex].position.y, 0);
        if (rand == 0)
        {
            Instantiate(Monster[MonsterIndex], position, Quaternion.identity);
        }
        else if (rand == 1)
        {
            while (TwoLine == M_LineIndex) 
                    TwoLine = Random.Range(0, 4);
            
        Vector3 position2 = new Vector3(10, Line[TwoLine].position.y,0);

            Instantiate(Monster[MonsterIndex], position, Quaternion.identity);
            Instantiate(Monster[MonsterIndex2], position2, Quaternion.identity);
        }
        StartCoroutine(SpawnMonster());
    }
    IEnumerator ScorePlus()
    {
        yield return new WaitForSecondsRealtime(0.05f);
        Score += 3;
        Score_Text.text = Score + " M";
        StartCoroutine(ScorePlus());
    }
    private void Update()
    {
        ChangeLine();
        SetHP();
    }
   public void Player_Hit() 
    {
        P_HP--;
        StartCoroutine(P_invincibility());
    }
    public int P_invincibility_time;
    IEnumerator P_invincibility() 
    {
        Debug.Log("무적");
        P_Collider.isTrigger = true;
        P_Anim.SetBool("IsHit", true);
        yield return new WaitForSecondsRealtime(P_invincibility_time);
        P_Collider.isTrigger = false;
        P_Anim.SetBool("IsHit", false);
    }
    public void SetHP() 
    {
        if (P_HP <= 0)
        {
            HP_UI[0].SetActive(false);
            HP_UI[1].SetActive(false);
            HP_UI[2].SetActive(false);
            GameOver();
        }
        else if (P_HP == 1)
        {
            HP_UI[0].SetActive(false);
            HP_UI[1].SetActive(false);
        }
        else if (P_HP == 2)
        {
            HP_UI[0].SetActive(false);
        }
        else if (P_HP <= 3) 
        {
            P_HP = 3;
            HP_UI[0].SetActive(true);
            HP_UI[1].SetActive(true);
            HP_UI[2].SetActive(true);
        }
    }
    public void GameOver() 
    {
        GameOver_Panel.SetActive(true);
        StopAllCoroutines();
        GameOver_Score.text = Score + "M";
        IsGameOver = true;
    }

    // ================================== 몬스터 패턴  ======================================
    void Monster_5_P() 
    {
    }
    // ================================== 몬스터 패턴  ======================================
}
