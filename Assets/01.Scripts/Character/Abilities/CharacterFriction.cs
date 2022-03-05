using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFriction : CharacterAbility
{
    public override void AwakeAbility(Character character)
    {
        base.AwakeAbility(character);
    }

    public override void UpdateAbility()
    {
        base.UpdateAbility();
    }

    public override void FixedUpdateAbility()
    {
        ApplyFriction();
    }

    void ApplyFriction()
    {
        if (InputReader.Instance.MoveInput.magnitude < 0.01F)
        {
            Vector2 amount = CharacterController.Body2D.velocity.normalized * -1F;
            amount *= CharacterController.Settings.friction;
            CharacterController.AddForce(amount, ForceMode2D.Force);
            Debug.DrawRay(Character.Position, amount, Color.yellow);
        }
    }

}
