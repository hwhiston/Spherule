using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    private const string FIRESPHERE = "FireSphere";
    private const string WATERSPHERE = "WaterSphere";
    private const string ICESPHERE = "IceSphere";
    private const string DARKSPHERE = "DarkSphere";
    private const string LIGHTSPHERE = "LightSphere";
    private const string PLASMASPHERE = "PlasmaSphere";

    private const string DARKFIRESPHERE = "DarkFireSphere";
    private const string LIGHTFIRESPHERE = "LightFireSphere";
    private const string PLASMAFIRESPHERE = "PlasmaFireSphere";

    private const string DARKWATERSPHERE = "DarkWaterSphere";
    private const string LIGHTWATERSPHERE = "LightWaterSphere";
    private const string PLASMAWATERSPHERE = "PlasmaWaterSphere";

    private const string DARKICESPHERE = "DarkIceSphere";
    private const string LIGHTICESPHERE = "LightIceSphere";
    private const string PLASMAICESPHERE = "PlasmaIceSphere";

    private string[] typeList = new string[] { FIRESPHERE, WATERSPHERE, ICESPHERE, DARKSPHERE, LIGHTSPHERE, PLASMASPHERE,
        DARKFIRESPHERE, LIGHTFIRESPHERE, PLASMAFIRESPHERE, DARKWATERSPHERE, LIGHTWATERSPHERE, PLASMAWATERSPHERE, DARKICESPHERE, LIGHTICESPHERE, PLASMAICESPHERE};

    public GameObject loadedSphere;
    public GameObject nextSphere;
    public GameObject holdedSphere;
    public List<GameObject> spheres;
    public bool generateRandomDeck;

    private GameObject firstSphere;
    private GameObject secondSphere;
    private GameObject storageSphere;

    private GameObject gameMaster;
    private GameGUILayout guiLayout;

    void Awake()
    {
        spheres = new List<GameObject>();
        if (generateRandomDeck)
        {
            spheres = GenerateRandomDeck();
        }
        gameMaster = GameObject.Find("_GameMaster");
        guiLayout = gameMaster.GetComponent<GameGUILayout>();
    }

    private List<GameObject> GenerateRandomDeck()
    {
        List<GameObject> tempList = new List<GameObject>();
        for(int i = 0; i < 20; i++)
        {
            tempList.Add((GameObject)Resources.Load(typeList[Random.Range(0, typeList.Length)]));
        }
        return tempList;
    }

    // Use this for initialization
    void Start()
    {
        foreach (GameObject sphere in spheres)
        {
            sphere.GetComponent<Spherule_Actions>().TagAsHome();
        }
        guiLayout.leftInDeck = spheres.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Deal()
    {
        if (OutOfSpheres())
        {
            return null;
        }
        else
        {
            firstSphere = FirstSphere();
            if (HaveMoreThanOneSphere())
            {
                secondSphere = NextSphere();
            }
        }
        return firstSphere;
    }

    public GameObject Draw()
    {
        guiLayout.leftInDeck = spheres.Count;
        if (OutOfSpheres())
        {
            return null;
        }
        else
        {
            firstSphere = secondSphere;
        }
        firstSphere.transform.position = loadedSphere.transform.position;
        if (HaveMoreThanTwoSpheres())
        {
            secondSphere = NextSphere();
        }
        else if (storageSphere != null)
        {
            secondSphere = storageSphere;
        }
        else if (HaveMoreThanOneSphere())
        {
            secondSphere = NextSphere();
        }

        return firstSphere;
    }

    public GameObject GetStoredSphere()
    {
        if (HaveMoreThanOneSphere())
        {
            if (storageSphere == null)
            {
                GameObject temp;
                storageSphere = firstSphere;
                storageSphere.transform.position = holdedSphere.transform.position;
                temp = spheres[0];
                spheres.RemoveAt(0);
                spheres.Insert(spheres.Count, temp);
                firstSphere = Draw();
            }
            else
            {
                GameObject temp;
                Swap(spheres, 0, spheres.Count - 1);
                firstSphere.transform.position = holdedSphere.transform.position;
                storageSphere.transform.position = loadedSphere.transform.position;
                temp = firstSphere;
                firstSphere = storageSphere;
                storageSphere = temp;
            }
        }

        return firstSphere;
    }

    public GameObject FirstSphere()
    {
        firstSphere = (GameObject)Instantiate(spheres[0]);
        firstSphere.transform.position = loadedSphere.transform.position;
        return firstSphere;
    }

    public GameObject NextSphere()
    {
        secondSphere = (GameObject)Instantiate(spheres[1]);
        secondSphere.transform.position = nextSphere.transform.position;
        return secondSphere;
    }

    private void Swap(List<GameObject> list, int indexA, int indexB)
    {
        GameObject tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    public float GetSphereCount()
    {
        return spheres.Count;
    }

    private bool OutOfSpheres()
    {
        return spheres.Count == 0;
    }

    private bool HaveMoreThanOneSphere()
    {
        return spheres.Count > 1;
    }

    private bool HaveMoreThanTwoSpheres()
    {
        return spheres.Count > 2;
    }
}
