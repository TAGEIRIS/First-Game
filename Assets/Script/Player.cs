using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float speed = 5f;//�ٶ�
    public Transform PlayerVisual;
    public float hp= 15f;
    public bool isDead=false;//�Ƿ�����
    public float maxHp = 15f;//�������ֵ



    private void Awake()
    {
        Instance = this;
        PlayerVisual = GameObject.Find("PlayerVisual").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;

        Move();
    }


    //wasd �ƶ�
    public void Move()
    {
        float moveHcrizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector2 moveMent=new Vector2(x:moveHcrizontal, y:moveVertical);

        moveMent.Normalize();
        transform.Translate(translation:(Vector3)(moveMent*speed*Time.deltaTime));

        TurnAround(moveHcrizontal);
    }

    //תͷ
    public void TurnAround(float h)
    {
        if (h == -1)
        {
            PlayerVisual.localScale = new Vector3(x: -1, PlayerVisual.localScale.y, PlayerVisual.localScale.z);
        }
        else if (h == 1)
        {
            PlayerVisual.localScale = new Vector3(x: 1, PlayerVisual.localScale.y, PlayerVisual.localScale.z);
        }
    }

    //����
    public void Injured(float attack)
    {
        if (isDead) return;

        //�жϱ��ι����Ƿ������
        if (hp - attack <= 0)
        {
            hp = 0;
            Dead();
        }
        else
        {
            hp -= attack;
        }
        GamePanel.instance.RenewHp();
    }

    //����
    public void Attack()
    {

    }


    //����
    public void Dead()
    {
        isDead = true;

        LevelController.Instance.BadGame();

    }


}
