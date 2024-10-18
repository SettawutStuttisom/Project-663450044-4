using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ศัตรูที่ต้องการสร้าง
    public Vector3 spawnPosition; // ตำแหน่งสำหรับการสร้างศัตรู
    public float spawnInterval = 2f; // ระยะเวลาการสร้างศัตรู

    private void Start()
    {
        // เรียกใช้ฟังก์ชัน SpawnEnemy ทุกๆ spawnInterval วินาที
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        // สร้างศัตรูที่ตำแหน่งที่กำหนดใน spawnPosition
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}