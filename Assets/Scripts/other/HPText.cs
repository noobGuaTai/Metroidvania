using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPText : MonoBehaviour
{
    public PlayerAttribute pa;
    int HP = 100;
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        if (pa != null && text != null)
        {
            if(pa.HP < 100)
            {
                text.text = " " + pa.HP.ToString();
            }
            else
            {
                text.text = pa.HP.ToString();
            }
            
        }
    }
}
