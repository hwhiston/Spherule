using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if(v.x >= -160f && v.x <= 160f && v.y >= -160f && Time.timeScale != 0)
		{
			transform.rotation = Quaternion.LookRotation(Vector3.forward, v - transform.position);
		}
	}
}
