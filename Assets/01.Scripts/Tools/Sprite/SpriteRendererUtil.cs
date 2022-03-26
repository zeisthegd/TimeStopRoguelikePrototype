using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRendererUtil
{
    public static IEnumerator Flicker(SpriteRenderer spriteRenderer, Color color, float duration, float interval = 0.05F)
    {
        float _duration = 0;
        while (_duration < duration)
        {
            _duration += Time.deltaTime;
            spriteRenderer.material.color = color;
            yield return new WaitForSeconds(interval);
            spriteRenderer.material.color = Color.white;
        }
        spriteRenderer.material.color = Color.white;
    }
}
