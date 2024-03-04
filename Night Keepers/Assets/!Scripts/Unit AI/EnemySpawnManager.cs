using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class EnemySpawnManager : Singleton<EnemySpawnManager>
{
    private List<GameObject> _playerBaseList = new List<GameObject>();
    public Vector3 targetPlayerBase;

    private List<Transform> _spawnPointList = new List<Transform>();

    // temp
    [SerializeField] private GameObject _enemyPrefab;

    private WaitForSeconds waitForSeconds;
    [Header("Spawn Time Settings")]
    [SerializeField] private float _spawnDelay = .2f;

    [Header("Spawn Position Settings")]
    [SerializeField] private float _zOffset = 10f;

    // temp
    [Header("Spawn Count Settings")]
    private int _spawnCount = 50;

    private void OnEnable()
    {
        waitForSeconds = new WaitForSeconds(_spawnDelay);
        Debug.Log("enable");
    }

    private void Start()
    {
        _spawnPointList.AddRange(from Transform child in transform select child);
        Debug.Log("spawn point list set");
        if (_playerBaseList.Count > 0)
        {
            // for now it only picks the first base in the list later we will have to create a logic to pick one and spawn enemies according to that and pick the base according to that
            targetPlayerBase = _playerBaseList[0].transform.position;
            Debug.Log("target base selected");
        }

        PickSpawnPoint();
    }

    private void PickSpawnPoint()
    {
        int randomIndex = Random.Range(0, _spawnPointList.Count);
        Debug.Log("spawn point picked");
        StartCoroutine(SpawnEnemyWithDelay(_spawnPointList[randomIndex]));
    }

    IEnumerator SpawnEnemyWithDelay(Transform spawnPoint)
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            float randomZOffset = Random.Range(-_zOffset, _zOffset);
            Vector3 spawnPosition = spawnPoint.position + spawnPoint.forward * randomZOffset;

            Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("enemy spawned");

            yield return waitForSeconds;
        }
    }
}