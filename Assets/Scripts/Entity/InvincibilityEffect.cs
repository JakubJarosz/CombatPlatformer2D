using System.Collections;
using UnityEngine;

public class InvincibilityEffect : MonoBehaviour
{
    public void StartInvFrames(SpriteRenderer sprite, float duration) {
        StartCoroutine(InvincibilityCoroutine(sprite, duration)); // visual effect
    }

    private IEnumerator InvincibilityCoroutine(SpriteRenderer sprite, float duration) {
        float timer = 0f;

        float startInterval = 0.12f; // slow at start
        float endInterval = 0.03f; // fast at end

        bool visible = true;
        while (timer < duration) {
            float t = timer / duration; // progress from 0 to 1
            float interval = Mathf.Lerp(startInterval, endInterval, t);

            visible = !visible;
            sprite.enabled = visible;
            yield return new WaitForSeconds(interval);

            timer += interval;
        }
        sprite.enabled = true;
    }
}
