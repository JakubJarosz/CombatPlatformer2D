using UnityEngine;

public class EnemyVisualEvents : MonoBehaviour
{
    [SerializeField] private ProjectileSpawn projectile;
    [SerializeField] private EnemyMeleeHitBox meleeHitBox;

    public void SpawnProjectile() {
        projectile.SpawnProjectile();
    }

    public void MeleeHit() {
        meleeHitBox.MeleeHit();
    }
}
