using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject resource;
    public Transform origin;
    public bool on;
    public float rps;
    public float xForce = 100;
    public float yForce = 200;
    public float maxScale;
    public float minScale;
    public bool randomScale = false;
    public bool randomScaleUniform = false;

    public float ttl = 10;

    private float counter;

	void Start () {
		
	}
	
	void Update () {
        if (!on) return;
        counter += Time.deltaTime;
        if(counter>=1/rps)
        {
            counter = 0;
            Spawn();
        }
	}

    void Spawn()
    {
        var newResource = Instantiate(resource, origin.position, Quaternion.Euler(Random.insideUnitSphere*360)) as GameObject;
        if(randomScale)
        {
            if(randomScaleUniform)
            {
                newResource.transform.localScale = Vector3.one * Random.Range(minScale, maxScale);
            }
            else
            {
                newResource.transform.localScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), Random.Range(minScale, maxScale));
            }
        }
        
        newResource.GetComponent<Rigidbody>().AddForce(transform.TransformPoint(xForce, yForce, 0));
        Destroy(newResource, ttl);
    }
}

