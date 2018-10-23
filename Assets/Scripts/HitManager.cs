using UnityEngine;

public class HitManager {

    Rigidbody2D enemyRb2D;


    public bool hit(Vector2 argHitPower)
    {
        if (enemyRb2D != null)
        {
            enemyRb2D.bodyType = RigidbodyType2D.Dynamic;
            enemyRb2D.AddForce(argHitPower);
            enemyRb2D.AddTorque(-10);
            return true;
        }

        return false;
    }


    public void setEnemyRigidBody2D(Rigidbody2D argEnemyRb2D)
    {
        enemyRb2D = argEnemyRb2D;
    }


    public void resetEnemyRigidBody2D()
    {
        enemyRb2D = null;

    }

}
