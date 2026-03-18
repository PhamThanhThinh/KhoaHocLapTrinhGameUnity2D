using NUnit.Framework;
using System;
//using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private Rigidbody2D rb2d;
  private Animator anim;

  public Collider2D[] colliders;
  //public List<Collider2D> viDuListCollider;

  // phạm vi sát thương
  // bán kính sát thương
  [Header("Attack Detail")]

  // dùng cho quái vật
  [SerializeField]
  private float banKinhGaySatThuong;
  [SerializeField]
  private float attackPointCach1;
  [SerializeField]
  private Transform attackPoint;
  
  //private float diemTanCong;

  // phát hiện đối tượng tấn công
  // phát hiện kẻ địch
  // phát hiện quái vật
  [SerializeField]
  private LayerMask phatHienQuaiVat;

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

  // gây sát thương lên kẻ địch
  // DamageEnemies
  //[ContextMenu("GaySatThuongLenKeDich")]
  public void GaySatThuongLenKeDich()
  {
    //    // default damage applied when calling message handlers
    //    float damage = 1f;

    //    // compute detection center: player's position + facing direction * attackPoint
    //    Vector2 center = (Vector2)transform.position + (Vector2)transform.right * attackPoint;

    //    // find all colliders in the attack radius that belong to the enemy layer mask
    //    colliders = Physics2D.OverlapCircleAll(center, banKinhGaySatThuong, phatHienQuaiVat);

    //    if (colliders == null || colliders.Length == 0)
    //    {
    //      return;
    //    }

    //    // notify each hit object
    //    foreach (var col in colliders)
    //    {
    //      if (col == null) continue;

    //      var go = col.gameObject;

    //      // Try common method names used by enemy scripts. Use DontRequireReceiver to avoid errors
    //      go.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
    //      go.SendMessage("ReceiveDamage", damage, SendMessageOptions.DontRequireReceiver);
    //      go.SendMessage("OnHit", SendMessageOptions.DontRequireReceiver);
    //    }

    //#if UNITY_EDITOR
    //    // helpful debug info in editor (can be removed or guarded by a debug flag)
    //    Debug.Log($"Attack detected {colliders.Length} target(s) at {center} with radius {banKinhGaySatThuong}.");
    //#endif


    // default damage applied when calling message handlers
    //float damage = 1f;

    // Cách 1:
    // compute detection center: player's position + facing direction * attackPoint
    // Vector2 center = (Vector2)transform.position + (Vector2)transform.right * attackPointCach1;

    // colliders = Physics2D.OverlapCircleAll(center, banKinhGaySatThuong, phatHienQuaiVat);

    // Cách 2:
    // chỉ nhận enemy một lần (một lần có thể có nhiều enemy)
    // colliders = Physics2D.OverlapCircleAll(attackPoint.position, banKinhGaySatThuong, phatHienQuaiVat);

    // Cách 3:
    Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, banKinhGaySatThuong, phatHienQuaiVat);

    foreach (Collider2D item in colliders)
    {
      item.GetComponent<Enemy>().GaySatThuong();
    }

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

  //// vẽ đường thẳng
  //private void OnDrawGizmos()
  //{
  //  Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0, -groundDistance)));
  //}

  // vẽ đường thẳng
  private void OnDrawGizmos()
  {
    Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0, -groundDistance)));
    Gizmos.DrawWireSphere(attackPoint.position, banKinhGaySatThuong);
  }

}
