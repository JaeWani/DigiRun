using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

[System.Serializable]
public class SaveData
{
    public int highest_score;
}
public class GameManager : MonoBehaviour
{
    public bool IsGameOver;
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
        Hit_Image[0].SetActive(false);
        //StartCoroutine(SpawnMonster());
        StartCoroutine(ScorePlus());
        Save();
        GameOver_Panel.SetActive(false) ;
        Random_Pattern();

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
    //============���� ����=================
    [Header ("����")]
    [SerializeField] List<Sprite> Pattern_Sprite = new List<Sprite>();// 0. �Ϲ� ���� / 1. ���ͽ�  / 2. ���� �ͽ�
    [SerializeField] public List<GameObject> Pattern = new List<GameObject>(); // 0. �Ϲ� ���� / 2. ���ͽ� / 2. ���� �ͽ� 1 / 
    [SerializeField] Image Pattern_Image;
    [SerializeField] int _usePattern;



    [Range(0f, 100f)]
    [SerializeField] public float Pattern_CoolTime;

    [SerializeField] public int Pattern_Index;

     public void Random_Pattern() 
    {
        Debug.Log("���� ����!");
        Pattern_Index = Random.Range(0, 5);
            int i= Pattern_Index;
        while (i == Pattern_Index)
        {
            Pattern_Index = Random.Range(0, 5);
            Debug.Log("���� ��ȣ �̴� ��");
        }

        Pattern_Image.sprite = Pattern_Sprite[Pattern_Index];
        Debug.Log("���� ����!1");
        Pattern[Pattern_Index].SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coll")
        {
            Debug.Log("����");
            Pattern[Pattern_Index].transform.position = new Vector3(0, 0, 0);
            Pattern[Pattern_Index].SetActive(false);
            Random_Pattern();
            Debug.Log("����2");
        }
    }
    //============���� ����=================

    [SerializeField] public int P_HP = 3;

    [Header ("���� ���� �г�")]
    [SerializeField] GameObject GameOver_Panel;
    [SerializeField] Text GameOver_Score;
    [SerializeField] Text GameOver_Highest_Score;
    //���ھ� �ؽ�Ʈ ����ٰ� ����.

    [SerializeField]
    Text Score_Text;
    public int Score = 0;

    [SerializeField] public GameObject[] HP_UI = new GameObject[3];

    // ����  ( �� )
    [SerializeField] 
    public Transform[] Line = new Transform[4];

    //���� ������
    [SerializeField]
    public GameObject[] Monster = new GameObject[5];

    // �÷��̾� 
    [Header ("�÷��̾�")]
    [SerializeField] GameObject P;
    [SerializeField] Animator P_Anim;
    [SerializeField] BoxCollider2D P_Collider;

    //�÷��̾� ���� �ε��� ��ȣ

     [Header (" ���� �ε��� ��ȣ")]
     [SerializeField] int P_LineIndex = 1;
     [SerializeField] int M_LineIndex;
     [SerializeField] int MonsterIndex;
     [SerializeField] int MonsterIndex2;
     [SerializeField] int TwoLine;

     [Header ("���� ���� �ð� ( ���� ��� ���� )")]
     [SerializeField] float MonsterSpawnTime = 3;
    //�÷��̾� ���� ����
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

    //���� ���� �ڷ�ƾ
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
        if (IsCandy == false)
        {
            Debug.Log("�۵���");
            ChangeLine();
        }
        else
        {
            Debug.Log("�� �۵���");
        }
    
        SetHP();
        P.transform.localEulerAngles = new Vector3(0,0,0);
    }
   public void Player_Hit() 
    {
        P_HP--;
        StartCoroutine(P_invincibility());
    }
    [Header ("�÷��̾� ���� �ð�")]  [Range (0,5f)]
    public float P_invincibility_time;
    IEnumerator P_invincibility() 
    {
        Debug.Log("����");
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
    [Header("Candy")]
    [SerializeField] float Candy_Hit_time = 0.7f;
    bool IsCandy;
    IEnumerator CandyGhost_Hit() 
    {
        Debug.Log("IsCandy Ȱ��ȭ");
        IsCandy = true;
        yield return new WaitForSecondsRealtime(Candy_Hit_time);
        Debug.Log("IsCandy �� Ȱ��ȭ");
        IsCandy = false;
    }
   public void StartCandy() 
    {
        Debug.Log("�ҷȴ�.");
        StartCoroutine(CandyGhost_Hit());
    }
    [SerializeField] List<GameObject> Hit_Image = new List<GameObject>();
}
