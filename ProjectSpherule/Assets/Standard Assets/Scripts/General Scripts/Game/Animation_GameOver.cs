using UnityEngine;
using System.Collections;

public class Animation_GameOver : MonoBehaviour {

    GUISkin skin;
    private Rect menu;
    private Rect btnResume;
    private Rect btnQuit;

    private float rateOfDescent;
    private float secondsAlive = 10f;
    private bool finishedDescending = false;

    // Use this for initialization
    void Start () {
        menu = new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 20);
        btnResume = new Rect(Screen.width / 2 - 50, Screen.height / 2 + 20, 100, 20);
        btnQuit = new Rect(Screen.width / 2 - 50, Screen.height / 2 + 40, 100, 20);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Camera.main.ViewportToScreenPoint(gameObject.transform.position).y > Screen.height / 2)
        {
            gameObject.transform.position += Vector3.down * rateOfDescent * Time.deltaTime;
            rateOfDescent += 0.005f;
        }
        else
        {
            finishedDescending = true;
        }
    }

    void OnGUI()
    {
        if(finishedDescending)
        {
            GUI.Label(menu, "Game Over");
            if (GUI.Button(btnResume, "Retry"))
                Application.LoadLevel("TestScene");
            if (GUI.Button(btnQuit, "Quit"))
                Application.LoadLevel("LevelSelect");
        }
    }

    IEnumerator PauseDestroy()
    {
        yield return new WaitForSeconds(secondsAlive);
        Destroy(gameObject);
    }
}
