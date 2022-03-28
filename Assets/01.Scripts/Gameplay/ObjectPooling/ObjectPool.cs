using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
namespace Penwyn.Game
{
    public class ObjectPool : MonoBehaviour
    {
        [ReadOnly] public List<PoolableObject> PooledObjects;
    }
}

