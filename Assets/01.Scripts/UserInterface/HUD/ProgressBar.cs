using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using NaughtyAttributes;

public class ProgressBar : MonoBehaviour
{
    public Slider ActualValue;
    public Slider LostValue;
    public float LostDuration = 0.5F;
    public float DelayBeforeDeplete = 0;

    public virtual void SetValue(float newValue)
    {
        if (newValue < ActualValue.value)
            StartCoroutine(DepleteValue(newValue));
        else if (LostValue != null)
            LostValue.value = newValue;

        if (ActualValue != null)
            ActualValue.value = newValue;
    }

    /// <summary>
    /// Slowly show the lost progress.
    /// </summary>
    protected virtual IEnumerator DepleteValue(float newValue)
    {
        LostValue?.DOKill();
        if (DelayBeforeDeplete > 0)
            yield return new WaitForSeconds(DelayBeforeDeplete);
        LostValue?.DOValue(newValue, LostDuration);
    }

    /// <summary>
    /// Set the max values of 2 sliders.
    /// </summary>
    public virtual void SetMaxValue(float newMaxValue)
    {
        if (ActualValue != null)
            ActualValue.maxValue = newMaxValue;
        if (LostValue != null)
            LostValue.maxValue = newMaxValue;
    }

    public virtual void SetWidth(float newWidth)
    {
        Vector2 newSize = new Vector2(newWidth, ActualValue.GetComponent<RectTransform>().sizeDelta.y);
        if (ActualValue != null)
            ActualValue.GetComponent<RectTransform>().sizeDelta = newSize;
        if (LostValue != null)
            LostValue.GetComponent<RectTransform>().sizeDelta = newSize;
    }


}
