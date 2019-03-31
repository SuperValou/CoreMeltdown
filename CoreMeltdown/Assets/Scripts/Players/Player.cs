using System;
using UnityEngine;

namespace Assets.Scripts.Players
{
    public class Player
    {
        public const float MaxShield = 100;
        public const float MaxHealth = 100;
        
        public float Shield { get; private set; }
        public float Health { get; private set; }

        public string PlayerName { get; set; }

        public bool IsRadiationPoisoned { get; private set; }

        public Player(float initialShield)
        {
            if (initialShield < 0)
            {
                throw new ArgumentException($"{nameof(initialShield)} cannot be negative.");
            }

            Shield = Math.Min(initialShield, MaxShield);
            Health = MaxHealth;
            IsRadiationPoisoned = false;
        }

        public void RegenerateShield(float regeneration)
        {
            if (regeneration < 0)
            {
                throw new ArgumentException($"{nameof(regeneration)} cannot be negative.");
            }

            float shield = Math.Min(MaxShield, this.Shield + regeneration);
            this.Shield = shield;
        }

        public void InflictRadiation(float radiation)
        {
            if (IsRadiationPoisoned)
            {
                return;
            }

            if (radiation < 0)
            {
                throw new ArgumentException($"{nameof(radiation)} cannot be negative.");
            }
            
            float remainingShield = Shield - radiation;
            Shield = Math.Max(0, remainingShield);

            float damage = Math.Max(0, -1 * remainingShield);
            Health = Math.Max(0, Health - damage);

            if (Health <= 0)
            {
                IsRadiationPoisoned = true;
            }
        }
    }
}