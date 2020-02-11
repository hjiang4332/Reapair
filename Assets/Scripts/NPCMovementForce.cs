using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementForce : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Animator animator;

    //add force implementation of movement
    public float accelerationTime = .5f;
    public float maxSpeed = 1f;
    private Vector2 movement;
    private float timeLeftTillMove;

    //heart attack
    private float timer = 1f;
    private float chanceToDie = .2f;
    private float diceRoll;
    private bool isDead = false;
    bool waitForTime = false;

    //spawn soul
    public GameObject soul;

    private AudioSource audio;

    //sounds

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        handleMovement();
        if(tag == "NPC")    //random death
        {
            handleHeartAttackDeath();
        }
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

    void handleHeartAttackDeath()
    {
        int time = (int)timer;
        //Every 5 seconds, increase chance to die or die
        if (time % 5 == 0 && !waitForTime && isDead == false)
        {
            diceRoll = Random.Range(0, 1);
            if(diceRoll <= chanceToDie)
            {
                Destroy(gameObject);
                GameObject enemy = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;
            }
        }
        StartCoroutine(WaitForNext());
    }

    IEnumerator WaitForNext()
    {
        waitForTime = true;
        yield return new WaitForSeconds(1f);
        waitForTime = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (tag == "NPC" && other.CompareTag("Hazard"))
        {
            //animator.SetBool("isDead", true)
            audio.Play();
            Destroy(gameObject);
            GameObject enemy = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;

        }
        if (tag == "NPC" && other.CompareTag("Car"))
        {
            //play sound
            Destroy(gameObject);
            GameObject enemy = Instantiate(soul, transform.position, Quaternion.identity) as GameObject;
        }
    }
}
