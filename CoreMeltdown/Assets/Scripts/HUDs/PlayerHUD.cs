using System;
using Assets.Scripts.Players;
using UnityEngine.UI;

namespace Assets.Scripts.HUDs
{
    public class PlayerHUD : BaseHUD
    {
        public Text healthLevel;
        public Text shieldLevel;
        
        private Player _player;

        public void SetPlayer(Player player)
        {
            if (_player != null)
            {
                throw new ArgumentException("Player already set.");
            }

            _player = player ?? throw new ArgumentNullException(nameof(player));
        }
        
        void Update()
        {
                float shieldRate = 100f * _player.Shield / Player.MaxShield;
                shieldLevel.text = shieldRate.ToString("##0") + "%";
                UpdateText(shieldLevel, shieldRate);

                float healthRate = 100f * _player.Health / Player.MaxHealth;
                healthLevel.text = healthRate.ToString("##0") + "%";
                UpdateText(healthLevel, healthRate);
        }

        private void UpdateText(Text text, float value)
        {
            if (value < dangerThreshold)
            {
                text.color = danger;
            }
            else if (value < warningThreshold)
            {
                text.color = warning;
            }
            else
            {
                text.color = safe;
            }
        }
    }
}