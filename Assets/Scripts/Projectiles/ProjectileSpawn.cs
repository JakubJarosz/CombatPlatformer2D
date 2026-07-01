using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private EnemyDetection detection;
    private EnemyAttack enemyAttack;
    public SpawnType spawnType;

    [Header("Spawn Position")]
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;

    public enum SpawnType {
        Static,
        Tracking
    }

    private int attackDamage;

    private void Awake() {
        enemyAttack = GetComponentInParent<EnemyAttack>();
    }

    private void Start() {
        enemyAttack.PerformRangeAttack += GetAttackDamage;
    }

    private void GetAttackDamage(int obj) {
        attackDamage = obj;
    }

    public void SpawnProjectile() {
        switch(spawnType) {
            case SpawnType.Static:
                Vector2 pos = (Vector2)transform.position + new Vector2(xOffset * detection.FacingDir(), yOffset);
                Projectile projectile = Instantiate(projectilePrefab, pos, Quaternion.identity).GetComponent<Projectile>();
                projectile.SetFacing(detection.FacingDir());
                projectile.SetDamage(attackDamage);
                break;  
            case SpawnType.Tracking:
                break;
        }
    }
}
