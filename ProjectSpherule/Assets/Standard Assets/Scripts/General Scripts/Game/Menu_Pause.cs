using UnityEngine;
using System.Collections;

public class Menu_Pause : MonoBehaviour {

	GUISkin skin;
	private Rect menu;
	private Rect btnResume;
	private Rect btnQuit;

	bool paused = false;

	void Awake()
	{
		menu = new Rect (Screen.width/2 - 50, Screen.height/2, 100, 20);
		btnResume = new Rect (Screen.width/2 - 50, Screen.height/2 + 20, 100, 20);
		btnQuit = new Rect (Screen.width/2 - 50, Screen.height/2 + 40, 100, 20);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P) && GameObject.Find("321Go(Clone)") == null)
			paused = togglePause();
	}
	
	void OnGUI()
	{
		if(paused)
		{
			GUI.Label(menu, "Pause");
			if(GUI.Button(btnResume, "Resume"))
				paused = togglePause();
			if(GUI.Button(btnQuit, "Quit"))
				Application.Quit();
		}
	}
	
	bool togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			return(false);
		}
		else
		{
			Time.timeScale = 0f;
			return(true);    
		}
	}
}
