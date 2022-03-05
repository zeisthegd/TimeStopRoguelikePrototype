using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRun : CharacterAbility
{
    [SerializeField] private float runSpeed;
    [SerializeField] private bool usingRawMovement = false;

    public override void AwakeAbility(Character character)
    {
        base.AwakeAbility(character);
    }

    public override void UpdateAbility()
    {
        base.UpdateAbility();
        Vector3.ClampMagnitude(CharacterController.Velocity, runSpeed);
    }

    public override void FixedUpdateAbility()
    {
        base.FixedUpdateAbility();
        if (InputReader.Instance.MoveInput.magnitude >= 0.01F)
        {
            if (!usingRawMovement)
                Run(InputReader.Instance.MoveInput);
            else RunRaw(InputReader.Instance.MoveInput);
        }
    }

    /// <summary>
    /// Use add force and difference between desired and current velocity to make the player move.
    /// </summary>
    public void Run(Vector2 input)
    {
        float desiredSpeed = (input * runSpeed).magnitude; // Desired top speed.
        float accelRate = CharacterController.GetAccelerationRate(input);
        float velPower = CharacterController.Settings.runPower;

        float moveForce = accelRate * runSpeed;// Apply the acceleration to the speed difference.
        CharacterController.AddForce(input.normalized * moveForce);
    }

    public void RunRaw(Vector2 input)
    {
        CharacterController.SetVelocity(input.normalized * runSpeed);
    }

    public override void ConnectEvents()
    {
        base.ConnectEvents();
    }

    public override void DisconnectEvents()
    {
        base.DisconnectEvents();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }
    public float RunSpeed { get => runSpeed; set => runSpeed = value; }
}
