using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnLocation;
    [SerializeField]
    private List<Enemy> _enemyList = new List<Enemy>();
    private List<GameObject> _enemiesToSpawn = new List<GameObject>();
    private int _currentWave = 1;
    private int _waveValue;
    private int _waveDuration = 20;
    private float _waveTimer;
    private float _spawnInterval;
    private float _spawnTimer;


    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_spawnTimer <= 0)
        {
            if (_enemiesToSpawn.Count > 0)
            {
                Instantiate(_enemiesToSpawn[0], _spawnLocation.position, Quaternion.identity);
                _enemiesToSpawn.RemoveAt(0);
                _spawnTimer = _spawnInterval;

            } else
            {
                _waveTimer = 0;
            }
        }
        else
        {
            _spawnTimer -= Time.fixedDeltaTime;
            _waveTimer -= Time.fixedDeltaTime;

        }
        if (_waveTimer <= 0)
        {
            _currentWave++;
            GenerateWave();
        }

    }

    void GenerateWave()
    {
        _waveTimer = _waveDuration;
        _waveValue = _currentWave * 10;
        GenerateEnemies();
        _spawnInterval = _waveDuration / _enemiesToSpawn.Count;
        _spawnTimer = _spawnInterval;

    }

    void GenerateEnemies()
    {
        _enemiesToSpawn.Clear();

        while(_waveValue > 0)
        {
            int randomEnemyId = Random.Range(0, _enemyList.Count);
            int randomEnemyCost = _enemyList[randomEnemyId].cost;

            if(_waveValue - randomEnemyCost >= 0)
            {
                _enemiesToSpawn.Add(_enemyList[randomEnemyId].enemyPrefab);
                _waveValue -= randomEnemyCost;
            }
        }

    }   
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
