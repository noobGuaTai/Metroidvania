using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{
    int HP = 10;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void ChangeHP(int amount)
    {
        HP += amount;
        Debug.Log("敌人当前HP: " + HP);
    }
}
