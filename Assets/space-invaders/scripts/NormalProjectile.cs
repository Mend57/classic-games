using UnityEngine;

public class NormalProjectile : Projectile
{
    protected override void Update() {
        base.Update();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        alienProjectileOnTrigger(collision);
    }
}
