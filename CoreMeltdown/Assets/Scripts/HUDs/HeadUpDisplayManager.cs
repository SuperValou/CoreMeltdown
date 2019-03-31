using System.Collections.Generic;
using Assets.Scripts.Players;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class HeadUpDisplayManager : MonoBehaviour
    {
        public Text healthLevel;
        public Text shieldLevel;
        public Text radiationLevel;

        public Color safe;
        public Color warning;
        public Color danger;

        private readonly IList<Player> _players = new List<Player>();

        void Update()
        {
            foreach (var player in _players)
            {
                float shieldRate = 100f * player.Shield / Player.MaxShield;
                shieldLevel.text = shieldRate.ToString("##0") + "%";
                UpdateColor(shieldLevel, shieldRate);

                float healthRate = 100f * player.Health / Player.MaxHealth;
                healthLevel.text = healthRate.ToString("##0") + "%";
                UpdateColor(healthLevel, healthRate);
            }
        }

        private void UpdateColor(Text text, float value)
        {
            if (value < 20)
            {
                text.color = danger;
            }
            else if (value < 50)
            {
                text.color = warning;
            }
            else
            {
                text.color = safe;
            }
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public void SetRadiationLevel(float radiation)
        {
            radiationLevel.text = radiation.ToString("0.0");

            if (radiation > 10)
            {
                radiationLevel.color = danger;
            }
            else if (radiation > 5)
            {
                radiationLevel.color = warning;
            }
            else
            {
                radiationLevel.color = safe;
            }
        }
    }
}