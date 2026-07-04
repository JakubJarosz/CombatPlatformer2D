using UnityEngine;

public class DeathHandProjectile : ProjectileBase
{
    protected override void Update() {
        base.Update();
        EnableCollision();
    }

    private void EnableCollision() {
        float enableTimer = projecitileDestroyTime * 0.2f; // enable collision after 30% of animation plays out
        if (enableTimer <= destroyTimer) {
            col.enabled = true;
        }
    }

    protected override void OnProjectileHit() {
        
    }
}
