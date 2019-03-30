using System.Collections.Generic;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public Player players;

        private ICollection<Player> _alivePlayers = new HashSet<Player>();

        public void KillPlayer(Player player)
        {
            if (!_alivePlayers.Contains(player))
            {
                Debug.LogWarning(player. + " was already dead.");
            }
        }

        public void EndGame()
        {

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.LogWarning("Inflicting damage");
                players[0].InflictRadiation(23);
            }
        }
    }
}