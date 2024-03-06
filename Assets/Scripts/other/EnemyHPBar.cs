using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    public GameObject Player;
    PlayerAttackTect pat;
    GameObject currentEnemy;
    float lastTime;
    int lastHP;
    Image enemyHealthBar;
    EnemyAttribute ea;
    void Start()
    {
        pat = Player.GetComponentInChildren<PlayerAttackTect>();
        
    }


    void Update()
    {
        if(pat.currentEnemy != null)
        {
            currentEnemy = pat.currentEnemy;
            ea = currentEnemy.GetComponent<EnemyAttribute>();
            Show();
        }
    }

    void Show()
    {
        GetComponent<Image>().enabled = true;
        enemyHealthBar = GameObject.FindGameObjectWithTag("EnemyBar2").GetComponent<Image>();
        enemyHealthBar.enabled = true;
        enemyHealthBar.fillAmount = ea.HP / 100f;
        if(Time.time - ea.lastChangeHPTime >= 2f)
        {
            GetComponent<Image>().enabled = false;
            GameObject.FindGameObjectWithTag("EnemyBar2").GetComponent<Image>().enabled = false;
        }
    }

}
