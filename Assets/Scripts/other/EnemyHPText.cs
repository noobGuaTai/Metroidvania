using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPText : MonoBehaviour
{
    public GameObject Player;
    PlayerAttackTect pat;
    GameObject currentEnemy;
    float lastTime;
    int lastHP;
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
        GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        GetComponentInChildren<TextMeshProUGUI>().text = ea.HP.ToString();
        if(Time.time - ea.lastChangeHPTime >= 2f)
        {
            GetComponent<Image>().enabled = false;
            GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
    }
}
