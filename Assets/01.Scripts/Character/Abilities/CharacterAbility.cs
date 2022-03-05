using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    [SerializeField] private bool abilityPermitted;
    public new string name;

    public List<CharacterAbilityStates> forbidStates;



    private Character character;
    private CharacterController characterController;


    public virtual void AwakeAbility(Character character)
    {
        this.character = character;
        this.characterController = character.Controller;
        if (abilityPermitted)
        {
            ConnectEvents();
        }
    }

    public void ChangePermission(bool permission)
    {
        if (permission)
        {
            ConnectEvents();
        }
        else
        {
            DisconnectEvents();
        }
    }

    public virtual void UpdateAbility() { }
    public virtual void FixedUpdateAbility() { }
    public virtual void ConnectEvents() { }
    public virtual void DisconnectEvents() { }
    public virtual void OnDisable() { }

    public Character Character { get => character; set => character = value; }
    public CharacterController CharacterController { get => characterController; }
    public bool AbilityPermitted { get => abilityPermitted; }
}
