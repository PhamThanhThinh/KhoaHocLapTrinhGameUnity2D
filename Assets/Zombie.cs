using UnityEngine;

public class Zombie : Enemy
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  protected override void Start()
  {
    base.Start();
    // health point
    health = 150;
    // tốc độ
    speed = 1.5f;
  }
}
