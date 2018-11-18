using UnityEngine;
using System.Collections;

public class Sphere_Fire : Spherule {

	// Use this for initialization
	void Start () {
		strength = 1;
		type = TYPE_FIRE;
        if (actions != null)
        {
            actions.points = 20f;
            actions.coloredOnce = true;
            actions.ResetLaunchSpeed();
        }
        typesWeakAgainst.Add(TYPE_WATER);
        typesWeakAgainst.Add(TYPE_LIGHTWATER);
        typesWeakAgainst.Add(TYPE_DARKWATER);
        typesWeakAgainst.Add(TYPE_PLASMAWATER);
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
        actions.AddToAdjacentSpheres(onComingSphere);

        if (onComingSphere.tag == Spherule_Actions.TAG_HOME && !onComingSphere.GetComponent<Spherule_Actions>().isDead && !actions.isDead)
		{
			if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_WATER))
			{
                actions.SphereWon(gameObject);
                return;
			}
			else if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_ICE))
			{
                actions.SphereLost(c.gameObject);
				return;
			}
            else if ((onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_DARK) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_LIGHT) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_PLASMA)))
            {
                switch (onComingSphere.GetComponent<Spherule>().Type)
                {
                    case TYPE_DARK:
                        actions.PaintSphere(onComingSphere, TYPE_DARKFIRE);
                        break;
                    case TYPE_LIGHT:
                        actions.PaintSphere(onComingSphere, TYPE_LIGHTFIRE);
                        break;
                    case TYPE_PLASMA:
                        actions.PaintSphere(onComingSphere, TYPE_PLASMAFIRE);
                        break;
                }
                actions.SphereLost(c.gameObject);
            }
            else if(actions.HasLessOrEqualStrength(onComingSphere))
			{
                actions.SphereTie(onComingSphere);
				return;
			}
			else if(!actions.HasLessOrEqualStrength(onComingSphere))
			{
                actions.SphereLost(onComingSphere);
				return;
			}
		}

		if(c.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
		{
			Destroy(gameObject);
			return;
		}
	}
	
}
