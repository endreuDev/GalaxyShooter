using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyShipPrefab;
    [SerializeField] private GameObject[] powerUps;

    [SerializeField] private float _minEnemySpawnRate;
    [SerializeField] private float _maxEnemySpawnRate;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    public IEnumerator SpawnEnemyRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            float enemySpawnRate = Random.Range(_minEnemySpawnRate, _maxEnemySpawnRate);
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7.78f, 7.78f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    public IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerup], new Vector3(Random.Range(-7.78f, 7.78f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
}
