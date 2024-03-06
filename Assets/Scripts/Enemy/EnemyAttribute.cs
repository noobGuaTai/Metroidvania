using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttribute : MonoBehaviour
{
    public int HP = 100;
    public int strike = 10;
    Animator anim;
    EnemyAttackTect eat;
    EnemyCheckPlayer ecp;
    Image enemyHealthBar; // 血条Image组件
    TextMeshProUGUI enemyHealthText; // 血量Text组件
    Image enemyHealthBarOutline;
    Image enemyHealthTextOutline;
    public float lastChangeHPTime;
    void Start()
    {
        anim = GetComponent<Animator>();
        eat = GetComponentInChildren<EnemyAttackTect>();
        ecp = GetComponent<EnemyCheckPlayer>();

    }


    void Update()
    {
    }

    public void ChangeHP(int amount)
    {
        HP += amount;
        Debug.Log("敌人当前HP: " + HP);
        if (HP <= 0)
        {
            StartCoroutine(Die());
        }
        lastChangeHPTime = Time.time;
    }

    IEnumerator Die()
    {
        ecp.isDied = true;
        anim.SetBool("death", true);
        eat.enabled = false;
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }


}
