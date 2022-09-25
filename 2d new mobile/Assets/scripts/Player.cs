using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] runSprites;
    public Sprite climbSprite;
    private int spriteindex;

    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    public AudioClip walkSound;
    public AudioClip jumpSound;
    
    private Collider2D[] results;
    public float speed = 10f;
    public float climbSpeed = 3f;
    public float jumpSpeed = 40f;
    public Joystick joystick;
    
    private  Vector2  directions;
    
    private bool grounded;
    public bool climbing;

    private void Awake()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        results=new Collider2D[4];
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimateSprites), 1f / 12f, 1f / 12f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }


    private void CheckCollision()
    {   
        grounded = false;
        climbing = false;

        Vector2 size=collider.bounds.size;
        size.y += 0.1f;
        size.x /= 2f;
        int amount= Physics2D.OverlapBoxNonAlloc(transform.position,size,0f,results);


        for(int i=0;i<amount;i++)
        {
            GameObject hit = results[i].gameObject;
            if(hit.layer==LayerMask.NameToLayer("Ground"))
            {
                grounded=hit.transform.position.y<(transform.position.y-0.5f);
                Physics2D.IgnoreCollision(collider,results[i],!grounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                climbing = true;
            }


        }

    }
    
    // Update is called once per frame
    void Update()
    {   
        CheckCollision();

        if (joystick.Horizontal >= 0.2f)
        {
            AudioSource.PlayClipAtPoint(walkSound, transform.position, 3f);
            directions.x = speed;

        }
        else if(joystick.Horizontal<=-0.2f)
        {
            AudioSource.PlayClipAtPoint(walkSound, transform.position, 3f);
            directions.x = -speed;
        }
        else
        {
            directions.x = 0f;
        }
       // directions.x = joystick.Horizontal * speed;
        if(climbing)
        {
            directions.y = joystick.Vertical * climbSpeed;
        }
        else
        {
            directions += Physics2D.gravity * Time.deltaTime;
        }
        //directions.x = Input.GetAxis("Horizontal") * speed;
        

        if (grounded)
        {
            directions.y = Mathf.Max(directions.y, -1f);
        }
        if(directions.x < 0f)
        {
            transform.eulerAngles =new  Vector3(0f, 180f, 0f);
        }
        else if(directions.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position+directions*Time.fixedDeltaTime);
    }
    public void Jump()
    {
        // if (Input.GetButtonDown("Jump"))
        // {
        if (grounded)
        {
            AudioSource.PlayClipAtPoint(jumpSound, transform.position, 3f);
            directions = Vector2.up * jumpSpeed;
        }
       // }
       /* else
        {
           
        }*/
    }
    
    private void AnimateSprites()
    {
        if(climbing)
        {
            spriteRenderer.sprite = climbSprite;
        }
        else if(directions.x!=0f)
        {
            spriteindex++;
            if(spriteindex >= runSprites.Length)
            {
                spriteindex = 0;
            }
            spriteRenderer.sprite=runSprites[spriteindex];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Objective"))
        {   
            enabled = false; 
            FindObjectOfType<gamemanager>().LevelComplete();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            enabled = false;
            FindObjectOfType<gamemanager>().LevelFailed();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("fallingbarrel"))
        {
            enabled = false;
            FindObjectOfType<gamemanager>().LevelFailed();
        }
    }

}
