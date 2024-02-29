using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCollisionControllerNamespace;
using Unity.VisualScripting;

namespace PlayerMoveControllerNamespace
{
    public class PlayerMoveController : MonoBehaviour
    {
        Animator anim;
        new SpriteRenderer renderer;
        Rigidbody2D rb;
        public float moveSpeed = 5f;
        public float jumpSpeed = 8f;
        float moveX;//左右移动输入
        bool jumping;//跳跃输入
        // bool isOnGround = false;//判断在地上
        // bool isOnOneWay= false;//判断在平台上
        public PlayerCollisionController pcc;
        public int jumpNum = 2;//跳跃段数
        private bool isLongJumping = false;
        private Coroutine checkJumpReleaseCoroutine;
        private float jumpCooldown = 0.2f; // 跳跃后的冷却时间，可以根据需要调整
        private float lastJumpTime; // 上次跳跃的时间
        bool isAttacking = false;

        void Start()
        {
            anim = GetComponent<Animator>();
            renderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            jumping = Input.GetButtonDown("Jump");
            Jump();
            CheckGround(pcc);
            
        }

        void FixedUpdate()
        {
            Attack();
            MoveX();
           
        }

        void MoveX()
        {
            if (moveX != 0)
            {
                if(!isAttacking)
                {
                    rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
                    transform.localScale = new Vector3(moveX * 3, 3, 1);
                    anim.SetBool("run", true);
                }       
            }
            else
            {
                // 当没有输入时，停止水平移动
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetBool("run", false);
            }

        }

        void Jump()
        {
            if (jumping && jumpNum > 0)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                //isOnGround = false;
                jumpNum--;
                anim.SetBool("jump", true);
                // 检测跳跃释放
                if (checkJumpReleaseCoroutine != null)
                {
                    StopCoroutine(checkJumpReleaseCoroutine);
                }
                checkJumpReleaseCoroutine = StartCoroutine(CheckJumpRelease());
                lastJumpTime = Time.time;
            }
        }

        IEnumerator CheckJumpRelease()
        {
            yield return null; // 等待下一帧，确保长跳逻辑开启
            isLongJumping = true;

            yield return new WaitForSeconds(0.135f);
            if (!Input.GetButton("Jump")) // 如果在0.135秒内松开按键
            {
                // 施加一个向下的力来模拟角色被拍回地面
                rb.AddForce(Vector2.down * jumpSpeed / 2, ForceMode2D.Impulse);
            }
            isLongJumping = false;
        }

        void Attack()
        {
            if (Input.GetButton("Attack") && !isAttacking)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);//如果在移动，则停止移动
                anim.SetBool("attack", true);
                isAttacking = true;
                Invoke("ResetAttack", 0.6f); // 假设攻击动画长度为1秒
            }
        }

        void ResetAttack()
        {
            isAttacking = false;
            anim.SetBool("attack", false);
        }

        void CheckGround(PlayerCollisionController pcc)
        {
            if (pcc.OnGround && Time.time - lastJumpTime > jumpCooldown)
            {
                jumpNum = 2;
                anim.SetBool("jump", false);
            }
        }
    }
}
