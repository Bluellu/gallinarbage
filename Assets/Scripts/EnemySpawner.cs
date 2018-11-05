using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    Vector3 playerPosition;
    List<Vector3> startPositions = new List<Vector3> { };
    

	// Use this for initialization
	void Start () {
        playerPosition = (GameObject.FindGameObjectsWithTag("Player"))[0]
                         .transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        //inefficient
        //GameObject[] waypointArray = GameObject.FindGameObjectsWithTag(waypointNumber);

    }
}
