using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] protected float projecitileDestroyTime;
    [SerializeField] private LayerMask playerLayer;

    protected Rigidbody2D rb;
    protected Collider2D col;
    private SpriteRenderer projectileSprite;

    protected float projectileDir;
    protected int projectileDamage;
    protected float destroyTimer;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        projectileSprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update() {
        destroyTimer += Time.deltaTime;
        DestroyProjectile();
    }

    private void DestroyProjectile() {
        if (destroyTimer >= projecitileDestroyTime) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        IDamageable hit = collision.GetComponent<IDamageable>();
        if (hit != null) {
            hit.TakeDamage(projectileDamage);
            OnProjectileHit();
        }
    }

    protected virtual void OnProjectileHit() {
        Destroy(gameObject);
    }

    public void SetDamage(int damage) {
        projectileDamage = damage;
    }

    public void SetFacing(float facing) {
        projectileDir = facing;
        projectileSprite.flipX = projectileDir < 0;
    }
}
