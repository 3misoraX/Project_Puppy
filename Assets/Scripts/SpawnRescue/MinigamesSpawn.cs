using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MinigamesSpawn : MonoBehaviour, ISpawner
{
    public List<GameObject> MinigamesPool;
    public GameObject Spawn(Vector3 spawnPoint)
    {
        int i = Random.Range(0, MinigamesPool.Count);
        return Instantiate(MinigamesPool[i],spawnPoint, MinigamesPool[i].transform.rotation);
    }
}