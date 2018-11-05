using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public int lives;
    public float groundPos;
    public float maxJump;
    public float jumpSpeed;
    public float fallSpeed;

    private bool jumping;
    private int availableLives;

    private Animator anim;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    void Start() {
        availableLives = lives;
        jumping = false;
        StartCoroutine(StartCountdown(10)); // Time at Idle state
        anim.SetTrigger("Run_R");
    }

    // Update is called once per frame
    void Update() {
        //accept input only if grounded
        if ((transform.position.x == groundPos) || 
            (transform.position.x == -groundPos)) { 
            // Touch input
            if (Input.touchCount > 0) {
                Touch touch = Input.touches[0];
                // Only accept input at the beginning of a touch.
                if (touch.phase == TouchPhase.Began) { 
                    if (touch.position.x > Screen.width / 2) { //Right touch
                        Move(1);
                    } else if (touch.position.x <= Screen.width / 2) { //Left touch
                        Move(0);
                    }
                }

            // Keyboard input
            } else if (Input.anyKey) {
                if (Input.GetKeyDown("right")) {
                    Move(1);
                } else if (Input.GetKeyDown("left")) {
                    Move(0);
                }
            }
        }
    }

    /* Detect collisions with enemies and reduce the number of lives on damage. */
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("COLLISION/n");
        if (col.gameObject.tag == "Enemy")
        {
            EnemyBehaviour enemyScript = col.gameObject.GetComponent<EnemyBehaviour>();

            if (enemyScript.CanDamage())
            {
                availableLives--;
                enemyScript.HasDamaged(); // Set enemy's ability to damage to false

                // Play the appropriate damage animation
                if (this.transform.position.x >= 0)
                    anim.SetTrigger("Dmg_R");
                else if (this.transform.position.x < 0)
                    anim.SetTrigger("Dmg_L");

                // Remove one life icon from the Canvas
                if (availableLives >= 0)
                {
                    var lifeIcon = GameObject.Find("life" + (availableLives + 1));
                    lifeIcon.SetActive(false);
                    Debug.Log("life" + (availableLives + 1) + "\n");
                }

                if (availableLives < 0)
                    SceneManager.LoadScene(2);
            }
        }
    }
    
        IEnumerator StartCountdown(int n)
    {
        yield return new WaitForSeconds(n);
    }

    /* Calculate whether player will move or jump to the specified direction. */
    void Move(int direction) {
        if (direction == 1) { //Move RIGHT
            // Player is already on the RIGHT: jump.
            if (transform.position.x > 0) {
                this.anim.SetBool("Jump_R", true); // set animation status 
                StartCoroutine(Jump(1));
                // Player is on the LEFT: switch to RIGHT.
            } else if (transform.position.x < 0) {
                anim.SetTrigger("Run_R");
                Vector3 p = transform.position;
                UpdateX(-p.x);
            }
        } else if (direction == 0) { //Move LEFT
            // Player is already  on the LEFT: jump.
            if (transform.position.x < 0) {
                this.anim.SetBool("Jump_L", true); // set animation status 
                StartCoroutine(Jump(0));
                // Player is on the RIGHT: switch to LEFT.
            } else if (transform.position.x > 0) {
                anim.SetTrigger("Run_L");
                Vector3 p = transform.position;
                transform.position = new Vector3(-p.x, p.y, p.z);
            }
        }
    }

    
    /* Jump to the specified horizontal direction. */
    IEnumerator Jump(int direction) {
        jumping = true;

        if (direction == 1) { // Jump to the right
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
                        // Make sure player returns to original position.
                        UpdateX(groundPos);
                        this.anim.SetBool("Jump_R", false); // reset animation status 
                        StopAllCoroutines();
                    }
                }
                yield return new WaitForEndOfFrame();
            }            

        } else if (direction == 0) { // Jump to the left
            this.anim.SetBool("Jump_L", true); // set animation status 

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
                        // Make sure player returns to original position.
                        UpdateX(-groundPos);
                        this.anim.SetBool("Jump_L", false); // reset animation status 

                        StopAllCoroutines();
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }
        yield return 0;
    }


    /* Update x position as defined */
    void UpdateX(float x) {
        Vector3 p = transform.position;
        transform.position = new Vector3(x, p.y, p.z);
    }
}

