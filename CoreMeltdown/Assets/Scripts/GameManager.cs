using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.ControlRooms;
using Assets.Scripts.HUDs;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public TopDown2DPlayerController[] playerControllers;

        public PlayerHUD[] playerHUDs;
        public RadiationsHUD radiationsHUD;

        public NuclearCore nuclearCore;

        public GlobalTimer globalTimer;

        private readonly string[] _playerNames = new[] {"Alice", "Bob"};

        private readonly Dictionary<Player, TopDown2DPlayerController> _players = new Dictionary<Player, TopDown2DPlayerController>();

        private readonly ICollection<Player> _alivePlayers = new HashSet<Player>();
        
        void Start()
        {
            if (playerControllers.Length > _playerNames.Length)
            {
                throw new ArgumentException($"Looks like we lack some player names here! '{nameof(playerControllers)}' has {playerControllers.Length} items but there are only {_playerNames.Length} player names available.");
            }

            if (playerControllers.Length > playerHUDs.Length)
            {
                throw new ArgumentException($"Looks like we lack some player HUD here! '{nameof(playerControllers)}' has {playerControllers.Length} items but there are only {playerHUDs.Length} HUD available.");
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

                playerHUDs[i].SetPlayer(player);
            }
        }
        
        void Update()
        {
            radiationsHUD.SetRadiationLevel(nuclearCore.RadiationsPerSecond);

            foreach (var alivePlayer in _alivePlayers.ToList())
            {
                alivePlayer.InflictRadiation(nuclearCore.RadiationsPerSecond * Time.deltaTime);

                if (alivePlayer.IsRadiationPoisoned)
                {
                    KillPlayer(alivePlayer);
                }
            }

            if (_alivePlayers.Count == 0 || globalTimer.TimeIsUp)
            {
                EndGame();
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
        }

        private void EndGame()
        {
            nuclearCore.Stop();

            foreach (TopDown2DPlayerController playerController in _players.Values)
            {
                playerController.enabled = false;
            }

            Debug.LogWarning("THE END");
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}