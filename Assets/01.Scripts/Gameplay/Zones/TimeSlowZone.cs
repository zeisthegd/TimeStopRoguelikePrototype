using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;


namespace Penwyn.Game
{
    public class TimeSlowZone : MonoBehaviour
    {
        [ReadOnly] public float Radius = 360;
        [ReadOnly] public float SlowTimeScale = 0.5F;
        public float DurationToNormalize = 1;

        protected Collider2D[] _ObjectsInRange = new Collider2D[] { };

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            _ObjectsInRange = Physics2D.OverlapCircleAll((Vector2)this.transform.position, Radius);
            if (Radius > 0 && _ObjectsInRange != null && _ObjectsInRange.ToList().Contains(other))
            {
                if (other.gameObject.FindComponent<Slowable>())
                {
                    other.gameObject.FindComponent<Slowable>()?.Slow(SlowTimeScale);
                }
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.activeInHierarchy)
                other.gameObject.FindComponent<Slowable>()?.Normalize(DurationToNormalize);
        }

        public Collider2D[] ObjectsInRange { get => _ObjectsInRange; }
    }
}
