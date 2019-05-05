using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : InteractiveObject {

    private bool Selected;
    public Color unSelectedColor;
    public Color selectedColor;

    private Material thisMaterial;

    public bool falseTap;

	// Use this for initialization
	void Start () {

    //Selected = false;
    thisMaterial = this.GetComponent<Renderer>().material;


    thisMaterial.SetColor("_Color", unSelectedColor);
    


		
	}
	
	// Update is called once per frame
	void Update () {

        if (falseTap == true)
        {
            falseTap = false;
            this.Tapped();
        }
		
	}

    public override void Tapped()
    {

        Debug.Log("tapped!");
        Debug.Log("Selected: " + Selected);
        //throw new System.NotImplementedException();

        if (Selected == true)
        {
            Selected = false;
            Debug.Log("isseletced set false!");
            thisMaterial.color = unSelectedColor;
        }
        else if (Selected == false)
        {
            Selected = true;
            thisMaterial.color = selectedColor;
        }
    }
}
