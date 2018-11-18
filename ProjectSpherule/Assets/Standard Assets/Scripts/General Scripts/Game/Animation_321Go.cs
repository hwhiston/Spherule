using UnityEngine;
using System.Collections;

public class Animation_321Go : MonoBehaviour {

    private int counter = 0;

	// Use this for initialization
	void Start () {
        InvokeRepeating("StartCountdown", 0f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void StartCountdown()
    {
        switch (counter)
        {
            case 0:
                break;
            case 1:
                gameObject.GetComponent<GUIText>().text = "3";
                break;
            case 2:
                gameObject.GetComponent<GUIText>().text = "2";
                break;
            case 3:
                gameObject.GetComponent<GUIText>().text = "1";
                break;
            case 4:
                gameObject.GetComponent<GUIText>().text = "GO!";
                break;
            default:
                Destroy(gameObject);
                break;
        }
        counter++;
    }
}
