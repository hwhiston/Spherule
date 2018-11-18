using UnityEngine;
using System.Collections;

public class Sphere_Light : Spherule {

	// Use this for initialization
	void Start () {
		strength = 1;
		type = TYPE_LIGHT;
        if (actions != null)
        {
            actions.points = 30f;
            actions.coloredOnce = true;
            actions.ResetLaunchSpeed();
        }
        typesWeakAgainst.Add(TYPE_PLASMA);
        typesWeakAgainst.Add(TYPE_PLASMAFIRE);
        typesWeakAgainst.Add(TYPE_PLASMAWATER);
        typesWeakAgainst.Add(TYPE_PLASMAICE);
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
			if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_PLASMA))
			{
                actions.SphereWon(gameObject);
                return;
			}
			else if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_DARK))
			{
                actions.SphereLost(onComingSphere);
				return;
			}
            else if ((onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_FIRE) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_WATER) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_ICE)))
            {
                switch (onComingSphere.GetComponent<Spherule>().Type)
                {
                    case TYPE_FIRE:
                        actions.PaintSphere(onComingSphere, TYPE_LIGHTFIRE);
                        break;
                    case TYPE_WATER:
                        actions.PaintSphere(onComingSphere, TYPE_LIGHTWATER);
                        break;
                    case TYPE_ICE:
                        actions.PaintSphere(onComingSphere, TYPE_LIGHTICE);
                        break;
                }
                actions.SphereLost(onComingSphere);
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
