using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Spherule : MonoBehaviour {

    protected const string TYPE_NORMAL = "Normal";
    protected const string TYPE_WATER = "Water";
    protected const string TYPE_FIRE = "Fire";
    protected const string TYPE_ICE = "Ice";
    protected const string TYPE_DARK = "Dark";
    protected const string TYPE_LIGHT = "Light";
    protected const string TYPE_PLASMA = "Plasma";
    protected const string TYPE_DARKFIRE = "DarkFire";
    protected const string TYPE_LIGHTFIRE = "LightFire";
    protected const string TYPE_PLASMAFIRE = "PlasmaFire";
    protected const string TYPE_DARKWATER = "DarkWater";
    protected const string TYPE_LIGHTWATER = "LightWater";
    protected const string TYPE_PLASMAWATER = "PlasmaWater";
    protected const string TYPE_DARKICE = "DarkIce";
    protected const string TYPE_LIGHTICE = "LightIce";
    protected const string TYPE_PLASMAICE = "PlasmaIce";
    protected const string TYPE_HEART = "Heart";
    
    protected string[] sphereTypesAll;
    protected string[] sphereTypesPaint;
    protected List<string> typesWeakAgainst;

    private bool isMouseOver = false;
    private Vector3 mousePos;

    public float strength;
    public float weight;
    protected string type;

    protected Spherule_Actions actions;

    void Awake()
    {
        actions = gameObject.GetComponent<Spherule_Actions>();
        typesWeakAgainst = new List<string>();
        sphereTypesAll = new string[] { TYPE_NORMAL, TYPE_WATER, TYPE_FIRE, TYPE_ICE,
            TYPE_DARK, TYPE_LIGHT, TYPE_PLASMA,
            TYPE_DARKFIRE, TYPE_LIGHTFIRE, TYPE_PLASMAFIRE,
            TYPE_DARKWATER, TYPE_LIGHTWATER, TYPE_PLASMAWATER,
            TYPE_DARKICE, TYPE_LIGHTICE, TYPE_PLASMAICE,};
        sphereTypesPaint = new string[] { TYPE_WATER, TYPE_FIRE, TYPE_ICE, TYPE_DARK, TYPE_LIGHT, TYPE_PLASMA };
    }

    public Spherule_Actions SpheruleActions
    {
        get { return this.actions; }
    }

    public string[] SphereTypesAll
    {
        get { return this.sphereTypesAll; }
    }

    public string[] SphereTypesPaint
    {
        get { return this.sphereTypesPaint; }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    void OnMouseEnter()
    {
        isMouseOver = true;
        mousePos = Input.mousePosition;
    }

    void OnMouseExit()
    {
        isMouseOver = false;
    }


    void OnGUI()
    {
        if ((isMouseOver && actions == null)  || (isMouseOver && actions != null && !actions.IsInPlay()))
        {
            GUI.Box(new Rect(mousePos.x, Screen.height - mousePos.y, 100, 55),
                    "Type: " + Type + "\n" +
                    "Strength: " + Strength + "\n" +
                    "Weight: " + Weight
            );
        }
    }

    public float Strength
    {
        get { return this.strength; }
        set { this.strength = value; }
    }

    public float Weight
    {
        get { return this.weight; }
        set { this.weight = value; }
    }

    public string Type
    {
        get { return this.type; }
        set { this.type = value; }
    }

    public bool IsWeakAgainst(string hitType)
    {
        return typesWeakAgainst.Contains(hitType);
    }

	// Update is called once per frame
	void Update () {

	}

}
