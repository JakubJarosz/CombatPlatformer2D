using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [Header("Detection Zone")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector2 detectionSize;

    [Header("Range Zone")]
    [SerializeField] private Vector2 rangeSize;

    [Header("Melee Zone")]
    [SerializeField] private Vector2 meleeSize;

    private void OnDrawGizmosSelected() {
        // Detection Zone
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, detectionSize);
        // Range Zone
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, rangeSize);
        // Melee Zone
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, meleeSize);
    }

    public bool IsPlayerDetected() {
        return Physics2D.OverlapBox(transform.position, detectionSize, 0f, playerLayer);
    }

    public float FacingDir() {
        Collider2D player = Physics2D.OverlapBox(transform.position, detectionSize, 0f, playerLayer);
        if (player == null) return 0.0f;
        return player.transform.position.x < transform.position.x ? -1 : 1;
    }

    public bool IsInRangeRange() {
        return Physics2D.OverlapBox(transform.position, rangeSize, 0f, playerLayer);
    }

    public bool IsInMeleeRange() {
        return Physics2D.OverlapBox(transform.position, meleeSize, 0f, playerLayer);
    }
}
