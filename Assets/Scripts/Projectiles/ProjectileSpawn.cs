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
                Vector2 posStat = (Vector2)transform.position + new Vector2(xOffset * detection.FacingDir(), yOffset);
                Projectile projectileStat = Instantiate(projectilePrefab, posStat, Quaternion.identity).GetComponent<Projectile>();
                projectileStat.SetFacing(detection.FacingDir());
                projectileStat.SetDamage(attackDamage);
                break;  
            case SpawnType.Tracking:
                if (detection.IsPlayerDetected()) {
                    Vector2 posTrack = new Vector2(detection.GetDetectedPlayer().position.x, yOffset);
                    Projectile projectileTrack = Instantiate(projectilePrefab, posTrack, Quaternion.identity).GetComponent<Projectile>();
                    projectileTrack.SetDamage(attackDamage);
                }
                break;
        }
    }
}
