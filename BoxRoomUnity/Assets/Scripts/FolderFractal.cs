using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolderFractal : MonoBehaviour
{
    public int RecurseIterations = 3;
    public int ChildrenPerIteration = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        if (RecurseIterations > 0)
		{
            for(int i = 0; i < ChildrenPerIteration; i++)
			{
                Vector3 pos = transform.position;
                pos.z = pos.z - 0.1f;
                GameObject child = Instantiate(gameObject, pos, Quaternion.identity, transform);
                FolderFractal fract = child.GetComponent<FolderFractal>();
                fract.RecurseIterations = RecurseIterations - 1;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
