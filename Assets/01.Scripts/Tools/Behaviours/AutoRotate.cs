using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Tools
{
    public class AutoRotate : MonoBehaviour
    {
        public GameObject GameObjectToRotate;
        public float Speed;

        // Update is called once per frame
        void Update()
        {
            GameObjectToRotate.transform.Rotate(0, 0, Speed * Time.deltaTime);
        }
    }
}

