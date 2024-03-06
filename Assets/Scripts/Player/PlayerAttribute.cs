using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    public int HP = 100;
    public int strike = 10;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeHP(int amount)
    {
        HP += amount;
    }
}
