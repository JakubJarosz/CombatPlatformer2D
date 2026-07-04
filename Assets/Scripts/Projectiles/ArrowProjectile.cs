using UnityEngine;

public class ArrowProjectile : ProjectileBase {

    [SerializeField] private float traverseSpeed;

    protected override void Update() {
        base.Update();
        Tryjectory();
    }

    private void Tryjectory() {
        rb.linearVelocity = new Vector2(projectileDir * traverseSpeed, 0f);
    }
}
