using System.Threading;
using UnityEngine;

public class Cooldown_Enemy : MonoBehaviour
{
  private SpriteRenderer sr;

  [SerializeField]
  private float thoiGian = 0.5f;

  private void Awake()
  {
    sr = GetComponent<SpriteRenderer>();
  }

  public float time;

  private void Update()
  {
    // frame khung hình
    Debug.Log(Time.deltaTime);

    // bộ đếm giờ, đếm ngược
    if (time > 0f)
    {
      time -= Time.deltaTime;

      // kết thúc thời gian bất tử
      if (time <= 0f)
      {
        time = 0f;
        ResetColor();
      }
    }
  }

  // thời gian player hồi chiêu
  // thời gian enemy có thể nhận thêm sát thương
  // trong thời gian đếm ngược thì enemy không nhận thêm sát thương
  // cơ chế bất tử

  [ContextMenu("UpdateTime")]
  private void UpdateTime()
  {
    time = thoiGian;
  }

  // take damage
  public void GaySatThuong()
  {
    // Nếu đang bất tử thì bỏ qua
    if (time > 0f)
    {
      Debug.Log(gameObject.name + " is invulnerable, damage ignored.");
      return;
    }

    Debug.Log(gameObject.name + " sát thương vật lý!");
    sr.color = Color.red;

    // Bắt đầu thời gian bất tử
    time = thoiGian;
  }

  private void ResetColor()
  {
    sr.color = Color.white;
  }

}
