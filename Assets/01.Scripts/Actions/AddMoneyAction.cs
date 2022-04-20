using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Penwyn.Game;
namespace Penwyn.Tools
{
    public class AddMoneyAction : MonoBehaviour
    {
        public int Amount = 1;

        public virtual void AddMoneyTo()
        {
            Characters.Player.CharacterMoney.Add(Amount);
        }
    }
}

