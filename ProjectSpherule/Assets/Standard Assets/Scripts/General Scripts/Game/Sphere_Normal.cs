using UnityEngine;
using System.Collections;

public class Sphere_Normal : Spherule {

	// Use this for initialization
	void Start () {
		strength = 1;
        type = TYPE_NORMAL;
        if (actions != null)
        {
            actions.points = 10f;
            actions.ResetLaunchSpeed();
            if (tag == Spherule_Actions.TAG_HOME)
            {
                actions.immuneInLauncher = true;
            }
        }
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
			if(actions.HasLessOrEqualStrength(onComingSphere))
			{
                actions.TagAsOpposing();
                actions.PaintType(onComingSphere.GetComponent<Spherule>().Type);
                actions.SphereTie(onComingSphere);
				return;
			}
			else
			{
                actions.SphereLost(onComingSphere);
				return;
			}
		}
		else if(onComingSphere.tag == Spherule_Actions.TAG_OPPOSING && !actions.immuneInLauncher && !actions.coloredOnce && !actions.isDead && 
            System.Array.IndexOf(sphereTypesPaint, onComingSphere.GetComponent<Spherule>().Type) >= 0)
		{
            actions.PaintSphere(onComingSphere);
		}

		if(c.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
		{
			Destroy(gameObject);
			return;
		}
	}
}
