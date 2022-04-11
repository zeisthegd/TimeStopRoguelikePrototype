using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;

namespace Penwyn.Tools
{
    [RequireComponent(typeof(ObjectPooler))]
    public class CharacterGhostTrail : MonoBehaviour
    {
        public GameObject GhostPrefab;
        public ObjectPooler GhostPooler;
        public float Delay = 1F;
        public SortingLayer Layer;
        public Color Color;
        public Material Material;

        protected float _delta;
        protected SpriteRenderer _targetRenderer;

        protected virtual void Awake()
        {
            _targetRenderer = GetComponent<SpriteRenderer>();
            if (GhostPrefab != null)
            {
                GhostPooler.ObjectToPool = GhostPrefab;
                GhostPooler.Init();
            }
        }

        protected virtual void Update()
        {
            if (_delta > 0)
            {
                _delta -= Time.deltaTime;
            }
            else
            {
                _delta = Delay;
                CreateGhost();
            }
        }

        public virtual void CreateGhost()
        {
            GameObject ghostObject = GhostPooler.PullOneObject().gameObject;
            ghostObject.gameObject.SetActive(true);
            ghostObject.transform.localScale = _targetRenderer.gameObject.transform.localScale;
            ghostObject.transform.position = _targetRenderer.gameObject.transform.position;

            SpriteRenderer ghostObjectRenderer = ghostObject.GetComponent<SpriteRenderer>();
            ghostObjectRenderer.sprite = _targetRenderer.sprite;
            ghostObjectRenderer.sortingLayerID = _targetRenderer.sortingLayerID;
            ghostObjectRenderer.sortingOrder = -1;
            ghostObjectRenderer.color = Color;
            if (Material)
                ghostObjectRenderer.material = Material;
        }

    }
}


