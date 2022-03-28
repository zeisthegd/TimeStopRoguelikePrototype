using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

namespace Penwyn.Game
{
    public class PoolableObject : MonoBehaviour
    {
        [HorizontalLine]
        [Header("Poolable Object")]
        public float LifeTime = 1;

        public virtual void Destroy()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnEnable()
        {
            if (LifeTime > 0)
            {
                Invoke("Destroy", LifeTime);
            }
        }

        public virtual void OnDisable()
        {
            CancelInvoke();
        }
    }
}

