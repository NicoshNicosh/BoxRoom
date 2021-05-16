using UnityEngine;
using UnityEngine.EventSystems;


public class AutoSelector : MonoBehaviour
{
    public EventSystem EventSystem;
    public GameObject LastObject;
    private void Update()
    {

        if (!EventSystem.currentSelectedGameObject)
        {
            EventSystem.SetSelectedGameObject(LastObject ? LastObject : EventSystem.firstSelectedGameObject);
        }
        LastObject = EventSystem.currentSelectedGameObject;

    }
}
