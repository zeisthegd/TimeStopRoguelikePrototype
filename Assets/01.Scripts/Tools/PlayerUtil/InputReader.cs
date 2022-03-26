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
        public event UnityAction FirstSkillPressed;
        public event UnityAction FirstSkillReleased;

        public event UnityAction SecondSkillPressed;
        public event UnityAction SecondSkillReleased;

        public event UnityAction ThirdSkillPressed;
        public event UnityAction ThirdSkillReleased;


        #endregion

        #region Logic Variables

        public bool IsHoldingFirstSkill { get; set; }
        public bool IsHoldingSecondSkill { get; set; }
        public bool IsHoldingThirdSkill { get; set; }

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

        public void OnSkill_First(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsHoldingFirstSkill = true;
                FirstSkillPressed?.Invoke();
            }
            else
            {
                IsHoldingFirstSkill = false;
                FirstSkillReleased?.Invoke();
            }

        }

        public void OnSkill_Second(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsHoldingSecondSkill = true;
                SecondSkillPressed?.Invoke();
            }
            else
            {
                IsHoldingSecondSkill = false;
                SecondSkillReleased?.Invoke();
            }
        }

        public void OnSkill_Third(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.started)
            {
                IsHoldingThirdSkill = true;
                ThirdSkillPressed?.Invoke();
            }
            else
            {
                IsHoldingThirdSkill = false;
                ThirdSkillReleased?.Invoke();
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
