using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("弹幕参数")]
    public float fireBallSpeed;
    private bool isAttack;
    public Animator animator;
    public GameObject attacker;
    RuntimePlatform platform;
    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("Reply", 0, 1f);
        //attacker=GetComponent<GameObject>();
        platform = Application.platform;
    }
    void Reply()
    {
        transform.GetComponent<Parameter>().map["magic"] += transform.GetComponent<Parameter>().map["maxMagic"] / 50;
        transform.GetComponent<Parameter>().map["health"] += transform.GetComponent<Parameter>().map["maxHealth"] / 100;
        transform.GetComponent<Parameter>().map["magic"] = Mathf.Min(transform.GetComponent<Parameter>().map["magic"], transform.GetComponent<Parameter>().map["maxMagic"]);
        transform.GetComponent<Parameter>().map["health"] = Mathf.Min(transform.GetComponent<Parameter>().map["health"], transform.GetComponent<Parameter>().map["maxHealth"]);
    }
    // Update is called once per frame
    void Update()
    {
        if (platform != RuntimePlatform.Android)
            Attack();
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            isAttack = true;
            animator.SetTrigger("Attack");
            Invoke("AttackEnd", 3);
        }
        if (Input.GetKeyDown(KeyCode.L) && !isAttack)
        {
            if (transform.GetComponent<Parameter>().map["magic"] >= 10)
            {
                transform.GetComponent<Parameter>().map["magic"] -= 10;
                //isAttack = true;
                animator.SetTrigger("Attack");
                for (var i = 0; i < 15; i++)
                    FireBalls();
                //Invoke("AttackEnd", 3);
            }

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (transform.GetComponent<Parameter>().map["magic"] >= transform.GetComponent<Parameter>().map["maxMagic"] * 0.2f)
            {
                transform.GetComponent<Parameter>().map["magic"] -= transform.GetComponent<Parameter>().map["maxMagic"] * 0.2f;
                animator.SetTrigger("Kick");
            }

        }
    }
    void AttackEnd()
    {
        isAttack = false;
    }
    public void MobileKick()
    {
        if (transform.GetComponent<Parameter>().map["magic"] >= transform.GetComponent<Parameter>().map["maxMagic"] * 0.2f)
        {
            transform.GetComponent<Parameter>().map["magic"] -= transform.GetComponent<Parameter>().map["maxMagic"] * 0.2f;
            animator.SetTrigger("Kick");
        }
    }
    public void MobileAttack()
    {
        if (!isAttack)
        {
            isAttack = true;
            animator.SetTrigger("Attack");
            Invoke("AttackEnd", 3);
        }
    }
    public void MobileAttacks()
    {
        if (!isAttack)
        {
            if (transform.GetComponent<Parameter>().map["magic"] >= 10)
            {
                transform.GetComponent<Parameter>().map["magic"] -= 10;
                animator.SetTrigger("Attack");
                for (var i = 0; i < 15; i++)
                    FireBalls();
            }
        }
    }
    public void FireBalls()
    {
        GameObject fireball = FireBallsPool.instance.GameFromPool();
        fireball.GetComponent<FireBall>().Init(this.gameObject, fireBallSpeed);

        //fireball.transform.localScale = this.transform.localScale;
        //fireball.transform.position = this.transform.position + new Vector3(fireball.transform.localScale.x, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!transform.GetComponent<BoxCollider2D>().isActiveAndEnabled)
            return;
        if (collision.CompareTag("Enemy"))
        {
            this.GetComponent<Parameter>().Attack(collision.gameObject);
            collision.GetComponent<Parameter>().Hit();
        }
        if (collision.GetComponent<Rigidbody2D>())//击退效果
        {
            //var velocity = collision.GetComponent<Rigidbody2D>().velocity;
            //collision.GetComponent<Rigidbody2D>().velocity += v2 * repal;
            collision.GetComponent<Rigidbody2D>().velocity += new Vector2(transform.localScale.x * 100,0);
        }
    }
}
