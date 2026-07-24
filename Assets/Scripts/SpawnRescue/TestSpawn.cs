using UnityEngine;

public class TestSpawn : MonoBehaviour, ISpawner
{
    public GameObject spawnPrefab;
    public GameObject Spawn(Vector3 spawnPoint)
    {
        return Instantiate(spawnPrefab, spawnPoint, spawnPrefab.transform.rotation);
    }
}