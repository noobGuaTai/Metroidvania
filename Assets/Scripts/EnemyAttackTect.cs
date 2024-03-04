using System.Collections;
using System.Collections.Generic;
using PlayerMoveControllerNamespace;
using UnityEngine;

public class EnemyAttackTect : MonoBehaviour
{
    EnemyAttribute ea;
    new Collider2D collider2D;
    void Start()
    {
        ea = GetComponentInParent<EnemyAttribute>();
        collider2D = GetComponent<Collider2D>();
    }


    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerAttribute pa = collider.gameObject.GetComponent<PlayerAttribute>();
            PlayerMoveController pmc = collider.gameObject.GetComponent<PlayerMoveController>();
            if (pa != null && pmc != null)
            {
                pa.ChangeHP(-ea.strike);

                // 获取玩家的Rigidbody2D组件
                Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = Vector2.zero; // 将玩家的速度重置为0，防止玩家在受到攻击后继续移动
                    // 设置推力大小和方向
                    Vector2 forceDirection = (collider.transform.position - transform.position).normalized;
                    float forceMagnitude = 2f; // 根据需要调整这个力的大小
                    // 对玩家施加力，实现击退效果
                    rb.AddForce(forceDirection * forceMagnitude, ForceMode2D.Impulse);
                    pmc.isHurting = true;
                    pmc.anim.SetBool("hurt", true);
                    collider2D.enabled = false; // 禁用碰撞器，防止玩家再次受击
                    StartCoroutine(Hurt(pmc));
                }
            }
        }
    }

    IEnumerator Hurt(PlayerMoveController pmc)
    {
        yield return new WaitForSeconds(0.3f);
        pmc.isHurting = false;
        pmc.anim.SetBool("hurt", false);
        yield return new WaitForSeconds(0.7f);
        collider2D.enabled = true; // 1秒后玩家才可再次受击
    }
}
