// ########################################################
// #
// #
// #            Script written by Felix
// #
// #            
// #            
// #
// #
// #
// #
// #
// ########################################################

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;
using TMPro;


public class EventSystemManager : MonoBehaviour
{
    [Header("Event System")]
    public EventSystem eventSystem;

    public static EventSystemManager instance;


    public InputSystemUIInputModule inputSystemUIInputModule;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
    }

    public void SetCurrentSelectedGameObject(GameObject newSelectedGameObject)
    {
        eventSystem.SetSelectedGameObject(newSelectedGameObject);
        Button newSelectable = newSelectedGameObject.GetComponent<Button>();
        newSelectable.Select();
        newSelectable.OnSelect(null);
    }
}
