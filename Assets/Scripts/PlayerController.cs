using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        //Touch input
        if (Input.touchCount > 0) { 
            Touch touch = Input.touches[0];
            if (touch.position.x > Screen.width / 2) { //Right touch
                move(1);
            } else if (touch.position.x <= Screen.width / 2) {     //Left touch
                move(0);
            }

        //Key input
        } else if (Input.anyKey) {
            if (Input.GetKeyDown("right")) {
                move(1);    
            } else if (Input.GetKeyDown("left")) {
                move(0);
            }
        }
    }

    void move(int direction) {
        if (direction == 1) { //Move RIGHT
            Debug.Log("RIGHT");
            //Player is already on the RIGHT: jump
            if (transform.position.x > 0) {
                //TODO        
            //Player is on the LEFT: switch to RIGHT
            } else if (transform.position.x <= Screen.width / 2) {
                Vector3 p = transform.position;
                transform.position = new Vector3(-p.x, p.y, p.z);
            }

        } else if (direction == 0) { //Move LEFT
            Debug.Log("LEFT");
            //Player is already  on the LEFT: jump
            if (transform.position.x <= 0) {
                //TODO

                //Player is on the RIGHT: switch to LEFT
            } else if (transform.position.x > 0) {
                Debug.Log("should MOVE");
                Vector3 p = transform.position;
                transform.position = new Vector3(-p.x, p.y, p.z);
            }
        }
    }
}

