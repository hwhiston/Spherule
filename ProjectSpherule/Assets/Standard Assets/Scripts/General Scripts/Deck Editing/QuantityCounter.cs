using UnityEngine;
using System.Collections;

public class QuantityCounter : MonoBehaviour {

    public GUISkin defaultSkin;
    public int numSpheres;
    private Vector3 textPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        textPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
    }

    void OnGUI()
    {
        GUI.skin = defaultSkin;
        GUI.Label(new Rect(textPos.x + gameObject.transform.lossyScale.x, (-textPos.y) + Screen.height - gameObject.transform.lossyScale.y, 100, 30), "x " + numSpheres);
    }

}
