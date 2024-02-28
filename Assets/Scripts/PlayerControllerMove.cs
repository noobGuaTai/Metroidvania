using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GroundSensorNamespace;
using Unity.VisualScripting;

namespace PlayerControllerMoveNamespace
{
    public class PlayerControllerMove : MonoBehaviour
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
        public GroundSensor gs;
        public int jumpNum = 1;//跳跃段数 第一次跳跃不减段数，因此设为1
        private bool isLongJumping = false;
        private Coroutine checkJumpReleaseCoroutine;

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
            CheckGround(gs);
        }

        void FixedUpdate()
        {
        
            MoveX();
            Attack();
        }

        void MoveX()
        {
        
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
            if(moveX != 0)
            {
                transform.localScale = new Vector3(moveX * 3, 3, 1);
                anim.SetBool("run", true);
            }
            else
            {
                anim.SetBool("run", false);
            }
        
        }

        void Jump()
        {
            if(jumping && jumpNum > 0)
            {
                rb.velocity = Vector2.up * jumpSpeed;
                //isOnGround = false;
                jumpNum--;
                //anim.SetBool("jump", true);
                // 检测跳跃释放
                if (checkJumpReleaseCoroutine != null)
                {
                    StopCoroutine(checkJumpReleaseCoroutine);
                }
                checkJumpReleaseCoroutine = StartCoroutine(CheckJumpRelease());

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
            if(Input.GetButtonDown("Attack"))
            {
                anim.SetBool("attack", true);
            }
            else
            {
                anim.SetBool("attack", false);
            }
        }

        // void OnCollisionEnter2D(Collision2D other)
        // {
        //     if(other.contacts[0].normal == Vector2.up)
        //     {
        //         Debug.Log("OnFloor");
        //         isOnGround = true;
        //         jumpNum = 2;
        //         anim.SetBool("jump", false);
            
        //     }
        // }

        void CheckGround(GroundSensor gs)
        {
            // isOnGround = gs.OnGround;
            // isOnOneWay = gs.OnOneWay;
            if(gs.OnGround)
            {
                jumpNum = 1;
                anim.SetBool("jump", false);
            }
        }
    }
}
