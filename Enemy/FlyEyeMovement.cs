using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Rigidbody2D rigidbody;
    public GameObject player;
    public LayerMask layer;
    public float speed = 3;
    public bool isAttack=false;
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (player != null)
        {
            Flip();
            Move();
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            //Debug.Log("1111");
            this.GetComponent<Parameter>().Attack(other.gameObject);
        }

    }
    void Flip()
    {
        if (player.transform.position.x < this.transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
    void Move()
    {

        Vector2 v = (player.transform.position - transform.position).normalized;
        rigidbody.velocity = new Vector2(v.x * speed, v.y * speed);
        if (Physics2D.OverlapCircle(this.transform.position, 0.5f, layer))
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
            Invoke("AttackEnd", 0.5f);
        }
    }
    void AttackEnd()
    {
        isAttack = false;
    }
    public void Return()
    {
        //ZombiePool.instance.ReturnPool(this.gameObject);
        FlyEyePool.instance.ReturnPool(this.gameObject);
        Score.instance.score++;
    }
}
