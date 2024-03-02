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
        public float dashSpeed = 12f; // 冲刺速度
        public float dashTime = 0.15f; // 冲刺持续时间
        float moveX;//左右移动输入
        bool jumping;//跳跃输入
        bool isOnGround = false;//判断在地上
        public bool isDashing = false;//判断是否在冲刺
        // bool isOnOneWay= false;//判断在平台上
        public PlayerCollisionController pcc;
        public int jumpNum = 2;//跳跃段数
        private bool isLongJumping = false;
        private Coroutine checkJumpReleaseCoroutine;
        private float jumpCooldown = 0.2f; // 跳跃后的冷却时间，可以根据需要调整
        private float lastJumpTime; // 上次跳跃的时间
        public bool isAttacking = false;
        Vector2 moveDirection = Vector2.right;//移动方向

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
            if (moveX > 0)
            {
                moveDirection = Vector2.right;//获取移动方向
            }
            if (moveX < 0)
            {
                moveDirection = Vector2.left;//获取移动方向
            }
            Jump();
            CheckGround(pcc);
            StartDash();
        }

        void FixedUpdate()
        {
            
            Attack();
            MoveX();
            
        }

        void MoveX()
        {
            if (moveX != 0 && !isDashing)
            {
                if (!isAttacking)
                {
                    rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
                    transform.localScale = new Vector3(moveX * 3, 3, 1);
                    anim.SetBool("run", true);
                }
            }
            else
            {
                if(!isDashing)
                {
                    // 当没有输入时，停止水平移动
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    anim.SetBool("run", false);
                }
            }

        }

        void Jump()
        {
            if (jumping && jumpNum > 0 && !isAttacking)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                //isOnGround = false;
                jumpNum--;
                anim.SetBool("jump", true);
                isOnGround = false;

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
            if (Input.GetButton("Attack") && !isAttacking && isOnGround)
            {
                if(!isDashing)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);//如果在移动，则停止移动（除非在冲刺）
                }
                
                anim.SetBool("attack", true);
                isAttacking = true;
                Invoke("ResetAttack", 0.6f); // 攻击动画长度为0.6秒
            }
        }

        void ResetAttack()
        {
            isAttacking = false;
            anim.SetBool("attack", false);
        }

        void StartDash()
        {
            if (Input.GetButtonDown("Dash") && !isDashing)
            {
                StartCoroutine(Dash());
            }

        }

        IEnumerator Dash()
        {
            isDashing = true; // 标记为正在冲刺
            anim.SetBool("dash", true);
            rb.velocity = moveDirection * dashSpeed;
            rb.gravityScale = 0; // 禁用重力
            yield return new WaitForSeconds(dashTime); // 冲刺持续时间
            isDashing = false; // 冲刺结束
            rb.gravityScale = 2; // 恢复重力
            rb.velocity = Vector2.zero; // 冲刺后停止所有移动
            anim.SetBool("dash", false);
        }

        void CheckGround(PlayerCollisionController pcc)
        {
            if (pcc.OnGround && Time.time - lastJumpTime > jumpCooldown)
            {
                jumpNum = 2;
                anim.SetBool("jump", false);
                isOnGround = true;
            }
        }
    }
}
