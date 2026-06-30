using UnityEngine;

public class EnemyVisualEvents : MonoBehaviour
{
    [SerializeField] private ProjectileSpawn projectile;

    public void SpawnProjectile() {
        projectile.SpawnProjectile();
    }
}
