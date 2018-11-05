using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour {
    public float moveSpeed;
    private bool canDamage;

	// Use this for initialization
	void Start () {
        canDamage = true;
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

    /* Return offensive status (can or not damage) */
    public bool CanDamage()
    {
        return canDamage;
    }

    /* To disable enemy once it has damaged the player. */
    public void HasDamaged()
    {
        canDamage = false;
    }

    /* Destroy enemy once it reaches the end-of-screen trigger. */
    void OnTriggerEnter2D(Collider2D col)
    {
        if (this.tag == "GameEnder")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            if (col.gameObject.name == "bottom")
            {
                Destroy(this.gameObject);
            }
        }
    }
}