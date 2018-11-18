using UnityEngine;
using System.Collections;

public class Sphere_Heart : Spherule{

	// Use this for initialization
	void Start () {
        strength = 1;
        type = TYPE_HEART;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (actions == null)
        {
            return;
        }

        GameObject onComingSphere = c.gameObject;

        if (onComingSphere.tag == Spherule_Actions.TAG_HOME && !onComingSphere.GetComponent<Spherule_Actions>().isDead && !actions.isDead)
        {
            actions.SphereTie(onComingSphere);
            return;
        }

        if (c.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
