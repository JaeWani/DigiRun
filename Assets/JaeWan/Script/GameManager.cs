using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //스코어 텍스트 만들다가 껐다.

    [SerializeField]
    TextMeshProUGUI Score_Text;

    // 라인  ( 줄 )
    [SerializeField] 
    public Transform[] Line = new Transform[4];

    //몬스터 프리펩
    [SerializeField]
    public GameObject[] Monster = new GameObject[4];

    // 플레이어 
    [SerializeField]
    GameObject P;

    //플레이어 라인 인덱스 번호
     [SerializeField] int P_LineIndex = 1;
     [SerializeField] int M_LineIndex;
     [SerializeField] int MonsterIndex;
     [SerializeField] int MonsterIndex2;
     [SerializeField] int TwoLine;

     [SerializeField] float MonsterSpawnTime = 3;
    private void Start()
    {
        Debug.Log(P_LineIndex);
        StartCoroutine(SpawnMonster());
    }
    //플레이어 라인 변경
    void ChangeLine()
    {
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

    //몬스터 스폰 코루틴
    IEnumerator SpawnMonster()
    {
        M_LineIndex = Random.Range(0, 4);
        MonsterIndex = Random.Range(0, 4);
        MonsterIndex2 = Random.Range(0,4);
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
    private void Awake()
    { 
        
    }
    private void Update()
    {
        ChangeLine();
    }

}
