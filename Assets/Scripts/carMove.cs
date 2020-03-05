using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carMove : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;

    //add force implementation of movement
    public float accelerationTime = .5f;
    public float maxSpeed = 1f;
    private Vector2 movement;
    private float timeLeftTillMove;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }

    void FixedUpdate()
    {
        rb2d.AddForce(movement * maxSpeed);
    }

    private void handleMovement()
    {
        timeLeftTillMove -= Time.deltaTime;
        if (timeLeftTillMove <= 0)
        {
            int randomNum = Random.Range(0, 2);
            if (randomNum == 0)
            {
                movement = new Vector2(Random.Range(-1f, 1f), 0);   //move horiz
            }
            else
            {
                movement = new Vector2(0, Random.Range(-1f, 1f));   //move vert
            }
            timeLeftTillMove += accelerationTime;
        }
    }
}
