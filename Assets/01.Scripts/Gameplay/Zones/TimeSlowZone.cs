using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowZone : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    [HideInInspector] public float currentRadius = 360;
    [SerializeField] float durationToNormalize;

    float slowTimeScale = 0.5F;

    Collider2D[] objectsInRange = new Collider2D[] { };

    void OnTriggerEnter2D(Collider2D other)
    {
        objectsInRange = Physics2D.OverlapCircleAll((Vector2)this.transform.position, currentRadius);
        if (currentRadius > 0 && objectsInRange != null && objectsInRange.ToList().Contains(other))
        {
            if (other.gameObject.FindComponent<Slowable>())
            {
                other.gameObject.FindComponent<Slowable>()?.Slow(slowTimeScale);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.FindComponent<Slowable>()?.Normalize(durationToNormalize);
    }

    public float SlowTimeScale { get => slowTimeScale; set => slowTimeScale = value; }
    public Collider2D[] ObjectsInRange { get => objectsInRange; set => objectsInRange = value; }
}
