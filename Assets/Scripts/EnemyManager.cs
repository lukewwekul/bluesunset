using UnityEngine;

public class EnemyManager : MonoBehaviour {

    Rigidbody2D rb2D;
    Animator    animator;
    Vector2     bounceWallPower = new Vector2(-100, -100);
    float       hitWallTorquePower = 40f;



    void Start()
    {

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }



    void Update()
    {

        if (rb2D.velocity.y > 1)
        {

            animator.SetBool("flyFL", true);
        }

        else
        {

            animator.SetBool("flyFL", false);
        }

        if (rb2D.IsSleeping())
        {
            rb2D.bodyType = RigidbodyType2D.Static;
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "wall")
        {

            Debug.Log(collision.relativeVelocity);
            rb2D.AddForce(bounceWallPower);
            //rb2D.AddForce(collision.relativeVelocity*100);
            rb2D.AddTorque(hitWallTorquePower);
        }
    }


}
