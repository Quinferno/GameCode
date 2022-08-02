using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generic3DInteract : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;

    [Tooltip("Only objects on this layer can trigger")]
    public LayerMask triggerLayerMask;
    [Tooltip("Input manager button that triggers")]
    public string interactButton;
    [Tooltip("Is a triggering object close enough?")]
    [HideInInspector] public bool objectTriggerable = false;
    [HideInInspector] public bool isConversing = false;//Newly added

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

    


    private void OnTriggerStay(Collider other) // was told "Stay" was wasteful and could use an alternative
    {
       if (triggerLayerMask == (triggerLayerMask | (1 << other.gameObject.layer)))
        {
            if (Input.GetButtonDown(interactButton)) //Everything below needs editing for different object purposes
            {
                gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                isConversing = true;//Newly added
            }
        }
   }
}
    

