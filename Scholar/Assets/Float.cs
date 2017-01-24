using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    var y0 = transform.position.y;

        transform.forward = new Vector3(0.0f, y0 + 0.25f * Mathf.Sin(Time.deltaTime), 0.0f);
	}
}
