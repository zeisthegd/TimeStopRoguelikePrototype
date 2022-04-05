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
        public float NormalizeDuration = 1;

        protected List<GameObject> _objectsInRange = new List<GameObject>();

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            Slowable slowableObject = other.gameObject.GetComponent<Slowable>();
            if (Radius > 0 && slowableObject && !_objectsInRange.Contains(other.gameObject))
            {
                _objectsInRange.Add(other.gameObject);
                slowableObject?.Slow(SlowTimeScale);
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (_objectsInRange.Contains(other.gameObject))
            {
                _objectsInRange.Remove(other.gameObject);
                if (other.gameObject.activeInHierarchy)
                    other.gameObject.GetComponent<Slowable>().Normalize(NormalizeDuration);
            }
        }

        public List<GameObject> ObjectsInRange { get => _objectsInRange; }
    }
}
