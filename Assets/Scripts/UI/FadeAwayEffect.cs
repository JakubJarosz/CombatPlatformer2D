using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayEffect : MonoBehaviour
{
    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public IEnumerator FadeOut(float duration) {
        float elapsed = 0f;
        Color c = image.color;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            c.a = Mathf.Lerp(1f, 0f, t);

            image.color = c;

            yield return null;
        }

        image.color = c;
    }

    public IEnumerator FadeIn(float duration) {
        float elapsed = 0f;
        Color c = image.color;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            c.a = Mathf.Lerp(0f, 1f, t);

            image.color = c;

            yield return null;
        }

        image.color = c;
    }
}
