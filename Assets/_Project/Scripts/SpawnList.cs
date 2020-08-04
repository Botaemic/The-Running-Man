using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpawnList : ScriptableObject
{
    [SerializeField] private List<GameObject> _spawnList = new List<GameObject>();

    public GameObject GetRandomSpawn()
    {
        return _spawnList.Count > 0 ? _spawnList[Random.Range(0, _spawnList.Count)] : null;
    }
}
