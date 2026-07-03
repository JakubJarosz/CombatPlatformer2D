using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float traverseSpeed;
    [SerializeField] private float projecitileDestroyTime;
    [SerializeField] private LayerMask playerLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer projectileSprite;

    private float projectileDir;
    private int projectileDamage;
    private float destroyTimer;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        projectileSprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        destroyTimer += Time.deltaTime;
        Tryjectory();
        //DestroyProjectile();
    }

    private void Tryjectory() {
        rb.linearVelocity = new Vector2(projectileDir * traverseSpeed, 0f);
    }

    //private void DestroyProjectile() {
    //    if (destroyTimer >= projecitileDestroyTime) {
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    IDamageable hit = collision.GetComponent<IDamageable>();
    //    if (hit != null) {
    //        hit.TakeDamage(projectileDamage);
    //        Destroy(gameObject);
    //    }
    //}

    public void SetFacing(float facing) {
        projectileDir = facing;
        projectileSprite.flipX = projectileDir < 0;
    }

    public void SetDamage(int damage) {
        projectileDamage = damage;
    }
}
