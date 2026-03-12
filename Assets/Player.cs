using System;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb2d;
  private Animator anim;
  public float trucOx;
  [SerializeField]
  private float tocDo;
  [SerializeField]
  private float jump = 8;
  [SerializeField]
  private bool facingRight = true;

  void Awake()
  {
    rb2d = GetComponent<Rigidbody2D>();
    anim = GetComponentInChildren<Animator>();
  }

  void Update()
  {
    HandleInput();
    HandleMovement();
    HandleAnimation();
    HadleFlip();
  }

  private void HandleAnimation()
  {
    bool isMoving = rb2d.linearVelocity.x != 0;

    anim.SetBool("isMoving", isMoving);
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
    // dòng code chủ yếu của jump
    rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jump);
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

  //[ContextMenu("Flip")]
  private void Flip()
  {
    transform.Rotate(0, 180, 0);
    facingRight = !facingRight;
  }

}
