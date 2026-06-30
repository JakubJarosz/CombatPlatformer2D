using UnityEngine;

public class Projectile : MonoBehaviour
{
    private SpriteRenderer projectileSprite;

    private void Awake() {
        projectileSprite = GetComponent<SpriteRenderer>();
    }

    public void SetFacing(float facing) {
        projectileSprite.flipX = facing < 0;
    }
}
