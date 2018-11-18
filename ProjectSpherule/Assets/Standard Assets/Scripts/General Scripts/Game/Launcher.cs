using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {

    public GUISkin defaultSkin;
    public GameObject deck;
	public GameObject loaded;
	public GameObject nextSphere;

	private Deck deckComponent;
    private const float MAX_TIME_BETWEEN_SHOTS = 15f;

    public float timeBetweenShots;

	// Use this for initialization
	void Start ()
	{
		deckComponent = deck.GetComponent<Deck> ();
		loaded = deckComponent.Deal ();
        timeBetweenShots = MAX_TIME_BETWEEN_SHOTS;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (loaded != null)
		{
			if(loaded.GetComponent<Spherule_Actions>().isDead && deckComponent.GetSphereCount() != 0)
			{
				LoadNextSphere();
			}

			if(timeBetweenShots <= 1 || (Input.GetMouseButtonDown(0) && loaded.GetComponent<Rigidbody2D>().isKinematic && Time.timeScale != 0 && GameObject.Find("321Go(Clone)") == null))
			{
                timeBetweenShots = MAX_TIME_BETWEEN_SHOTS;
				loaded.GetComponent<Rigidbody2D>().isKinematic = false;
				loaded.GetComponent<Rigidbody2D>().AddForce(transform.up * loaded.GetComponent<Spherule_Actions>().launchSpeed);
				deckComponent.spheres.RemoveAt(0);
			}

			if(Input.GetKeyDown(KeyCode.N) && !loaded.GetComponent<Spherule_Actions>().IsInPlay())
			{
				loaded = deckComponent.GetStoredSphere();
			}
        }
		else
		{
			LoadNextSphere();
		}
	}

    void FixedUpdate()
    {
        if (loaded != null)
        {
            timeBetweenShots -= Time.deltaTime;
            //Debug.Log(timeBetweenShots);
        }
    }

	void LoadNextSphere()
	{
		loaded = deckComponent.Draw();
	}

    void OnGUI()
    {
        GUI.skin = defaultSkin;
        if (timeBetweenShots < 3)
        {
            GUI.Label(new Rect(Screen.width / 2 + 60, Screen.height / 2 + 200, 100, 30), timeBetweenShots.ToString("0"));
        }
    }
}
