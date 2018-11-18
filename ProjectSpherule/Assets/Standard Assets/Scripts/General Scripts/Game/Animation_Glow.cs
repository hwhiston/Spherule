using UnityEngine;
using System.Collections;

public class Animation_Glow : MonoBehaviour {

    public bool glow = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
	        
	}

    public void StartGlow()
    {
        glow = true;
        //StartCoroutine(ToggleColor());
        InvokeRepeating("ToggleColor", 0.5f, 0.5f);
    }

    public void StopGlow()
    {
        glow = false;
        //StopCoroutine(ToggleColor());
        CancelInvoke("ToggleColor");
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    private void ToggleColor()
    {
        if (gameObject.GetComponent<MeshRenderer>().material.color != Color.green)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    /*IEnumerator ToggleColor()
    {
        while (glow)
        {
            if (gameObject.GetComponent<MeshRenderer>().material.color != Color.green)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
            yield return new WaitForSeconds(1000);
        }
    }*/

}
