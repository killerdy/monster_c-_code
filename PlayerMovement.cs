using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("speed")]
    public float moveSpeed;
    public float jumpSpeed;
    [Header("Object")]
    public Rigidbody2D rigidbody;
    public Animator animator;
    public GameObject gameObject;
    public LayerMask layer;
    [Header("parameter")]
    public float inputX;
    public float inputY;
    //public float inputY;
    public bool isGround;
    RuntimePlatform platform;
    // Start is called before the first frame update
    void Start()
    {
        platform=Application.platform;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (platform != RuntimePlatform.Android)
            inputX = Input.GetAxisRaw("Horizontal");
        Flip();
        Move();
        Jump();
    }
    private void Flip()
    {
        if (inputX == -1)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (inputX == 1)
            transform.localScale = new Vector3(1, 1, 0);
    }
    private void Move()
    {
        if(rigidbody.velocity.x<3*moveSpeed&&rigidbody.velocity.x>-3*moveSpeed)
        rigidbody.velocity += new Vector2(inputX * moveSpeed, 0);
        animator.SetBool("isGround", isGround);
        animator.SetFloat("Horizontal", rigidbody.velocity.x);
        animator.SetFloat("Vertical", rigidbody.velocity.y);

    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
            JumpEffect();
        isGround = Physics2D.OverlapCircle(transform.position + new Vector3(0, -1.2f, 0), 0.2f, layer);
    }
    public void JumpEffect()
    {
        if (transform.GetComponent<Parameter>().map["magic"] >= 4)
        {
            transform.GetComponent<Parameter>().map["magic"] -= 4;
            rigidbody.velocity += new Vector2(0, jumpSpeed);
        }
    }   
}
