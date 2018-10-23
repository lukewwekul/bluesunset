using UnityEngine;

public class EnemyManager : MonoBehaviour {

    Rigidbody2D rb2D;
    Animator    animator;
    AudioSource audioSrc;
    Vector2     bounceWallPower = new Vector2(-100, -100);
    float       hitWallTorquePower = 40f;



    void Start()
    {

        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }



    void Update()
    {

        if (rb2D.velocity.y > 1)
        {

            animator.SetBool("flyFL", true);
            animator.SetBool("fallFL", false);
        }

        else if (rb2D.velocity.y < -1)
        {

            animator.SetBool("flyFL", false);
            animator.SetBool("fallFL", true);
        }

        else
        {

            animator.SetBool("flyFL", false);
            animator.SetBool("fallFL", false);
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
            rb2D.AddTorque(hitWallTorquePower);
        }

        audioSrc.volume = collision.relativeVelocity.magnitude / 12.5f;
        audioSrc.Play();
    }


}
