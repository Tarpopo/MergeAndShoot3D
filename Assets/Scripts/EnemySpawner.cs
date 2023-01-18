using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Extensions;
using Interfaces;
using UnityEngine;

public class EnemySpawner : ManagerBase, IStart
{
    public bool HaveEnemies => _activeEnemies.Count > 0;
    public event Action OnAllEnemiesDie;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _despawnDelay;
    [SerializeField] private SpawnPoints[] _stagePoint;
    [SerializeField] private GameObject[] _enemies;
    private ManagerPool _managerPool;
    private readonly List<Enemy> _activeEnemies = new List<Enemy>();
    private WaitForSeconds _despawnDelayWaiter;
    private WaitForSeconds _spawnIntervalWaiter;

    public void OnStart()
    {
        _despawnDelayWaiter = new WaitForSeconds(_despawnDelay);
        _spawnIntervalWaiter = new WaitForSeconds(_spawnInterval);
        _managerPool = Toolbox.Get<ManagerPool>();
        _managerPool.AddPool(PoolType.Entities);
        Toolbox.Get<PointMover>().OnEndMoveToPoint += StartSpawnEnemies;
    }

    public bool TryGetClosetEnemy(Transform point, out Enemy enemy)
    {
        if (_activeEnemies.Count <= 0)
        {
            enemy = null;
            return false;
        }

        enemy = _activeEnemies.GetClosestElement(point.position);
        return true;
    }

    public void StartSpawnEnemies(int stageIndex) => StartCoroutine(SpawnCoroutine(_stagePoint[stageIndex]));

    private void DespawnEnemy(IDamageable damageable)
    {
        var enemy = (Enemy)damageable;
        _activeEnemies.Remove(enemy);
        StartCoroutine(DespawnCoroutine(enemy));
    }

    private void CheckEnemiesEnd()
    {
        if (HaveEnemies) return;
        OnAllEnemiesDie?.Invoke();
    }

    private IEnumerator DespawnCoroutine(Enemy enemy)
    {
        yield return _despawnDelayWaiter;
        _managerPool.Despawn(PoolType.Entities, enemy.gameObject);
        CheckEnemiesEnd();
    }

    private IEnumerator SpawnCoroutine(SpawnPoints spawnPoints)
    {
        while (spawnPoints.HavePoint)
        {
            var enemy = _managerPool.Spawn<Enemy>(PoolType.Entities, _enemies.GetRandomElement(),
                spawnPoints.GetSpawnPoint().position);
            _activeEnemies.Add(enemy);
            enemy.OnDie -= DespawnEnemy;
            enemy.OnDie += DespawnEnemy;
            yield return _spawnIntervalWaiter;
        }
    }
}

[Serializable]
public class SpawnPoints
{
    public bool HavePoint => _points.Count > 0;
    [SerializeField] private List<Transform> _points;

    public Transform GetSpawnPoint()
    {
        var point = _points[0];
        _points.Remove(point);
        return point;
    }
}