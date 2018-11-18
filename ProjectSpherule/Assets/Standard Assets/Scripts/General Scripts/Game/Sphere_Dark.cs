using UnityEngine;
using System.Collections;

public class Sphere_Dark : Spherule {

	// Use this for initialization
	void Start () {
		strength = 1;
		type = TYPE_DARK;
        if (actions != null)
        {
            actions.points = 30f;
            actions.coloredOnce = true;
            actions.ResetLaunchSpeed();
        }
        typesWeakAgainst.Add(TYPE_LIGHT);
        typesWeakAgainst.Add(TYPE_LIGHTFIRE);
        typesWeakAgainst.Add(TYPE_LIGHTWATER);
        typesWeakAgainst.Add(TYPE_LIGHTICE);
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
			if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_LIGHT))
			{
                actions.SphereWon(gameObject);
                return;
			}
			else if(onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_PLASMA))
			{
                actions.SphereLost(c.gameObject);
				return;
			}
            else if ((onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_FIRE) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_WATER) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_ICE)))
            {
                switch(onComingSphere.GetComponent<Spherule>().Type)
                {
                    case TYPE_FIRE:
                        actions.PaintSphere(onComingSphere, TYPE_DARKFIRE);
                        break;
                    case TYPE_WATER:
                        actions.PaintSphere(onComingSphere, TYPE_DARKWATER);
                        break;
                    case TYPE_ICE:
                        actions.PaintSphere(onComingSphere, TYPE_DARKICE);
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
