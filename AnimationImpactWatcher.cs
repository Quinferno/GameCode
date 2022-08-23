using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationImpactWatcher : MonoBehaviour
{
    public event Action OnImpact; 

    //Called by animation
    private void Impact()
    {
        if (OnImpact != null)
            OnImpact();
    }
}
