using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //components 
    private Rigidbody2D rb2d;
    private Animator animator;   //let PlayerAttack(child) access but not other classes
    
    //movement
    private float moveSpeed = 5f;
    private Vector2 movement;
    private bool faceR = true;

    //attack
    private float timeLeftTillAttack;
    public float resetAttackCooldown;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    //public float attackRangeX;
    //public float attackRangeY;

    //scoring
    private float p1Score = 0;
    private float p2Score = 0;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (name == "Player1")
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("speed", Mathf.Abs(movement.x));

            if (timeLeftTillAttack <= 0)
            {
                if (Input.GetButtonDown("Attack") && !animator.GetBool("isAttacking"))
                {
                    animator.SetBool("isAttacking", true);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        Destroy(enemiesToDamage[i].gameObject);
                        p1Score++;
                        Debug.Log(p1Score);
                    }
                    timeLeftTillAttack = resetAttackCooldown;
                }
                else if (Input.GetButtonDown("Attack") && animator.GetBool("isAttacking"))
                {
                    animator.SetBool("isAttacking", false);
                }
            }
            else
            {
                timeLeftTillAttack -= Time.deltaTime;
            }
        }
        else if(name == "Player2")
        {
            movement.x = Input.GetAxisRaw("Horizontal2");
            movement.y = Input.GetAxisRaw("Vertical2");
            animator.SetFloat("speed", Mathf.Abs(movement.x));

            if (timeLeftTillAttack <= 0) //attack
            {
                if (Input.GetButtonDown("Attack2") && !animator.GetBool("isAttacking"))
                {
                    animator.SetBool("isAttacking", true);
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    for( int i=0; i<enemiesToDamage.Length; i++)
                    {
                        Destroy(enemiesToDamage[i]);
                        p2Score++;
                        Debug.Log(p2Score);
                    }
                    timeLeftTillAttack = resetAttackCooldown;
                }
                else if (Input.GetButtonDown("Attack2") && animator.GetBool("isAttacking"))
                {
                    animator.SetBool("isAttacking", false);
                }
            }
            else
            {
                timeLeftTillAttack -= Time.deltaTime;
            }
        }

        if (faceR == false && movement.x > 0 || faceR == true && movement.x < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        faceR = !faceR;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        //Gizmos.DrawWireCube(attackPos.position, new Vector2(attackRangeX, attackRangeY));
    }
}
