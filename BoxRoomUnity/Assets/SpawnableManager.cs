using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnableManager : MonoBehaviour
{

    public List<GameObject> SpawnableRefs;
    public static SpawnableManager Instance;

    private void Awake()
    {
        SpawnableRefs = transform.Cast<Transform>().Select(it => it.gameObject).ToList();
        Instance = this;
    }
}