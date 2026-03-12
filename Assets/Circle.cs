using UnityEngine;

public class Circle : MonoBehaviour
{
  public Rigidbody2D rb;
  public float ox;
  public float moveSpeed;
  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    ox = Input.GetAxisRaw("Horizontal");
    rb.linearVelocity = new Vector2(ox * moveSpeed, rb.linearVelocity.y);
  }
}
