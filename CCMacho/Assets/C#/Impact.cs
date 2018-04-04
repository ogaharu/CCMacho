using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour {

	int frame = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		++frame;
		Debug.Log(frame.ToString());
		if(frame == 60)
		{
			transform.localScale = new Vector3(40f, 40f, 40f);
			Debug.Log("in");
		}
	}
}
