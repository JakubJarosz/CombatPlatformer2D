using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [Header("Ground detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundRadius;

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, groundRadius);
    }

    public bool IsGrounded() {
        return Physics2D.OverlapCircle(transform.position, groundRadius, groundLayer);
    }
}
