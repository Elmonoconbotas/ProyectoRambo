using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float Speed = 2;
    public float JumpForce = 3;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2D;
    public Animator Animator;
    private float LastShoot;
    private int Health = 5;
    private bool Grounded;
    private Vector2 DiagonalUpRight = new Vector2(1, 1);
    private Vector2 DiagonalDownRight = new Vector2(1, -1);
    private Vector2 DiagonalUpLeft = new Vector2(-1, 1);
    private Vector2 DiagonalDownLeft = new Vector2(-1, -1);

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    

    void FixedUpdate()
    {
        /*Horizontal = Input.GetAxisRaw("Horizontal");  

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("Running", Horizontal != 0.0f);
        
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        } */


        if (Input.GetKey("d"))
        {
            rb2D.velocity = new Vector2(Speed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            Animator.SetBool("running", true);
        }
        else if (Input.GetKey("a"))
        {
            rb2D.velocity = new Vector2(-Speed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            //transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            Animator.SetBool("running", true);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            Animator.SetBool("running", false);
        }

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKey("w") && Grounded)
        {
            rb2D.AddForce(Vector2.up * JumpForce);
        }
   

        if (Input.GetKey("space") && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
        

        //ANIMACIONES
        if (Grounded == false)
        {
            Animator.SetBool("jumping", true);
            Animator.SetBool("running", false);
        }

        if (Grounded) Animator.SetBool("jumping", false);

        if (Input.GetKey("space") /*&& (Input.GetKey("d") || Input.GetKey("right") || Input.GetKey("a") || Input.GetKey("left") && Grounded)*/)
        {
            Animator.SetBool("shooting", true);
        }
        else Animator.SetBool("shooting", false);

        if (Input.GetKey("s") && Grounded)
        {
            Animator.SetBool("crouch", true);
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            Animator.SetBool("running", false);
        }
        else Animator.SetBool("crouch", false);
    }

    private void Shoot()
    {
        /*Vector3 direction;

        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;*/
        Vector3 direction;
        if (spriteRenderer.flipX == false) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    /*private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }*/
    
    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject);
    }
}
