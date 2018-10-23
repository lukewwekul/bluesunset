using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D     rb2D;
    BoxCollider2D   bc2D;
    SpriteRenderer  sr;
    Animator        animator;

    HitManager      hitMng;

    int             moveHorizontal = 0;

    Vector2         moveVector = new Vector2(0, 0),
                    colliderOffsetMoveRight = new Vector2(0, 0),
                    colliderOffsetMoveLeft = new Vector2(0, 0);

    public Vector2  spriteFlipOffsetX = new Vector2(1.45f, 0),
                    hitPower = new Vector2(3750, 3750);

    public float    moveSpeed = 4,
                    hitTorque = -40;



	void Start ()
    {

        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();

        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        colliderOffsetMoveRight = bc2D.offset;
        colliderOffsetMoveLeft.Set(bc2D.offset.x * -1, bc2D.offset.y);

        hitMng = new HitManager();
	}



	void Update ()
    {

        moveHorizontal = (int)Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.R))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}



    void FixedUpdate()
    {

        playerMove();
    }



    bool playerMove()
    {

        if (!useWeapon() && moveHorizontal !=0)
        {

            if (!flipPlayer())
            {

                animator.SetBool("moveFL", true);
                moveVector.Set(moveSpeed * moveHorizontal, 0);
                rb2D.MovePosition(rb2D.position + moveVector * Time.fixedDeltaTime);
            }
            return true;
        }
        else
        {

            animator.SetBool("moveFL", false);
            return false;
        }
    }



    bool flipPlayer()
    {

        if (moveHorizontal == 1 && sr.flipX)
        {

            bc2D.offset = colliderOffsetMoveRight;
            sr.flipX = false;
            rb2D.MovePosition(rb2D.position + spriteFlipOffsetX);
            return true;
        }
        
        else if (moveHorizontal == -1 && !sr.flipX)
        {

            bc2D.offset = colliderOffsetMoveLeft;
            sr.flipX = true;
            rb2D.MovePosition(rb2D.position - spriteFlipOffsetX);
            return true;
        }

        else
        {

            return false;
        }
    }



    bool useWeapon()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.E))
        {

            animator.SetBool("hitFL", true);
            return true;
        }
        else
        {

            animator.SetBool("hitFL", false);
            return false;
        }
    }



    void OnTriggerStay2D(Collider2D col)
    {

        if(col.tag == "enemy")
        {

            var tmpEnemyRb2D = col.gameObject.GetComponent<Rigidbody2D>();
            hitMng.setEnemyRigidBody2D(tmpEnemyRb2D);
        }
        else
        {

            hitMng.resetEnemyRigidBody2D();
        }
    }


    void OnTriggerExit2D()
    {

        hitMng.resetEnemyRigidBody2D();
    }



    public bool hit()
    {

        Debug.Log(hitMng.hit(hitPower, hitTorque));
        return false;
    }


}
