using UnityEngine;

public class Enemy : MonoBehaviour
{
  // health point
  public int health = 100;
  // tốc độ
  public float speed = 2f;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  protected virtual void Start()
  {

  }

  // Update is called once per frame
  protected virtual void Update()
  {
    Move();
  }

  protected virtual void Move()
  {
    Debug.Log("chức năng di chuyển");
  }

  public virtual void TakeDamage (int damage)
  {
    health -= damage;

    Debug.Log(name + " bị trừ " + damage + " máu");

    if (health <= 0)
    {
      Die();
    }

  }

  protected virtual void Die()
  {
    Debug.Log(name + " đã chết");
    Destroy(gameObject);
  }

  public void GaySatThuong()
  {

  }
}
