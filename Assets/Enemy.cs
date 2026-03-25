using UnityEngine;

public class Enemy : MonoBehaviour
{
  // health point
  [SerializeField] private int health = 100;
  // tốc độ
  [SerializeField] private float speed = 2f;

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

  // method lấy dữ liệu
  public int GetHealth() { return health; }
  public float GetSpeed() { return speed; }

  // method cập nhật dữ liệu
  protected void SetHealth(int value)
  {
    health = value;
  }

  protected void SetSpeed(float value)
  {
    speed = value;
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
