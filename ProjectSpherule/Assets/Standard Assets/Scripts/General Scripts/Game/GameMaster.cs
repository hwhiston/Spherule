using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class GameMaster : MonoBehaviour {

    public GameObject spawnHere;
	public GameObject[] spheres;
    public TextAsset defaultFile;

	private string levelGeneratorText;
	private char[] levelLayout;
	private Dictionary<char, GameObject> textToSphereType;
    private bool gameOver;
    private bool levelComplete;
    private Vector3 spawnPosition;
    private bool heartSpawned = false;

    // Use this for initialization
    void Awake() {
        if (GameObject.Find("Level_Persistence") != null)
        {
            levelGeneratorText = GameObject.Find("Level_Persistence").GetComponent<PersistenceLevel>().selectedLevel.text;
            GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = GameObject.Find("Level_Persistence").GetComponent<PersistenceLevel>().backgroundImage;
        }
        else
        {
            levelGeneratorText = defaultFile.text;
        }
        
        textToSphereType = new Dictionary<char, GameObject>()
		{
			{ '0', null },
			{ 'f', spheres[0] },
			{ 'i', spheres[1] },
			{ 'w', spheres[2] },
			{ 'n', spheres[3] },
			{ 'l', spheres[4] },
			{ 'd', spheres[5] },
			{ 'p', spheres[6] },
            { '1', spheres[7] },
            { '2', spheres[8] },
            { '3', spheres[9] },
            { '4', spheres[10] },
            { '5', spheres[11] },
            { '6', spheres[12] },
            { '7', spheres[13] },
            { '8', spheres[14] },
            { '9', spheres[15] },
            { 'h', spheres[16] }
        };
        spawnPosition = spawnHere.transform.position;
	}

	void Start()
	{
        gameOver = false;
        levelComplete = false;
		levelLayout = CreateLayout (levelGeneratorText);
		SpawnSpheres (levelLayout);
        StartCountdown();
	}

	private char[] CreateLayout(string levelText){
        char[] lines = System.Array.FindAll(levelText.ToCharArray(), isNotCarriageReturn);
		//string[] lines = levelText.Split(new string[]{"\r\n", " "}, System.StringSplitOptions.None);
		return lines;
	}

    static bool isNotCarriageReturn(char c)
    {
        return c != '\r' && c != '\n';
    }

    private void SpawnSpheres(char[] levelLayout)
	{
		GameObject temp = new GameObject();
		bool alternateRow = false;
		float k = spawnPosition.y;
        float j = 0f;
        float positionModifier = 25f;
		for(int i = 0; i < levelLayout.Length; i++)
		{
			if(i > 90)
			{
				break;
			}

			if(textToSphereType[levelLayout[i]] != null)
			{
				if(alternateRow)
				{
					temp = (GameObject)Instantiate(textToSphereType[levelLayout[i]], new Vector2(spawnPosition.x + j + positionModifier/2, k), Quaternion.identity);
				}
				else
				{
					temp = (GameObject)Instantiate(textToSphereType[levelLayout[i]], new Vector2(spawnPosition.x + j, k), Quaternion.identity);
				}
				temp.GetComponent<Rigidbody2D>().isKinematic = true;
                if (temp.tag == "HeartSphere")
                {
                    temp.GetComponent<Spherule_Actions>().TagAsHeart();
                    heartSpawned = true;
                }
                else
                {
                    temp.GetComponent<Spherule_Actions>().TagAsIdle();
                }
			}

			j = j + positionModifier;

			if(alternateRow)
			{
				if(j == positionModifier*12)
				{
                    k = k - positionModifier;
					j = 0f;
					alternateRow = !alternateRow;
				}
			}
			else
			{
				if(j == positionModifier*13)
				{
                    k = k - positionModifier;
					j = 0f;
					alternateRow = !alternateRow;
				}
			}
		}
	}

    void StartCountdown()
    {
        DimScreen();
        Vector3 temp = Camera.main.WorldToViewportPoint(Vector3.zero);
        GameObject gameOverObj = (GameObject)GameObject.Instantiate(Resources.Load("321Go"), temp, Quaternion.identity);
    }

	// Update is called once per frame
	void FixedUpdate () {
		if(((GameObject.FindWithTag("OpposingSphere") == null && GameObject.FindWithTag("IdleSphere") == null) || (GameObject.FindWithTag("HeartSphere") == null && heartSpawned)) && !levelComplete)
		{
            levelComplete = true;
            DimScreen();
            Vector3 temp = Camera.main.WorldToViewportPoint(Vector3.zero);
            GameObject goObj = (GameObject)GameObject.Instantiate(Resources.Load("LevelComplete"), temp, Quaternion.identity);
        }

        if(levelComplete)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("LevelSelect");
            }
        }

        if (GameObject.FindWithTag("HomeSphere") == null && !gameOver)
        {
            gameOver = true;
            DimScreen();
            Vector3 temp = Camera.main.WorldToViewportPoint(new Vector3(0, 3, 0));
            GameObject gameOverObj = (GameObject)GameObject.Instantiate(Resources.Load("GameOver"), temp, Quaternion.identity);
        }

        if(gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Application.Quit();
            }
        }
	}

    private void DimScreen()
    {
        GameObject.Find("Background").GetComponent<SpriteRenderer>().material.color = Color.grey;
    }
}
