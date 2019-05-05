using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }


    // Update is called once per frame
    void Update() {

    }

    public abstract void Tapped();

    public virtual void setOn()
    {
        // stuff
    }
}
