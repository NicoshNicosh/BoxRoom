using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletSounds : MonoBehaviour
{
    public AudioSource toiletSound1, toiletSound2;
    private int interactCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractWithToilet()
	{
        if (interactCounter++ % 2 == 0)
		{
            toiletSound2.Stop();
            toiletSound1.Play();
		}
        else
		{
            toiletSound1.Stop();
            toiletSound2.Play();
		}
	}
}
