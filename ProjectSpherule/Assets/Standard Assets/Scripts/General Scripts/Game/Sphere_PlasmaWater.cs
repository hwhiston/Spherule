using UnityEngine;
using System.Collections;

public class Sphere_PlasmaWater : Spherule {

    // Use this for initialization
    void Start()
    {
        strength = 3;
        type = TYPE_PLASMAWATER;
        if (actions != null)
        {
            actions.points = 50f;
            actions.coloredOnce = true;
            actions.ResetLaunchSpeed();
        }
        typesWeakAgainst.Add(TYPE_DARK);
        typesWeakAgainst.Add(TYPE_ICE);
        typesWeakAgainst.Add(TYPE_DARKICE);
    }

    // Update is called once per frame
    void Update()
    {

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
            if (onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_ICE) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_DARK) || 
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_DARKICE))
            {
                actions.DestroySpheresOfType(onComingSphere);
                return;
            }
            else if (!actions.HasLessOrEqualStrength(onComingSphere))
            {
                actions.SphereLost(onComingSphere);
                return;
            }
        }

        if (c.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            Destroy(gameObject);
            return;
        }
    }
}
