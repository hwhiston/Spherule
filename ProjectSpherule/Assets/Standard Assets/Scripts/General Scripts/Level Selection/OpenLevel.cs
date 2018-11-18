using UnityEngine;
using System.Collections;

public class OpenLevel : MonoBehaviour {

    public TextAsset levelFile;
    public Sprite levelSprite;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void SelectLevel()
    {
        if (levelFile != null)
        {
            GameObject.Find("Level_Persistence").GetComponent<PersistenceLevel>().selectedLevel = levelFile;
            GameObject.Find("Level_Persistence").GetComponent<PersistenceLevel>().backgroundImage = levelSprite;
        }

        GameObject.Find("Background").GetComponent<UnityEngine.UI.Image>().sprite = levelSprite;
        GameObject.Find("Background").GetComponent<UnityEngine.UI.Image>().material.color = Color.grey;       
    }
}
