using System;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb2d;
  private Animator anim;

  // chuyển động
  [Header("Animation")]
  public float trucOx;
  [SerializeField]
  private float tocDo;
  [SerializeField]
  private float jump = 8;
  [SerializeField]
  private bool facingRight = true;

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
    
  }

  private void HandleMovement()
  {
    // chuyển động qua trái qua phải
    rb2d.linearVelocity = new Vector2(trucOx * tocDo, rb2d.linearVelocity.y);
  }

  private void Jump()
  {
    if (isGround)
    {
      rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jump);
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
