using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
namespace Penwyn.Game
{
    public class ObjectPool : MonoBehaviour
    {
        [ReadOnly] public List<GameObject> PooledObjects;

        [Button("Enable All Objects", EButtonEnableMode.Always)]
        public virtual void EnableAllObjects()
        {
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                PooledObjects[i].SetActive(true);
            }
        }
        
        [Button("Disable All Objects", EButtonEnableMode.Always)]
        public virtual void DisableAllObjects()
        {
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                PooledObjects[i].SetActive(true);
            }
        }
    }
}

