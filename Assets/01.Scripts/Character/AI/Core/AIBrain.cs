using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using NaughtyAttributes;
namespace Penwyn.Game
{
    public class AIBrain : MonoBehaviour
    {
        public List<AIState> States;
        public bool Enabled;
        [ReadOnly] public AIState CurrentState;

        protected Character _character;

        public virtual void Awake()
        {
            _character = GetComponent<Character>();
            AwakeStates();
            if (States.Count > 0)
                CurrentState = States[0];
        }

        public virtual void Update()
        {
            if (Enabled)
            {
                CurrentState.Update();
            }
        }

        public void ChangeState(string stateName)
        {
            if (stateName != "")
            {
                AIState newState = States.Find(x => x.Name == stateName);
                if (newState != null)
                    ChangeState(newState);
                else
                    Debug.LogWarning($"State {stateName} was not found.");
            }
            else
            {
                Debug.Log("State not change since stateName is null.");
            }
        }

        public void ChangeState(AIState state)
        {
            CurrentState.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public virtual void AwakeStates()
        {
            for (int i = 0; i < States.Count; i++)
            {
                States[i].Awake(this);
            }
        }

        public Character Character => _character;
    }
}

