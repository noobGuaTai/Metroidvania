using System.Collections;
using System.Collections.Generic;
using PlayerMoveControllerNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackTect : MonoBehaviour
{
    CapsuleCollider2D attackTect;
    public PlayerMoveController pmc;
    PlayerAttribute pa;

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
        if(pmc.isAttacking == true)
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
            }
        }
    }

}
