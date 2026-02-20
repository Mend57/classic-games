using UnityEngine;

public class SlowProjectile : Projectile
{
    protected override void Update() {
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        alienProjectileOnTrigger(collision);
    }
}
