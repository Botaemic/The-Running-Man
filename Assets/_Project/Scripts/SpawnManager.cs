using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private SpawnList _spawnlist = null;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private Transform _spawnLocation = null;

    [Header("Obstacles")]
    [SerializeField] private SpawnList _pickupSpawnList = null;
    [SerializeField] private float _pickupSpawnInterval = 2f;
    [SerializeField] private Transform[] _pickupSpawnLocation = null;


    private float _timer = 0;
    private float _energyTimer = 0;
    void Start()
    {
        _timer = _spawnInterval;
        _energyTimer = _pickupSpawnInterval;
    }

    void Update()
    {
        if (LevelManager.Instance.IsGameRunning)
        {
            _timer -= (Time.deltaTime * LevelManager.Instance.GameSpeed);
            if (_timer < Mathf.Epsilon)
            {
                var spawn = _spawnlist.GetRandomSpawn();
                Instantiate(spawn, _spawnLocation.position, spawn.transform.rotation);
                _timer = _spawnInterval;
            }


            _energyTimer -= (Time.deltaTime * LevelManager.Instance.GameSpeed);
            if (_energyTimer < Mathf.Epsilon)
            {
                var spawn = _pickupSpawnList.GetRandomSpawn();
                var location = _pickupSpawnLocation[Random.Range(0, _pickupSpawnLocation.Length)];
                Instantiate(spawn, location.position, spawn.transform.rotation);
                _energyTimer = _pickupSpawnInterval;
            }

        }
    }

}