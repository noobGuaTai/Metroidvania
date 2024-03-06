using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    bool nextLevel = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
        {
            // // 没有敌人时，确保该对象是激活的
            // gameObject.SetActive(true);
            nextLevel = true;
        }
        Debug.Log(enemies.Length);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && nextLevel == true)
        {
            SceneManager.LoadScene("Secend");
        }
    }

}
