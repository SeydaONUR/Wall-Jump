using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [Header("Simple Movement")]
    public Rigidbody2D rb;
    public float moveSpeed;
    public Transform groundPoint;
    public LayerMask ground;
    public float jumpForce;
    private bool onGround;
    [Header("Slide")]
    private bool isSliding;
    private bool isTouchWall;
    public float slidingSpeed;
    public Transform wallCheck;
    public LayerMask wallLayer;
    [Header("Wall Jump")]
    public bool isWallJump;
    public float jumpDirection;
    public Vector2 wallJumpForce;
    public float wallJumpTime;
    private float counter;

    [Header("Dash")]
    private bool canDash;
    private bool isDashing;
    [SerializeField]private float dashPower;
    // Start is called before the first frame update
    void Start()
    {
        canDash = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onGround == false)
        {
            canDash = true;
        }
        else
        {
            canDash = false;
            isDashing = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash==true)
        {
            isDashing = true;
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * dashPower, rb.velocity.y);
        }
        if (!isDashing)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        }
       /* else
        {
          rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * dashPower, rb.velocity.y) ;

        }*/

        if (rb.velocity.x>0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (rb.velocity.x<0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        onGround = Physics2D.OverlapCircle(groundPoint.position,.2f,ground);

        if (onGround==true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        //Debug.Log(-jumpDirection * wallJumpForce.x);
        //Wall Slide

        isTouchWall = Physics2D.OverlapCircle(wallCheck.position,.2f,wallLayer);
        if (isTouchWall ==true && onGround==false && rb.velocity.x!=0)
        {
            isSliding = true;
        }else
        {
            isSliding = false;
            isWallJump = false;
        }
        jumpDirection = transform.localScale.x;
        //Debug.Log(rb.velocity.x);
        if (isSliding == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -slidingSpeed, float.MaxValue));
            isWallJump = true;
        }
            /*counter += Time.deltaTime;
        }
        if (counter >= wallJumpTime)
        {
            isWallJump = true;
        }*/
        

        if (isWallJump==true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(-jumpDirection * wallJumpForce.x, wallJumpForce.y);
            //counter = 0;
            isWallJump = false;
            
            

        }
    }
    /*public void Dashing()
    {

        isDashing = true;
       
        if (isDashing)
        {
            Debug.Log("Dashing");
            //rb.gravityScale = 0;
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * dashPower, rb.velocity.y) ;
            isDashing = false;
        }
    }*/
    /*public void wallJump()
    {
        isWallJump = true;
        if (isWallJump)
        {
            rb.velocity = new Vector2(-jumpDirection*wallJumpForce.x,wallJumpForce.y);
        }
    }*/
}
