using UnityEngine;
using System.Collections;

public class OpenScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OpenSceneOnClick(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}
