using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public Rigidbody Body { get { return GetComponent<Rigidbody>(); } }

	public Vector3 position {
		get { return pos; }
	}

	public Vector3 pos = new Vector3();
	public Vector3 acc = new Vector3();
	public Vector3 vel = new Vector3();

    public string num;

	public void AddForce(Vector3 f){
		acc += f * 10000;
	}

    public GameObject GetNearestObject(string tag)
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestObject = null;

        foreach (var gameObject in objectsWithTag)
        {
            if(gameObject.name == num)
            {
                continue;
            }

            if (closestObject == null) closestObject = gameObject;
            

            // compares distances
            if (Vector3.Distance(transform.position, gameObject.transform.position) <= Vector3.Distance(transform.position, closestObject.transform.position))
            {
                closestObject = gameObject;
            }
        }
        return closestObject;
    }

    public GameObject GetSecondNearest(string tag)
    {
        var objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        Debug.Log(name);
        Stack<GameObject> closestObjects = new Stack<GameObject>();

        foreach (var gameObject in objectsWithTag)
        {
            if (gameObject.name == num)
            {
                continue;
            }

            if (closestObjects.Count == 0) closestObjects.Push(gameObject);


            // compares distances
            if (Vector3.Distance(transform.position, gameObject.transform.position) <= Vector3.Distance(transform.position, closestObjects.Peek().transform.position))
            {
                closestObjects.Push(gameObject);
            }
        }
        if(closestObjects.Count > 0)
        {
            Debug.Log("First Closest = " + closestObjects.Peek().name);
            closestObjects.Pop();
        }
        if (closestObjects.Count > 0)
        {
            Debug.Log("Second Closest = " + closestObjects.Peek().name);
            return closestObjects.Pop();
        }
        return null;
    }
}
