using Enemies;
using UnityEngine;
using Zenject;

public class WavesManager : IInitializable
{
    [Inject(Id = "turretTransform")] 
    private Transform _turretTransform;
    
    [Inject(Id = "planePrefab")]
    private GameObject _planePrefab;
    
    [Inject(Id = "spawnPoint")]
    private Transform _spawnPoint;
    
    [Inject(Id = "enemiesMovePoints")]
    private Transform[] _enemiesMovePoint;

    [Inject(Id = "enemiesExplosionEffectPrefab")]
    private GameObject _enemiesExplosionEffectPrefab;

    public int CountOfWaves { get; private set; } = 2;
    public int CurrentWave  { get; private set; }

    private readonly int _countOfEnemiesOnWave = 3;
    private int _currentCountOfEnemiesOnWave;
    
    [Inject(Id = "eventHandler")]
    private EventHandler _eventHandler;
    
    [Inject]
    public void Construct(InputSystem_Actions actions)
    {
        _eventHandler.onDefeatEnemy += DefeatedEnemy;
    }

    public void Initialize()
    {
        _currentCountOfEnemiesOnWave = _countOfEnemiesOnWave;
        
        SpawnWave();
    }

    private EnemyBase CreateAirplane()
    {
        GameObject enemyObj = Object.Instantiate(_planePrefab, _spawnPoint.position, Quaternion.identity);
        EnemyBase enemyBase = enemyObj.GetComponent<EnemyBase>();
        
        enemyBase.Initialize(_eventHandler, _turretTransform, _enemiesMovePoint, _enemiesExplosionEffectPrefab);
        return enemyBase;
    }

    private void DefeatedEnemy()
    {
        _currentCountOfEnemiesOnWave--;

        if (_currentCountOfEnemiesOnWave <= 0)
        {
            _currentCountOfEnemiesOnWave = _countOfEnemiesOnWave;
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        Debug.Log(CurrentWave);

        if (CurrentWave >= CountOfWaves)
        {
            _eventHandler.Win();
            return;
        }
        
        CurrentWave++;
        _eventHandler.StartNewWave();
        
        for (int i = 0; i < _countOfEnemiesOnWave; i++) 
        {
            EnemyBase enemyBase = CreateAirplane();
            enemyBase.transform.position = _spawnPoint.position + new Vector3(i * 5f, 0, 0);
            enemyBase.transform.rotation = _spawnPoint.rotation;
            Debug.Log($"Spawned airplane at {enemyBase.transform.position}");
        }
    }
}