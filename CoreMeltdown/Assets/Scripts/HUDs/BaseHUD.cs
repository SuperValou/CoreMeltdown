using UnityEngine;

namespace Assets.Scripts.HUDs
{
    public abstract class BaseHUD : MonoBehaviour
    {
        public Color safe;
        public Color warning;
        public Color danger;

        public float warningThreshold = 50;
        public float dangerThreshold = 20;
    }
}