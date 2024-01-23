using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EnemyContorl : Base
{

    public int level = 1;// 1 ~ 3 级
    public int color = 1;//1 白色  2红色  3 黄色  4绿色(level 3  才有黄绿)
    public int speed = 2;
    public float stateTime = 3;
    public float fireTime = 2;

    [SyncVar(hook = nameof(ChangeMoveState))]
    private bool isMove = false;
    private int dir = 0;
    private Animator animator;
    private Fire fire;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        fire = GetComponent<Fire>();
    }

    public override void OnStartServer()
    {
        InvokeRepeating("CheckState", 0, stateTime);
        InvokeRepeating("OpenFire", fireTime, fireTime);
        level = Random.Range(1, 4);
        animator.SetInteger("Level", level);
        if (level == 3)
        {
            animator.SetInteger("Color", Random.Range(3, 5));
        }
        else
        {
            animator.SetInteger("Color", Random.Range(1, 3));
        }

        // Debug.Log("EnemyContorl =======>" + isLocalPlayer);
    }
    void Update()
    {
        if (isMove)
        {
            animator.SetFloat("Speed", 1);
            Move();
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    [ServerCallback]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("MapObj"))
        {
            ChangeDir();
        }
        if (other.tag.Contains("Enemy"))
        {
            ChangeDir();
        }
        // if (other.tag.Contains("Player"))
        // {
        //     ChangeDir();
        // }
    }
    void CheckState()
    {
        isMove = Random.Range(0, 100) > 50;
        dir = Random.Range(1, 5);
    }
    private void Move()
    {
        switch (dir)
        {
            case 1:
                transform.position = new Vector3(transform.position.x + Time.deltaTime * speed, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, -90);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x - Time.deltaTime * speed, transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 4:
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed, transform.position.z);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
        }
    }
    private void ChangeDir()
    {
        if (!Utilities.IsInView(transform.position))
        {
            return;
        }
        switch (dir)
        {
            case 1:
                dir = Random.Range(2, 5);
                break;
            case 2:
                dir = Random.Range(1, 4);
                if (dir == 2)
                {
                    dir = 1;
                }
                break;
            case 3:
                dir = Random.Range(1, 4);
                if (dir == 3)
                {
                    dir = 4;
                }
                break;
            case 4:
                dir = Random.Range(1, 4);
                break;
        }
    }
    private void OpenFire()
    {
        // Debug.Log("EnemyContorl =======>" + isLocalPlayer);
        if (!Utilities.IsInView(transform.position))
        {
            return;
        }
        if (Random.Range(0, 100) > 50)
        {
            fire.CreateBullect(BullctType.Enemy);
        };
    }
    private void ChangeMoveState(bool oldValue, bool newValue)
    {
        isMove = newValue;
    }
}
