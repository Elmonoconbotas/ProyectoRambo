using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGruntScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;

    public float Speed;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        Rigidbody2D.velocity = Direction * Speed;

    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        JohnMovement john = collision.GetComponent<JohnMovement>();
        //GruntScript grunt = collision.GetComponent<GruntScript>();

        if (john != null)
        {
            john.Hit();
            DestroyBullet();
        }
        /*if (grunt != null)
        {
            grunt.Hit();
            DestroyBullet();
        }*/
    }
}

