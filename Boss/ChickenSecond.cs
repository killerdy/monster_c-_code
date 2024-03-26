using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenSecond : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Rigidbody2D rigidbody;
    public GameObject player;
    public LayerMask layer;
    public float speed = 3;
    public bool isAttack;

    public void Init()
    {
        this.GetComponent<Parameter>().map["health"] *= 250;
        this.GetComponent<Parameter>().map["attack"] *= 3;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        //transform.localScale = new Vector3(0.5f, 0.5f, 1);
        if (player != null)
        {
            Flip();
            Move();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("okokokokokok");
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Parameter>().Attack(other.gameObject);
        }

    }
    void Flip()
    {
        if (player.transform.position.x < this.transform.position.x)
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        else
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
    }
    void Move()
    {
        //int x = UnityEngine.Random.Range(0, 5);
        Vector2 v = (player.transform.position - transform.position);
        if (v.x > 2 || v.x < -2)
        {
            if (rigidbody.velocity.x < 3 * speed && rigidbody.velocity.x > -3 * speed)
                rigidbody.velocity += new Vector2(v.normalized.x * speed, 0);
        }
        //else
        //    rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
        animator.SetFloat("Horizontal", rigidbody.velocity.x);
        if (Physics2D.OverlapCircle(this.transform.position, 3f, layer))
        {
            rigidbody.velocity = new Vector2(0, 0);
            Attack();
        }

    }
    void Attack()
    {
        if (!isAttack && transform.GetComponent<Parameter>().map["health"] > 0)
        {
            isAttack = true;
            animator.SetTrigger("Attack");
            Invoke("AttackEnd", 0.8f);
        }
    }
    void AttackEnd()
    {
        isAttack = false;
    }
    public void Return()
    {
        //ZombiePool.instance.ReturnPool(this.gameObject);
        //BossChickenPool.instance.ReturnPool(this.gameObject);
        ChickSecondPool.instance.ReturnPool(this.gameObject);
        Score.instance.score += 30; 
    }
}
