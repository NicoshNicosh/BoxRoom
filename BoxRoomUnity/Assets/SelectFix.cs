using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFix : MonoBehaviour
{

    public Selectable s;

    private void OnEnable()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            s.Select();
        }
    }
}
