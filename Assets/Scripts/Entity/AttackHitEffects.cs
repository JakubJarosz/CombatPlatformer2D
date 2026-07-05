using System;
using System.Collections;
using UnityEngine;

public class AttackHitEffects : MonoBehaviour {

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public event Action<bool> IsKnocked;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void ApplyEffects(Vector2 dir, float force, float knockTime, float hitstopTime, float shakeDur, float shakeStrangth) {
        StartCoroutine(ApplyKnockback(dir, force, knockTime));
        StartCoroutine(ApplyHitstop(hitstopTime));
        StartCoroutine(ApplyChangeColor());
        StartCoroutine(ApplySquash());
        StartCoroutine(ApplyCameraShake(shakeDur, shakeStrangth));
    }

    // Knocback Effect
    private IEnumerator ApplyKnockback(Vector2 dir, float force, float knockTime) {
        IsKnocked?.Invoke(true);
        rb.AddForce(dir * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockTime);
        IsKnocked?.Invoke(false);
    }

    // Hitstop Effect
    private IEnumerator ApplyHitstop(float time) {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }

    // Color change Effect
    private IEnumerator ApplyChangeColor() {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.white;
    }

    // Squash Effect
    private IEnumerator ApplySquash() {
        transform.localScale = new Vector3(1.1f, 0.9f, 1f);
        yield return new WaitForSeconds(0.05f);
        transform.localScale = Vector3.one;
    }

    // Camera shake
    private IEnumerator ApplyCameraShake(float duration, float strength) {
        Vector3 originalPos = Camera.main.transform.position;

        float time = 0;
        while (time < duration) {
            Camera.main.transform.position = originalPos + (Vector3)UnityEngine.Random.insideUnitCircle * strength;
            time += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = originalPos;
    }
}
