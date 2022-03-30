using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererUtil
{
    public static IEnumerator Flicker(SpriteRenderer spriteRenderer, Color color, float duration, float interval = 0.05F)
    {
        float _duration = 0;
        Color baseColor = spriteRenderer.color;
        while (_duration < duration)
        {
            _duration += Time.deltaTime + interval * 2;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(interval);
            spriteRenderer.color = baseColor;
            yield return new WaitForSeconds(interval);
        }
        spriteRenderer.color = baseColor;
    }
}
