using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Penwyn.Tools;

namespace Penwyn.Game
{
    public class LevelManager : SingletonMonoBehaviour<LevelManager>
    {
        public GameObject PlayerToSpawn;
        public GameObject ExistedPlayer;

        public static event UnityAction PlayerSpawned;

        protected virtual void Start()
        {
            SpawnPlayer();
        }

        public virtual void SpawnPlayer()
        {
            if (ExistedPlayer != null)
                Characters.Player = ExistedPlayer.GetComponent<Character>();
            else if (PlayerToSpawn != null)
            {
                Characters.Player = Instantiate(PlayerToSpawn).GetComponent<Character>();
            }
            PlayerSpawned?.Invoke();
        }
    }
}
