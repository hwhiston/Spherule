using UnityEngine;
using System.Collections;

public class Animation_Point : MonoBehaviour {

	private float rateOfAscent;
	private float secondsAlive = 1.5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (PauseDestroy ());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gameObject.transform.position += Vector3.up * rateOfAscent * Time.deltaTime;
		rateOfAscent += 0.001f;
	}

	IEnumerator PauseDestroy()
	{
		yield return new WaitForSeconds(secondsAlive);
		Destroy (gameObject);
	}

}
