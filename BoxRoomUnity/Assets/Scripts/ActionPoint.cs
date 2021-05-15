using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ActionPoint : BaseEntity
{
    
    [Header("3D Only")]
    public UnityEvent OnInteract;
    public List<Transform> RootPoints = new List<Transform>();
    
    public void CharInteract()
    {   
        OnInteract.Invoke();
    }


}
