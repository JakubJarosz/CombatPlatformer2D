using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private EnemyDetection detection;
    public SpawnType spawnType;

    [Header("Spawn Position")]
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    public enum SpawnType {
        Static,
        Tracking
    }

    public void SpawnProjectile() {
        switch(spawnType) {
            case SpawnType.Static:
                Vector2 pos = (Vector2)transform.position + new Vector2(xOffset * detection.FacingDir(), yOffset);
                GameObject projectile = Instantiate(projectilePrefab, pos, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetFacing(detection.FacingDir());
                break;  
            case SpawnType.Tracking:
                break;
        }
    }
}
