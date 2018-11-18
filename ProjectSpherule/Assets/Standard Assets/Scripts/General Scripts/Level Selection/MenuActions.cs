using UnityEngine;
using System.Collections;

public class MenuActions : MonoBehaviour {

    public Sprite defaultBackground;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnMouseDown()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.tag == "Level")
            {
                GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = defaultBackground;
                GameObject.Find("Background").GetComponent<SpriteRenderer>().material.color = Color.white;
                go.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
}
