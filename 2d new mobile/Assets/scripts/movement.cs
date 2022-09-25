using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 direction;
    public float moveSpeed = 1f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal") * moveSpeed;
        Debug.Log(direction.x);
        //direction.y = Input.GetAxis("Vertical") * moveSpeed;
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position+direction*Time.fixedDeltaTime);
    }

}
