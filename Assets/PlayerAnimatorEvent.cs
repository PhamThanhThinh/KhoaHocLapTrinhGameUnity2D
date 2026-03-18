using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimatorEvent : MonoBehaviour
{
  private Player player;
  private void AttackStart()
  {
    Debug.Log("test attack");
  }

  public void Awake()
  {
    player = GetComponentInParent<Player>();
  }

  private void VoHieuHoaNhayVaDiChuyen()
  {
    player.KichHoatNhayVaDiChuyen(false);
  }
  private void KichHoatNhayVaDiChuyen()
  {
    player.KichHoatNhayVaDiChuyen(true);
  }

  public void GaySatThuongLenKeDich()
  {
    player.GaySatThuongLenKeDich();
  }  

}
