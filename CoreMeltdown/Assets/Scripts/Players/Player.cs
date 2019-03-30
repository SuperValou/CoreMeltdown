using System;

namespace Assets.Scripts.Players
{
    public class Player
    {
        public GameManager gameManager;
        public float maxShield = 100;
        public float maxHealth = 100;
        
        public float Shield { get; private set; }
        public float Health { get; private set; }

        public string DisplayName { get; set; }

        public void RegenerateShield(float regeneration)
        {
            if (regeneration < 0)
            {
                throw new ArgumentException($"{nameof(regeneration)} cannot be negative.");
            }

            float shield = Math.Min(maxShield, this.Shield + regeneration);
            this.Shield = shield;
        }

        public void InflictRadiation(float radiation)
        {
            if (radiation < 0)
            {
                throw new ArgumentException($"{nameof(radiation)} cannot be negative.");
            }

            float remainingShield = Shield - radiation;
            Shield = Math.Max(0, remainingShield);

            float damage = Math.Min(0, remainingShield);
            Health = Math.Max(0, Health - damage);
        }
    }
}