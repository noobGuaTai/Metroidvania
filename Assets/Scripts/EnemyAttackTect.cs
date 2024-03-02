using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackTect : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerAttribute pa = collider.gameObject.GetComponent<PlayerAttribute>();
            if (pa != null)
            {
                pa.ChangeHP(-1);
            }
        }
    }
}
