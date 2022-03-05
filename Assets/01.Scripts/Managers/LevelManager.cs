using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    [SerializeField] GameObject playerToSpawn;
    [SerializeField] GameObject existedPlayer;

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (existedPlayer == null)
            Characters.Player = existedPlayer.GetComponent<Character>();
        else if (playerToSpawn != null)
        {
            Characters.Player = Instantiate(playerToSpawn).GetComponent<Character>();
        }
    }
}
