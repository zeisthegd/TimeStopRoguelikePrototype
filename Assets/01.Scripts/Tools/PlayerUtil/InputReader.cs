using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Events;

namespace Penwyn.Tools
{
    [CreateAssetMenu(menuName = "Util/Input Reader")]
    public class InputReader : SingletonScriptableObject<InputReader>, PlayerInput.IGameplayActions
    {
        #region Gameplay Input Events

        //Movement
        public event UnityAction<Vector2> Move;

        //Skills Using
        public event UnityAction NormalAttackPressed;
        public event UnityAction NormalAttackReleased;

        public event UnityAction SpecialAttackPressed;
        public event UnityAction SpecialAttackReleased;

        public event UnityAction DashPressed;
        public event UnityAction DashReleased;

        public event UnityAction GrabProjectilesPressed;
        public event UnityAction GrabProjectilesReleased;


        #endregion

        #region Logic Variables

        public bool IsHoldingNormalAttack { get; set; }
        public bool IsHoldingSpecialAttack { get; set; }

        #endregion

        private PlayerInput playerinput;

        void OnEnable()
        {
            if (playerinput == null)
            {
                playerinput = new PlayerInput();
                playerinput.Gameplay.SetCallbacks(this);
            }
            EnableGameplayInput();
        }

        public void OnMove(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MoveInput = context.ReadValue<Vector2>();
                Move?.Invoke(MoveInput);
            }
            else
                MoveInput = Vector2.zero;
        }

        public void OnNormalAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsHoldingNormalAttack = true;
                NormalAttackPressed?.Invoke();
            }
            else
            {
                IsHoldingNormalAttack = false;
                NormalAttackReleased?.Invoke();
            }

        }

        public void OnSpecialAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsHoldingSpecialAttack = true;
                SpecialAttackPressed?.Invoke();
            }
            else
            {
                IsHoldingSpecialAttack = false;
                SpecialAttackReleased?.Invoke();
            }
        }

        public void OnDash(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                DashPressed?.Invoke();
            }
            else
            {
                DashReleased?.Invoke();
            }
        }

        public void OnGrabProjectiles(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                GrabProjectilesPressed?.Invoke();
            }
            else
            {
                GrabProjectilesReleased?.Invoke();
            }
        }

        public void EnableGameplayInput()
        {
            playerinput.Gameplay.Enable();
        }

        public void DisableGameplayInput()
        {
            playerinput.Gameplay.Disable();
        }

        void OnDisable()
        {
            DisableGameplayInput();
        }

        public Vector2 MoveInput { get; set; }
    }
}
