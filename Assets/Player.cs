using System;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb2d;
  private Animator anim;

  // chuyển động - movement
  //[Header("Animation")]
  [Header("Movement")]
  public float trucOx;
  [SerializeField]
  private float tocDo;
  [SerializeField]
  private float jump = 8;
  [SerializeField]
  private bool facingRight = true;
  // run move chạy di chuyển qua trái qua phải
  private bool canMove = true;
  // nhảy
  private bool canJump = true;

  // kiểm tra va chạm
  [Header("Collision")]
  [SerializeField]
  private float groundDistance;
  // có chạm mặt đất => true
  // không chạm mặt đất => false
  [SerializeField]
  private bool isGround;
  [SerializeField]
  private LayerMask groundLayer;     // layer mặt đất

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    anim = GetComponentInChildren<Animator>();
  }

  void Update()
  {
    HadleCollision();
    HandleInput();
    HandleMovement();
    HandleAnimation();
    HadleFlip();
  }

  //EnableJumpAndMovement
  public void KichHoatNhayVaDiChuyen(bool enable)
  {
    canMove = enable;
    canJump = enable;
  }

  private void HandleAnimation()
  {
    //bool isMoving = rb2d.linearVelocity.x != 0;


    anim.SetFloat("xVelocity", rb2d.linearVelocity.x);

    anim.SetBool("isGround", isGround);

    if (!isGround)
    {
      anim.SetFloat("yVelocity", rb2d.linearVelocity.y);
    }

  }

  

  private void HandleInput()
  {
    trucOx = Input.GetAxisRaw("Horizontal");

    if (Input.GetKeyDown(KeyCode.Space))
    {
      // log khi chức năng được kích hoạt
      Debug.Log("Bạn đang nhấn phím Space");
      Jump();
    }

    if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.F))
    {
      Attack();
    }
  }

  private void Attack()
  {
    //anim.SetTrigger("attack");
    if (isGround)
    {
      anim.SetTrigger("attack");
      rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
    }

  }

  private void Jump()
  {
    if (isGround && canJump)
    {
      rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jump);
    }
  }

  private void HandleMovement()
  {
    // chuyển động qua trái qua phải
    if (canMove) // canMove == true
    {
      rb2d.linearVelocity = new Vector2(trucOx * tocDo, rb2d.linearVelocity.y);
    }
    else
    {
      rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
    }
  }

  private void HadleFlip()
  {
    if (rb2d.linearVelocity.x > 0 && facingRight == false)
    {
      Flip();
    }
    else if (rb2d.linearVelocity.x < 0 && facingRight == true)
    {
      Flip();
    }
  }

  private void HadleCollision()
  {
    isGround = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundLayer);
  }

  //[ContextMenu("Flip")]
  private void Flip()
  {
    transform.Rotate(0, 180, 0);
    facingRight = !facingRight;
  }

  // vẽ đường thẳng
  private void OnDrawGizmos()
  {
    Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0, -groundDistance)));
  }

}
