using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public float moveSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        MoveDown();        	
	}

    /* Moves enemy down the screen. */
    void MoveDown()
    {
       transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("COLLIDED");
        if (col.gameObject.name == "bottom")
        {
            Destroy(this.gameObject);
        }
    }
}