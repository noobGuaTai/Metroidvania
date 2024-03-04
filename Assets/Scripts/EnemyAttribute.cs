using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{
    int HP = 100;
    public int strike = 10;
    Animator anim;
    EnemyAttackTect eat;
    EnemyCheckPlayer ecp;
    void Start()
    {
        anim = GetComponent<Animator>();
        eat = GetComponentInChildren<EnemyAttackTect>();
        ecp = GetComponent<EnemyCheckPlayer>();
        
    }

    
    void Update()
    {
        Debug.Log(ecp.isDied);
    }

    public void ChangeHP(int amount)
    {
        HP += amount;
        Debug.Log("敌人当前HP: " + HP);
        if(HP <= 0)
        {
            StartCoroutine(Die());
        }
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
