using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    public float patrolSpeed = 1f; // 巡逻速度
    public float chaseSpeed = 1f; // 追逐速度
    public float patrolDistance = 5f; // 巡逻距离
    public float detectionDistance = 2f; // 检测玩家的距离
    public LayerMask playerLayer; // 玩家层，用于Raycast检测玩家
    public LayerMask wallLayer; // 定义一个墙体层，用于射线检测
    private float movingRight = -1f; // 判断是否向右移动 -1为向左
    private float leftBound; // 左边界
    private float rightBound; // 右边界
    private Transform playerTransform; // 玩家的Transform
    private float lastFlipTime = -1; // 记录上次转向的时间
    private float flipCooldown = 1f; // 转向的冷却时间，这里设置为1秒
    float startTime;//开始巡逻时间
    public bool isDied = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftBound = transform.position.x - patrolDistance;
        rightBound = transform.position.x + patrolDistance;
        startTime = Time.time + 1f; // 在当前时间基础上增加1秒的延迟
    }

    void Update()
    {
        //CheckForPlayer();

        if (playerTransform != null)
        {
            //ChasePlayer();
        }
        else
        {
            if(Time.time > startTime && !isDied)
            {
                Patrol();
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
        }
    }

    void CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(movingRight, 0), detectionDistance, playerLayer);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            playerTransform = hit.collider.transform;
        }
        else
        {
            Debug.Log("nohit");
            playerTransform = null;
        }
    }

    void Patrol()
    {
        rb.velocity = new Vector2(movingRight * patrolSpeed, rb.velocity.y);

        // 检查是否已经到达边界并且是否已经过了冷却时间
        if ((transform.position.x <= leftBound || transform.position.x >= rightBound) && (Time.time - lastFlipTime > flipCooldown))
        {
            transform.localScale = new Vector3(movingRight * 2, 2, 1); // 转向
            movingRight = -movingRight; // 改变方向
            lastFlipTime = Time.time; // 更新上次转向时间
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞对象是否是墙体
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) // 假设您的墙体在名为"Wall"的层
        {
            transform.localScale = new Vector3(movingRight * 2, 2, 1);//转向
            movingRight = -movingRight;
        }
    }

    void ChasePlayer()
    {
        if (playerTransform != null)
        {
            // 更新速度以朝向玩家
            rb.velocity = new Vector2(chaseSpeed, rb.velocity.y);
            
        }
    }
}
