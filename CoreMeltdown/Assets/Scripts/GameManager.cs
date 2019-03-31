using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.HUDs;
using Assets.Scripts.Players;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public TopDown2DPlayerController[] playerControllers;
        public HeadUpDisplayManager HeadUpDisplayManager;

        private readonly string[] _playerNames = new[] {"Alice", "Bob"};

        private readonly Dictionary<Player, TopDown2DPlayerController> _players = new Dictionary<Player, TopDown2DPlayerController>();

        private readonly ICollection<Player> _alivePlayers = new HashSet<Player>();
        
        void Start()
        {
            if (playerControllers.Length > _playerNames.Length)
            {
                throw new ArgumentException($"Looks like we lack some player names here! '{nameof(playerControllers)}' has {playerControllers.Length} items but there are only {_playerNames.Length} player names available.");
            }

            for (int i = 0; i < playerControllers.Length; i++)
            {
                TopDown2DPlayerController controller = playerControllers[i];
                string playerName = _playerNames[i];

                var player = new Player(Player.MaxShield)
                {
                    PlayerName = playerName
                };
                controller.Initialize(player);

                _players.Add(player, controller);
                _alivePlayers.Add(player);

                HeadUpDisplayManager.AddPlayer(player);
            }
        }
        
        public void KillPlayer(Player player)
        {
            if (!_alivePlayers.Contains(player))
            {
                Debug.LogWarning(player.PlayerName + " was already dead.");
                return;
            }

            var controller = _players[player];
            controller.gameObject.SetActive(false);

            _alivePlayers.Remove(player);
            
            if (_alivePlayers.Count == 0)
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            Debug.LogWarning("THE END");
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                Debug.LogWarning("Inflicting damage");
                _alivePlayers.First().InflictRadiation(25 * Time.deltaTime);
            }

            foreach (var alivePlayer in _alivePlayers.ToList())
            {
                if (alivePlayer.IsRadiationPoisoned)
                {
                    KillPlayer(alivePlayer);
                }
            }
        }
    }
}