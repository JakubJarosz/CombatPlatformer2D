using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float traverseSpeed;
    private SpriteRenderer projectileSprite;

    private float projectileDir;

    private void Awake() {
        projectileSprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        Tryjectory();
    }

    private void Tryjectory() {
        transform.position += new Vector3(traverseSpeed * projectileDir, transform.position.y);
    }

    private void ProjectileHit() {

    }

    public void SetFacing(float facing) {
        projectileDir = facing;
        projectileSprite.flipX = projectileDir < 0;
    }
}
