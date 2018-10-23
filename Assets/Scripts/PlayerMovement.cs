using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb2D;
    BoxCollider2D bc2D;
    SpriteRenderer sr;
    Animator animator;

    int moveHorizontal = 0;

    Vector2 moveVector = new Vector2(0, 0),
            colliderOffsetMoveRight = new Vector2(0, 0),
            colliderOffsetMoveLeft = new Vector2(0, 0);

    public float moveSpeed = 4;
    public Vector2 spriteFlipOffsetX = new Vector2(1.45f, 0);


	void Start ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        colliderOffsetMoveRight = bc2D.offset;
        colliderOffsetMoveLeft.Set(bc2D.offset.x * -1, bc2D.offset.y);
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
	}


	void Update ()
    {
        moveHorizontal = (int)Input.GetAxisRaw("Horizontal");
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


    bool flipPlayer(){

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


}
