using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _enemyCount = 12;
    [Min(1)]
    [SerializeField] private int _spawnSectionCount = 4;

    [SerializeField] private Vector2 _spawnXRange;
    [SerializeField] private Vector2 _spawnZRange;
    [SerializeField] private float _spawnY;

    [SerializeField] private Transform _car;
    [SerializeField] private GameManager _gameManager;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        int remainingEnemies = _enemyCount % _spawnSectionCount;
        int currentSection = 0;
        for (int i = 0; i < _enemyCount - remainingEnemies; i++)
        {
            SpawnEnemy(currentSection);

            currentSection++;
            if (currentSection == _spawnSectionCount)
                currentSection = 0;
        }

        for (int i = 0; i < remainingEnemies; i++)
        {
            int randomSection = Random.Range(0, _spawnSectionCount);
            SpawnEnemy(randomSection);
        }
    }

    private Vector3 GetSpawnPosition(int currentSection)
    {
        Vector3 spawnPos = new();

        float sectionLength = (_spawnZRange.y - _spawnZRange.x) / _spawnSectionCount;
        float minZ = _spawnZRange.x + currentSection * sectionLength;
        float maxZ = minZ + sectionLength;

        spawnPos.x = Random.Range(_spawnXRange.x, _spawnXRange.y);
        spawnPos.y = _spawnY;
        spawnPos.z = Random.Range(minZ, maxZ);

        return spawnPos;
    }

    private void SpawnEnemy(int section)
    {
        Quaternion spawnRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        Enemy newEnemy = Instantiate(_enemyPrefab, GetSpawnPosition(section), spawnRotation);
        newEnemy.Initialize(_car, _gameManager);
    }
}
