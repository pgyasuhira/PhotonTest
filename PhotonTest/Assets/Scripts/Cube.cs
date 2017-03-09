using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Photon.MonoBehaviour {

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 pos = transform.position;
        pos.y -= Time.deltaTime;
        transform.position = pos;

        if (transform.position.y < -20.0f)
        {
            Destroy(this);
        }
    }
}
