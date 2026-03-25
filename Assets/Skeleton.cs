using UnityEngine;

public class Skeleton : Enemy
{
  // Start is called once before the first execution of Update after the MonoBehaviour is created
  protected override void Start()
  {
    base.Start();
    // health point
    health = 50;
    // tốc độ
    speed = 2.5f;
  }
}
