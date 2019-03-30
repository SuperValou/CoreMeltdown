using System.Collections.Generic;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public class ShieldRegenZone : MonoBehaviour
    {
        public float regenPerSecond;

        private readonly ICollection<Player> _playersToShield = new HashSet<Player>();

        void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player == null)
            {
                return;
            }

            _playersToShield.Add(player);
        }

        void Update()
        {
            float regeneration = Time.deltaTime * regenPerSecond;
            foreach (var player in _playersToShield)
            {
                player.RegenerateShield(regeneration);
            }
        }
    }
}
