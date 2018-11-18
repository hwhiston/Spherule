using UnityEngine;
using System.Collections;

public class PersistenceLevel : MonoBehaviour {

    public TextAsset selectedLevel;
    public Sprite backgroundImage;

    private static PersistenceLevel instance = null;

    public static PersistenceLevel Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
