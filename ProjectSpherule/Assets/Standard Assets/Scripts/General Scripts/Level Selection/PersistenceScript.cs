using UnityEngine;
using System.Collections;

public class PersistenceScript : MonoBehaviour {

    private static PersistenceScript instance = null;

    public static PersistenceScript Instance
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
}
