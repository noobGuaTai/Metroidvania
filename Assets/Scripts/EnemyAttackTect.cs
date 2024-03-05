using System.Collections;
using System.Collections.Generic;
using PlayerMoveControllerNamespace;
using UnityEngine;
using TMPro;

public class EnemyAttackTect : MonoBehaviour
{
    EnemyAttribute ea;
    new Collider2D collider2D;
    public GameObject damageTextPrefab;
    public Canvas canvas; // 引用你的UI Canvas
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
                    ShowDamageText(collider.transform.position, pa.strike);//展示伤害数字
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

    void ShowDamageText(Vector3 worldPosition, int damage)
    {
        // 将世界坐标转换为屏幕坐标
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // 实例化伤害文本预制件
        GameObject damageTextObj = Instantiate(damageTextPrefab, canvas.transform);

        // 将屏幕坐标转换为Canvas的局部坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPosition, canvas.worldCamera, out Vector2 localPoint);

        // 设置伤害文本的位置
        damageTextObj.GetComponent<RectTransform>().localPosition = new Vector3(localPoint.x + 50f, localPoint.y, 0);

        // 设置伤害数值
        damageTextObj.GetComponent<TextMeshProUGUI>().text = damage.ToString(); // 如果你使用TextMeshPro

    }
}
