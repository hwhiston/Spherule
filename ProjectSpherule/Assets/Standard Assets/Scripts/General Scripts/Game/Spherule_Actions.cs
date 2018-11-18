using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spherule_Actions : MonoBehaviour {

    public const string TAG_IDLE = "IdleSphere";
    public const string TAG_OPPOSING = "OpposingSphere";
    public const string TAG_HOME = "HomeSphere";
    public const string TAG_HEART = "HeartSphere";

    protected bool gotHit = false;

    protected List<GameObject> adjacentSpheres;
    protected List<GameObject> spheresInRow;

    public bool immuneInLauncher = false;
    public bool isDead = false;
    private float accumulatedPoints;
    private GameObject gameMaster;
    private GameGUILayout guiLayout;
    private bool glowing = false;

    private Spherule spherule;

    public float points;
    public float launchSpeed;
    public bool coloredOnce = false;

    void Awake()
    {
        spherule = gameObject.GetComponent<Spherule>();
        if (Application.loadedLevel != 1)
        {
            if (gameObject.GetComponent<Animation_Glow>() != null)
            {
                gameObject.GetComponent<Animation_Glow>().enabled = false;
            }
            gameObject.GetComponent<MK_RotateObject>().enabled = false;
        }

        adjacentSpheres = new List<GameObject>();
        spheresInRow = new List<GameObject>();
        gameMaster = GameObject.Find("_GameMaster");
        if (gameMaster != null)
        {
            guiLayout = gameMaster.GetComponent<GameGUILayout>();
        }
    }

    // Use this for initialization
    void Start () {
        accumulatedPoints = 0;
        points = 10f;
        if (launchSpeed == 0f)
        {
            launchSpeed = 100f;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (GameObject.Find("Arrow") != null && GameObject.Find("Arrow").GetComponent<Launcher>().loaded != null)
        {
            if (spherule.IsWeakAgainst(GameObject.Find("Arrow").GetComponent<Launcher>().loaded.GetComponent<Spherule>().Type) && gameObject.tag != TAG_HOME)
            {
                if (!gameObject.GetComponent<Animation_Glow>().glow)
                {
                    gameObject.GetComponent<Animation_Glow>().StartGlow();
                    glowing = true;
                }
            }
            else
            {
                gameObject.GetComponent<Animation_Glow>().StopGlow();
                glowing = false;
            }
        }
        else
        {
            if (gameObject.GetComponent<Animation_Glow>() != null)
            {
                gameObject.GetComponent<Animation_Glow>().StopGlow();
            }
            glowing = false;
        }
    }

    public void ResetLaunchSpeed()
    {
        launchSpeed = 3000f;
    }

    /// <summary>
    /// Generates the list of all spheres in the same row as the current sphere
    /// </summary>
    public void DetermineSpheresInRow()
    {
        spheresInRow.Clear();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.transform.position.y == gameObject.transform.position.y && go.tag == TAG_IDLE && go.GetComponent<Spherule>().Type != "Heart" && !go.Equals(gameObject))
            {
                spheresInRow.Add(go);
            }
        }
    }

    public void AddToAdjacentSpheres(GameObject sphereColliding)
    {
        if (sphereColliding.tag == TAG_IDLE && sphereColliding.GetComponent<Spherule>().Type != "Heart")
        {
            adjacentSpheres.Add(sphereColliding);
        }
    }

    public bool GotHit
    {
        get { return this.gotHit; }
        set { this.gotHit = value; }
    }

    /// <summary>
    /// Tags as opposing.
    /// </summary>
    public void TagAsOpposing()
    {
        this.tag = TAG_OPPOSING;
        if (!GetComponent<CircleCollider2D>().isTrigger)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    /// <summary>
    /// Tags as home.
    /// </summary>
    public void TagAsHome()
    {
        this.tag = TAG_HOME;
    }

    public void TagAsIdle()
    {
        this.tag = TAG_IDLE;
        if (!GetComponent<CircleCollider2D>().isTrigger)
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    public void TagAsHeart()
    {
        this.tag = TAG_HEART;
    }

    /// <summary>
    /// Replaces the current spheres with a new sphere of the specified type
    /// </summary>
    /// <param name="newType">type color to change the current sphere to</param>
	public void ChangeType(string newType)
    {
        for (int i = 0; i < spherule.SphereTypesAll.Length; i++)
        {
            if (spherule.SphereTypesAll[i].Equals(newType))
            {
                GameObject temp = (GameObject)Instantiate(Resources.Load(newType + "Sphere"), gameObject.transform.position, Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().isKinematic = true;
                temp.GetComponent<Spherule_Actions>().TagAsOpposing();
                Destroy(gameObject);
                break;
            }
        }
    }

    /// <summary>
    /// Changes the type of the current sphere to the specified type
    /// </summary>
    /// <param name="newType">type color to change the current sphere to</param>
	public void PaintType(string newType)
    {
        for (int i = 0; i < spherule.SphereTypesAll.Length; i++)
        {
            if (spherule.SphereTypesAll[i].Equals(newType))
            {
                spherule.Type = newType;
                break;
            }
        }
    }

    public bool HasLessOrEqualStrength(GameObject opposingGameObject)
    {
        return spherule.Strength <= opposingGameObject.GetComponent<Spherule>().strength;
    }

    /// <summary>
    /// Current sphere has defeated the opposing sphere
    /// </summary>
    /// <param name="defendingSphere">Opposing game object.</param>
    public void SphereWon(GameObject defendingSphere)
    {
        gotHit = true;
        AddPointsBonus();
        SpawnPointDisplay();
        Destroy(defendingSphere);
    }

    /// <summary>
    /// Curent sphere has lost against the opposing sphere
    /// </summary>
    /// <param name="attackingSphere">Opposing game object.</param>
    public void SphereLost(GameObject attackingSphere)
    {
        guiLayout.lastSavedNumber = 0f;
        DestroySphere(attackingSphere);
    }

    /// <summary>
    /// Current sphere has tied with the opposing sphere (both lost)
    /// </summary>s
    /// <param name="attackingSphere">Opposing game object.</param>
    public void SphereTie(GameObject attackingSphere)
    {
        AddPointsSimple();
        SpawnPointDisplay();

        isDead = true;
        attackingSphere.GetComponent<Spherule_Actions>().isDead = true;

        DestroySphere(gameObject);

        DestroySphere(attackingSphere);
    }

    /// <summary>
    /// Destroys all spheres in the same row as the current sphere
    /// </summary>
    /// <param name="attackingSphere">Opposing game object</param>
    public void DestroySpheresInRow(GameObject attackingSphere)
    {
        AddPointsSimple();
        SpawnPointDisplay();

        isDead = true;
        attackingSphere.GetComponent<Spherule_Actions>().isDead = true;

        DestroySphere(gameObject);

        foreach (GameObject sphere in spheresInRow)
        {
            AddPointsBonus();
            SpawnPointDisplay(sphere);

            DestroySphere(sphere);
        }

        DestroySphere(attackingSphere);
    }

    /// <summary>
    /// Destroys all spheres in the immediate radius of the current spher
    /// </summary>
    /// <param name="attackingSphere">Opposing game object</param>
    public void DestroyAdjacentSpheres(GameObject attackingSphere)
    {
        AddPointsSimple();
        SpawnPointDisplay();

        isDead = true;
        attackingSphere.GetComponent<Spherule_Actions>().isDead = true;
        DestroySphere(gameObject);

        foreach (GameObject sphere in adjacentSpheres)
        {
            AddPointsBonus();
            SpawnPointDisplay(sphere);
            DestroySphere(sphere);
        }

        DestroySphere(attackingSphere);
    }

    public List<GameObject> GetAdjacentSpheres()
    {
        List<GameObject> tempList = new List<GameObject>();

        foreach (GameObject sphere in adjacentSpheres)
        {
            tempList.Add(sphere);
        }

        return tempList;
    }

    protected void DestroySpheresInRadiusOfRandomSphere(GameObject attackingSphere)
    {
        List<GameObject> potentialSpheresToDestroy = new List<GameObject>();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.GetComponent<Spherule>() != null && (go.tag == TAG_IDLE || go.tag == TAG_OPPOSING) && !go.Equals(gameObject))
            {
                potentialSpheresToDestroy.Add(go);
            }
        }

        GameObject chosenSphere = potentialSpheresToDestroy[UnityEngine.Random.Range(0, potentialSpheresToDestroy.Count - 1)];
        DestroySphere(gameObject);
        chosenSphere.GetComponent<Spherule_Actions>().DestroyAdjacentSpheres(attackingSphere);
    }

    public void DestroySpheresOfType(GameObject attackingSphere)
    {
        List<GameObject> spheresOfType = new List<GameObject>();
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.GetComponent<Spherule>() != null && go.tag == TAG_IDLE && go.GetComponent<Spherule>().Type == attackingSphere.GetComponent<Spherule>().Type && !go.Equals(gameObject))
            {
                spheresOfType.Add(go);
            }
        }

        AddPointsSimple();
        SpawnPointDisplay();

        isDead = true;
        attackingSphere.GetComponent<Spherule_Actions>().isDead = true;
        DestroySphere(gameObject);

        foreach (GameObject sphere in spheresOfType)
        {
            AddPointsBonus();
            SpawnPointDisplay(sphere);
            DestroySphere(sphere);

        }

        DestroySphere(attackingSphere);
    }

    public void DestroySphere(GameObject sphere)
    {
        sphere.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        sphere.GetComponent<Spherule_Actions>().GotHit = true;
        sphere.GetComponent<Spherule_Actions>().isDead = true;
        sphere.GetComponent<Rigidbody2D>().gravityScale = 75f;
        sphere.GetComponent<Rigidbody2D>().mass = 1f;
        sphere.GetComponent<CircleCollider2D>().isTrigger = true;
        sphere.GetComponent<Rigidbody2D>().isKinematic = false;
        sphere.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 4000f);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            Destroy(gameObject);
            return;
        }

    }

    public void PaintSphere(GameObject onComingSphere, string hitType = "")
    {
        if (hitType == "")
        {
            hitType = onComingSphere.GetComponent<Spherule>().Type;
        }

        if (spherule.Type != hitType)
        {
            coloredOnce = true;
            this.TagAsOpposing();
            StartCoroutine(ReColor(hitType));
        }
    }

    IEnumerator ReColor(string hitType)
    {
        yield return new WaitForSeconds(0.01f);
        ChangeType(hitType);
    }

    public void SpawnPointDisplay(GameObject atObject = null)
    {
        if (accumulatedPoints != 0)
        {
            Vector3 temp = atObject == null ? Camera.main.WorldToViewportPoint(transform.position) : Camera.main.WorldToViewportPoint(atObject.transform.position);
            GameObject tempPoint = (GameObject)GameObject.Instantiate(Resources.Load("Point"), temp, Quaternion.identity);
            tempPoint.GetComponent<GUIText>().text = accumulatedPoints.ToString();
            gameMaster.GetComponent<GameGUILayout>().AddToScore(accumulatedPoints);
        }
    }

    private void AddPointsSimple()
    {
        guiLayout.lastSavedNumber = 0;
        //if (guiLayout.lastSavedNumber == 0)
        //{
        accumulatedPoints += points;
        //}
        //else
        //{
        //    accumulatedPoints = guiLayout.lastSavedNumber * 4f;
        //}
        //guiLayout.lastSavedNumber = 0f;
    }

    private void AddPointsBonus()
    {
        accumulatedPoints += points;
        if (guiLayout.lastSavedNumber == 0)
        {
            guiLayout.lastSavedNumber += accumulatedPoints;
        }
        else
        {
            guiLayout.lastSavedNumber += guiLayout.lastSavedNumber;
        }
        accumulatedPoints = guiLayout.lastSavedNumber * 2f;
    }

    public bool IsInPlay()
    {
        return !GetComponent<Rigidbody2D>().isKinematic;
    }
}
