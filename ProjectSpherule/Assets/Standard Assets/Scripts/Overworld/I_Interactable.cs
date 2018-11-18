using UnityEngine;
using System.Collections;

public class I_Interactable : MonoBehaviour {

    public string text;
    private Rect box;
    private bool textDisplaying = false;
    private GameObject collidingObject;

    // Use this for initialization
    void Start () {
        box = new Rect(Screen.width / 2 - 50, 0, Screen.width, Screen.height/10);
    }
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerStay2D(Collider2D c)
    {
        if(c.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                textDisplaying = !textDisplaying;
                collidingObject = c.gameObject;
            }
        }
    }

    void OnCollisionStay2D(Collision2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                textDisplaying = !textDisplaying;
                collidingObject = c.gameObject;
            }
        }
    }

    void OnGUI()
    {
        if(textDisplaying)
        {
            GUI.Label(box, text);
            if (collidingObject != null)
            {
                collidingObject.GetComponent<CharacterMovement>().enabled = false;
            }
        }
        else
        {
            if (collidingObject != null)
            {
                collidingObject.GetComponent<CharacterMovement>().enabled = true;
            }
        }
    }
}
