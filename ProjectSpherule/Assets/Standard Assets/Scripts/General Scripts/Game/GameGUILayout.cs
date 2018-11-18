using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameGUILayout : MonoBehaviour {

	public GUISkin defaultSkin;
	public float timeRemaining;		//300f = 5 minutes
	private string minutes;
	private string seconds;
	public string textTime; //added this member variable here so we can access it through other scripts
	public float currentScore;
	public float lastSavedNumber;
	public float leftInDeck;

	// Use this for initialization
	void Start () {
		currentScore = 0f;
		lastSavedNumber = 0f;
	}

	void Awake() {
		timeRemaining = 120f;
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (leftInDeck > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
	}

	public void AddToScore(float points)
	{
		currentScore += points;
	}

	void OnGUI()
	{
		minutes = Mathf.Floor (timeRemaining / 60).ToString ("00");
		seconds = (timeRemaining % 60 - 1).ToString ("00");
		textTime = minutes + ":" + seconds;
		GUI.skin = defaultSkin;
        GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 180, 100, 30), "Time: " + textTime);
		GUI.Label (new Rect (Screen.width/2 - 70, Screen.height/2 + 200, 100, 30), "Score:" + currentScore);
		GUI.Label(new Rect (Screen.width/2 + 20, Screen.height/2 + 180, 100, 30), "Deck:" + leftInDeck);
	}
}
