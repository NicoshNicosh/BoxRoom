using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Spawnables = new List<GameObject>();
    public void SpawnObject(int spawnableNum)
    {
        Instantiate(Spawnables[spawnableNum], SpawnTarget.position, SpawnTarget.rotation);
    } 
    public Transform SpawnTarget;

}