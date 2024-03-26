using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private bool flag = false;
    public float repal = 3;
    public GameObject owner;
    // Start is called before the first frame update
    public void Init(GameObject attacker, float speed)
    {
        flag = false;
        this.transform.localScale = attacker.transform.localScale;
        this.transform.position = attacker.transform.position + new Vector3(this.transform.localScale.x, -0.5f, 0);
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(this.transform.localScale.x, 0, 0) * speed;
        owner= attacker;
        Invoke("Explosin", 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            owner.GetComponent<Parameter>().Attack(collision.gameObject);
            collision.GetComponent<Parameter>().Hit();
        }
        if (flag)//爆炸后
        {
            //if (collision.GetComponent<Rigidbody2D>())//击退效果
            //{
            //    //var velocity = collision.GetComponent<Rigidbody2D>().velocity;
            //    Vector2 v2 = (collision.transform.localPosition - this.transform.localPosition).normalized;
            //    //collision.GetComponent<Rigidbody2D>().velocity += v2 * repal;
            //    collision.GetComponent<Rigidbody2D>().velocity += new Vector2(v2.x * repal, v2.y * repal);
            //    //Debug.Log(v2);
            //}
            if (collision.CompareTag("bullet"))//连锁反应
                collision.GetComponent<FireBall>().Explosin();
           
      
        }
        else
        {
            if (collision.CompareTag("Player") || collision.CompareTag("bullet")) ;
            else
                if(collision.CompareTag("Enemy"))
                Explosin();
        }
    }
    public void Explosin()
    {
        flag = true;
        this.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Animator>().SetTrigger("explosin");

    }
    void End()
    {
        FireBallsPool.instance.ReturnPool(this.gameObject);
    }
    // Update is called once per frame

}
