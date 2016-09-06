using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float groundPos;
    public float maxJump;
    public float jumpSpeed;
    public float fallSpeed;
    private bool jumping;

    void Start() {
        jumping = false;
    }

    // Update is called once per frame
    void Update() {
        //accept input only if grounded
        if ((transform.position.x == groundPos) || 
            (transform.position.x == -groundPos)) { 
            //Touch input
            if (Input.touchCount > 0) {
                Touch touch = Input.touches[0];
                //only accept input at the beginning of touch
                if (touch.phase == TouchPhase.Began) { 
                    if (touch.position.x > Screen.width / 2) { //Right touch
                        move(1);
                    } else if (touch.position.x <= Screen.width / 2) { //Left touch
                        move(0);
                    }
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
    }

    /*Calculates whether player will move or jump to the specified direction */
    void move(int direction) {
        if (direction == 1) { //Move RIGHT
            //Player is already on the RIGHT: jump
            if (transform.position.x > 0) {
                StartCoroutine(jump(1));
                //Player is on the LEFT: switch to RIGHT
            } else if (transform.position.x < 0) {
                Vector3 p = transform.position;
                updateX(-p.x);
            }

        } else if (direction == 0) { //Move LEFT
            //Player is already  on the LEFT: jump
            if (transform.position.x < 0) {
                StartCoroutine(jump(0));
                //Player is on the RIGHT: switch to LEFT
            } else if (transform.position.x > 0) {
                Vector3 p = transform.position;
                transform.position = new Vector3(-p.x, p.y, p.z);
            }
        }
    }

    
    /* Jumps to the specified horizontal direction */
    IEnumerator jump(int direction) {
        jumping = true;
        if (direction == 1) { //Jump to the right
            while (true) {
                if (transform.position.x >= maxJump) {
                    jumping = false;
                }
                if (jumping) {
                    transform.Translate(Vector3.right * jumpSpeed * Time.deltaTime);
                } else if (!jumping) {
                    Vector3 p = transform.position;
                    transform.position = Vector3.Lerp(transform.position, 
                                            new Vector3(groundPos, p.y, p.z), 
                                            fallSpeed * Time.deltaTime);
                    if (transform.position.x <= groundPos + 0.2) {
                        //make sure player returns to original position.
                        updateX(groundPos); 
                        StopAllCoroutines();
                    }
                }
                yield return new WaitForEndOfFrame();
            }            

        } else if (direction == 0) { //Jump to the left
            while (true) {
                if (transform.position.x <= -maxJump) {
                    jumping = false;
                }
                if (jumping) {
                    transform.Translate(
                        Vector3.left * jumpSpeed * Time.smoothDeltaTime);
                } else if (!jumping) {
                    Vector3 p = transform.position;
                    transform.position = Vector3.Lerp(transform.position, 
                                            new Vector3(-groundPos, p.y, p.z), 
                                            fallSpeed * Time.smoothDeltaTime);
                    if (transform.position.x >= -groundPos - 0.2) {
                        //make sure player returns to original position.
                        updateX(-groundPos);
                        StopAllCoroutines();
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }
        yield return 0;
    }


    /* Update x position as defined */
    void updateX(float x) {
        Vector3 p = transform.position;
        transform.position = new Vector3(x, p.y, p.z);
    }
}

