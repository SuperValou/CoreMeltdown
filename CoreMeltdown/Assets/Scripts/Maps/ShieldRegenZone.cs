using System.Collections.Generic;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public class ShieldRegenZone : MonoBehaviour
    {
        public float regenPerSecond;

        private readonly ICollection<Player> _playersToShield = new HashSet<Player>();

        void OnTriggerEnter2D(Collider2D collider2D)
        {
            var playerController = collider2D.gameObject.GetComponent<TopDown2DPlayerController>();
            if (playerController == null)
            {
                return;
            }

            _playersToShield.Add(playerController.GetPlayer());
        }

        void OnTriggerExit2D(Collider2D collider2D)
        {
            var playerController = collider2D.gameObject.GetComponent<TopDown2DPlayerController>();
            if (playerController == null)
            {
                return;
            }

            _playersToShield.Remove(playerController.GetPlayer());
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
