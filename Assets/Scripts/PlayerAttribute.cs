using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
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
        Debug.Log("当前HP: " + HP);
    }
}
