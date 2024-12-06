using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public float waveTimer;
    public GameObject _failPanel;
    public GameObject _successPanel;
    
    public int currenWave = 1;//��ǰ����
    public GameObject enemyG;//����Ԥ����
    public GameObject enemyR;
    public GameObject enemyB;
    public GameObject enemyBoss;
    public List<EnemyBase> enemy_List;//�����б�
    public Transform _map;


    private void Awake()
    {
        Instance=this;

        _failPanel = GameObject.Find("FailPanel");
        _successPanel = GameObject.Find("SuccessPanel");
    }
    void Start()
    {
        waveTimer = 24;


        //���ɵ���
        GenerateEnemy();
    }
    public void GenerateEnemy()
    {
        StartCoroutine(SwawnEnemy1());
        StartCoroutine(SwawnEnemy2());
        StartCoroutine(SwawnEnemy3());
        StartCoroutine(SwawnEnemy4());
    }

    //���λ��
    private Vector3 GetRandomPosition(Bounds bounds)
    {
        float safeDistance = 1f;
        float randomX = Random.Range(bounds.min.x + safeDistance, bounds.max.x + safeDistance);
        float randomY = Random.Range(bounds.min.y + safeDistance, bounds.max.y + safeDistance);
        float randomZ = 0f;
        return new Vector3(randomX,randomY,randomZ);
    }
    //������������
    IEnumerator SwawnEnemy1()
    {
        while (waveTimer>0&&Player.Instance.hp>0)
        {
            yield return new WaitForSeconds(0.5f);

            var spawnPoint = GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);

            EnemyBase go =Instantiate(enemyG,spawnPoint,Quaternion.identity).GetComponent<EnemyBase>();
            enemy_List.Add(go);

        }
    }
    IEnumerator SwawnEnemy2()
    {
        while (waveTimer>0&&Player.Instance.hp>0)
        {
            yield return new WaitForSeconds(1f);

            var spawnPoint = GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);

            EnemyBase go =Instantiate(enemyB,spawnPoint,Quaternion.identity).GetComponent<EnemyBase>();
            enemy_List.Add(go);

        }
    }
    IEnumerator SwawnEnemy3()
    {
        while (waveTimer>0&&Player.Instance.hp>0)
        {
            yield return new WaitForSeconds(3f);

            var spawnPoint = GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);

            EnemyBase go =Instantiate(enemyR,spawnPoint,Quaternion.identity).GetComponent<EnemyBase>();
            enemy_List.Add(go);

        }
    }
    
    IEnumerator SwawnEnemy4()
    {
        while (waveTimer>0&&Player.Instance.hp>0)
        {
            yield return new WaitForSeconds(15f);

            var spawnPoint = GetRandomPosition(_map.GetComponent<SpriteRenderer>().bounds);

            EnemyBase go =Instantiate(enemyBoss,spawnPoint,Quaternion.identity).GetComponent<EnemyBase>();
            enemy_List.Add(go);

        }
    }

    void Update()
    {
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
            if(waveTimer <= 0)
            {
                waveTimer = 0; 

                GoodGame();
            }

        }

        GamePanel.instance.RenewCountDown(waveTimer);

    }

    //��Ϸʤ��
    public void GoodGame()
    {
        _successPanel.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(routine: Gomenu());

        for (int i = 0; i < enemy_List.Count; i++)
        {
            enemy_List[i].Dead();
        }
    }
    //�����

    //��Ϸʧ��
    public void BadGame()
    {
        Debug.Log("badgame");
        _failPanel.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(routine:Gomenu());

        for (int i = 0; i < enemy_List.Count; i++)
        {
            enemy_List[i].Dead();
        }
    }

    IEnumerator Gomenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

}
