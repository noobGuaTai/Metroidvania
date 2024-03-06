using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 使其在Unity编辑器中可见
public class SpawnPointConfig
{
    public Transform spawnPoint; // 敌人生成点
    public float patrolDistance;
}

