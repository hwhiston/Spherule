using UnityEngine;
using System.Collections;

public class Sphere_Ice : Spherule {

	// Use this for initialization
	void Start ()
	{
		strength = 1;
		type = TYPE_ICE;
        if (actions != null)
        {
            actions.points = 20f;
            actions.coloredOnce = true;
            actions.ResetLaunchSpeed();
        }
        typesWeakAgainst.Add(TYPE_FIRE);
        typesWeakAgainst.Add(TYPE_LIGHTFIRE);
        typesWeakAgainst.Add(TYPE_DARKFIRE);
        typesWeakAgainst.Add(TYPE_PLASMAFIRE);
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
			if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_FIRE))
			{
                actions.SphereWon(gameObject);
                return;
			}
			else if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_WATER))
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
                        actions.PaintSphere(onComingSphere, TYPE_DARKICE);
                        break;
                    case TYPE_LIGHT:
                        actions.PaintSphere(onComingSphere, TYPE_LIGHTICE);
                        break;
                    case TYPE_PLASMA:
                        actions.PaintSphere(onComingSphere, TYPE_PLASMAICE);
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
