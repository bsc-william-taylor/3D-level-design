using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var anim = GameObject.Find("walk").GetComponent<Animation>();
	    anim.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
