using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using PlayerMoveControllerNamespace;

namespace PlayerCollisionControllerNamespace
{
    public class PlayerCollisionController : MonoBehaviour
    {
        [SerializeField] int collisionType; //碰撞类型 0为椭圆，1为box，2为圆
        Vector2 colliderSizeOrigin; //碰撞体原始大小
        CapsuleCollider2D capsule;
        BoxCollider2D box;
        CircleCollider2D circle;
        Rigidbody2D body;
        [SerializeField] bool onGround;//是否在地上
        [SerializeField] bool onOneWay;//是否在平台上
        LayerMask solidMask = 1 << 7;//1是开0是关，7是碰撞图层代码
        LayerMask oneWayMask = 1 << 8;
        LayerMask layerMask = 1 << 7 | 1 << 8;

        public bool OnGround { get => onGround; set => onGround = value; }
        public bool OnOneWay { get => onOneWay; set => onOneWay = value; }

        void Awake()
        {
            body = transform.parent.GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            switch(collisionType) 
            {
                case 0: capsule = GetComponent<CapsuleCollider2D>(); colliderSizeOrigin = capsule.size; break;
                case 1: box = GetComponent<BoxCollider2D>(); colliderSizeOrigin = box.size; break;
                case 2: circle = GetComponent<CircleCollider2D>(); colliderSizeOrigin = circle.radius * Vector2.one; break;
            }
        }

        //碰撞时执行
        void OnCollisionEnter2D(Collision2D collision)
        {
            int nLayer = collision.gameObject.layer;
            if(nLayer == 7 || nLayer == 8)
            {
                if(body.velocity.y <= 0.01f)
                {
                    CheckCollision(collision, nLayer);
                }
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            int nLayer = collision.gameObject.layer;
            if(nLayer == 7 || nLayer == 8)
            {
                CheckCollision(collision, nLayer);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            int nLayer = collision.gameObject.layer;
            if(nLayer == 7 || nLayer == 8)
            {
                onGround = false;
                onOneWay = false;
            }
        }

        //判断碰撞
        void CheckCollision(Collision2D collision, int layerNum)
        {
            for(int i = 0; i < collision.contactCount; i++)
            {
                switch(layerNum)
                {
                    case 7: onGround |= collision.GetContact(i).normal.y >= 0.35f; break;//物体下方受到碰撞，则判定在地板上
                    case 8: onOneWay |= collision.GetContact(i).normal.y >= 0.35f; break;//物体下方受到碰撞，则判定在平台上
                }
            }
        }




        void Update()
        {
        
        }
    }
}
