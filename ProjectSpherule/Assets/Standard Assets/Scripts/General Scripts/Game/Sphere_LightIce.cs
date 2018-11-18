using UnityEngine;
using System.Collections;

public class Sphere_LightIce : Spherule {

    // Use this for initialization
    void Start()
    {
        strength = 3;
        type = TYPE_LIGHTICE;
        if (actions != null)
        {
            actions.points = 50f;
            actions.coloredOnce = true;
            actions.ResetLaunchSpeed();
        }
        typesWeakAgainst.Add(TYPE_PLASMA);
        typesWeakAgainst.Add(TYPE_FIRE);
        typesWeakAgainst.Add(TYPE_PLASMAFIRE);
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
            if (onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_FIRE) ||
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_PLASMA) ||
                onComingSphere.GetComponent<Spherule>().Type.Contains(TYPE_PLASMAFIRE))
            {
                actions.DetermineSpheresInRow();
                actions.DestroySpheresInRow(onComingSphere);
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
