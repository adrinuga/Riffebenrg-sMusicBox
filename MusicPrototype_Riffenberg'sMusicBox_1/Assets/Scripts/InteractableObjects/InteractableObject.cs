using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InteractableObject: MonoBehaviour
{
    abstract public void OnClick();
    abstract public void MouseOver();
    abstract public Transform ReturnObject();


    


}

