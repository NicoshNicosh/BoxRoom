using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Spawnables = new List<GameObject>();
    public bool Persist;
    public void SpawnObject(int spawnableNum)
    {
        Instantiate(Spawnables[spawnableNum], SpawnTarget.position, SpawnTarget.rotation,Persist?SpawnableManager.Instance.transform:null);
        if (Persist)
		{
            var musicManager = FindObjectOfType<MusicManager>();
            musicManager.poopFart.pitch = Random.Range(0.5f, 1.5f);
            musicManager.poopFart.PlayOneShot(musicManager.poopFart.clip);
		}
    } 
    public Transform SpawnTarget;

}