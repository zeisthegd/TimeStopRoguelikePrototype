using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using NaughtyAttributes;

namespace Penwyn.Game
{
    public class ObjectPooler : MonoBehaviour
    {
        [Header("Pool Object")]
        public PoolableObject ObjectToPool;
        public int Size = 1;

        [HorizontalLine]
        public bool NestPoolBelowThis = true;
        public bool NestObjectsInPool = true;

        protected GameObject _waitingPool;
        protected ObjectPool _objectPool;

        [Button("Init Pool", EButtonEnableMode.Always)]
        public virtual void Init()
        {
            CreatePool();
            FillPool();
        }

        /// <summary>
        /// Instantiate a new pool if there's none created yet.
        /// </summary>
        public virtual void CreatePool()
        {
            if (_waitingPool == null)
            {
                _waitingPool = new GameObject(DefinePoolName());
                SceneManager.MoveGameObjectToScene(_waitingPool, this.gameObject.scene);
                _objectPool = _waitingPool.AddComponent<ObjectPool>();
                _objectPool.PooledObjects = new List<PoolableObject>();
                ApplyNesting();
            }
        }

        /// <summary>
        /// Fill pool till its full.
        /// </summary>
        public virtual void FillPool()
        {
            for (int i = 0; i < Size; i++)
            {
                AddOnePoolObject();
            }
        }

        public virtual PoolableObject PullOneObject()
        {
            foreach (PoolableObject item in _objectPool.PooledObjects)
            {
                if (!item.gameObject.activeInHierarchy)
                    return item;
            }
            return AddOnePoolObject();
        }

        /// <summary>
        /// Instantiate the object to pool. Then add it to the list.
        /// </summary>
        public virtual PoolableObject AddOnePoolObject()
        {
            if (ObjectToPool != null)
            {
                PoolableObject poolObject = (PoolableObject)Instantiate(ObjectToPool);
                poolObject.gameObject.SetActive(false);
                poolObject.transform.SetParent(NestObjectsInPool ? _objectPool.transform : null);
                _objectPool.PooledObjects.Add(poolObject);
                return poolObject;
            }
            Debug.LogWarning("No Object to pool. Please insert");
            return null;
        }

        /// <summary>
        /// Determine the parent of this pool.
        /// </summary>
        public virtual void ApplyNesting()
        {
            if (_waitingPool)
            {
                _waitingPool.transform.SetParent(NestPoolBelowThis ? this.transform : null);
            }
        }

        /// <summary>
        /// Type of pool and name of ObjectToPool.
        /// </summary>
        public virtual string DefinePoolName()
        {
            return $"[{this.GetType().ToString()}] +[{ObjectToPool.name}]";
        }
    }
}

