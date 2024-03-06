using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public List<SpawnPointConfig> spawnConfigs = new List<SpawnPointConfig>(); // 包含所有生成点配置的列表
    public GameObject enemyPrefab; // 敌人Prefab

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 确保是玩家触发了这个事件
        {
            foreach (var config in spawnConfigs)
            {
                SpawnEnemy(config);
            }
        }
    }

    void SpawnEnemy(SpawnPointConfig config)
    {
        if (enemyPrefab != null && config.spawnPoint != null)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, config.spawnPoint.position, Quaternion.identity);
            EnemyCheckPlayer ecp = newEnemy.GetComponent<EnemyCheckPlayer>();

            if (ecp != null)
            {
                ecp.patrolDistance = config.patrolDistance;
                // 根据config配置敌人
                
            }
        }
        gameObject.SetActive(false); // 生成后禁用这个生成点
    }
}
