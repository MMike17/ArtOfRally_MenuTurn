using UnityEngine;
using UnityModManagerNet;

using static UnityModManagerNet.UnityModManager;

namespace MenuTurn
{
    public class Settings : ModSettings, IDrawable
    {
        // [Draw(DrawType.)]

        [Draw(DrawType.Slider, Min = 0, Max = 2)]
        public float rotationSpeed = 0.5f;
        [Draw(DrawType.Auto)]
        public bool reverseRotation = false;
        [Draw(DrawType.Slider, Min = 0, Max = 5)]
        public float rotationDelay = 2;

        [Header("Debug")]
        [Draw(DrawType.Toggle)]
        public bool disableInfoLogs = true;

        public override void Save(ModEntry modEntry) => Save(this, modEntry);

        public void OnChange()
        {
            //
        }
    }
}
