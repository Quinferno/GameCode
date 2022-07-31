using Conversa.Demo.Scripts;
using Conversa.Runtime;
using Conversa.Runtime.Events;
using Conversa.Runtime.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generic3DInteract : MonoBehaviour
{
    [Tooltip("Only objects on this layer can trigger")]
    public LayerMask triggerLayerMask;
    [Tooltip("Input manager button that triggers")]
    public string interactButton;
    [Tooltip("Is a triggering object close enough?")]
    [HideInInspector] public bool objectTriggerable = false;

    public UIController uIController; //Needed to reference other scripts
    

    private void Start()
    {
        uIController = GetComponent<UIController>();//Gets the appropriate script if on object
    }

    private void OnTriggerEnter(Collider other) //if an object is within the collider box on the right layer, signals true
    {
        if (triggerLayerMask == (triggerLayerMask | (1 << other.gameObject.layer)))
        {
            objectTriggerable = true;
        }
    }

    private void OnTriggerExit(Collider other) //opposite of above, signals false when object not near
    {
        if (triggerLayerMask == (triggerLayerMask | (1 << other.gameObject.layer)))
        {
            objectTriggerable = false;
        }
    }

    private void OnTriggerStay (Collider other) // was told "Stay" was wasteful and could use an alternative
    {
        if (triggerLayerMask == (triggerLayerMask | (1 << other.gameObject.layer)))
        {
            if (Input.GetButtonDown(interactButton)) //Everything below needs editing
            {
                uIController.choiceWindow.SetActive(true);
            }
        }
    }
    
}//Don't think this was stray, just poorly formatted and confusing to look at.

