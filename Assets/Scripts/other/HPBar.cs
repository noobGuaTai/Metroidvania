using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Image image;
    public PlayerAttribute pa;
    int HP = 100;
    void Start()
    {
        image = GetComponent<Image>();
    }


    void Update()
    {
        if (pa != null)
        {
            HP = pa.HP;
        }
        if (image != null)
        {
            SetHealth(HP, 100);
        }
        
    }

    public void SetHealth(float health, float maxHealth)
    {
        image.fillAmount = health / maxHealth;
    }
}
