using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowerShooter : MonoBehaviour
{
    [SerializeField] GameObject enemyFollowerPrefab;
    Transform[] enemyFollowerSpawnSpots;
    [SerializeField] [Range(1f,5f)] float spawnTime = 2f;
    [SerializeField] [Range(1f,5f)] float enemySpeed = 1f;
    float timer;

    void Start()
    {
        timer = 0;
        enemyFollowerSpawnSpots = GetComponentsInChildren<Transform>();
    }

    void SpawnEnemyFollower(){
        int randomNumber = Random.Range(0, enemyFollowerSpawnSpots.Length);
        if(enemyFollowerPrefab != null){
            GameObject a = Instantiate(enemyFollowerPrefab, enemyFollowerSpawnSpots[randomNumber].transform.position, Quaternion.identity);
            a.GetComponent<EnemyFollower>().Initialize(enemySpeed);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnTime){
            SpawnEnemyFollower();
            timer = 0;
        }
    }
}
