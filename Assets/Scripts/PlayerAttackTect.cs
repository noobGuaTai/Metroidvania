using System.Collections;
using System.Collections.Generic;
using PlayerMoveControllerNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackTect : MonoBehaviour
{
    CapsuleCollider2D attackTect;
    public PlayerMoveController pmc;
    PlayerAttribute pa;
    public GameObject damageTextPrefab;
    public Canvas canvas; // 引用你的UI Canvas

    void Start()
    {
        attackTect = GetComponent<CapsuleCollider2D>();
        pa = GetComponentInParent<PlayerAttribute>();
    }


    void Update()
    {
        StartAttack(pmc);
    }

    void StartAttack(PlayerMoveController pmc)
    {
        if (pmc.isAttacking == true)
        {
            attackTect.enabled = true;
        }
        else
        {
            attackTect.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAttribute ea = collider.gameObject.GetComponent<EnemyAttribute>();
            if (ea != null)
            {
                ea.ChangeHP(-pa.strike);
                ShowDamageText(collider.transform.position, pa.strike);
            }
        }
    }

    public void ShowDamageText(Vector3 worldPosition, int damage)
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
