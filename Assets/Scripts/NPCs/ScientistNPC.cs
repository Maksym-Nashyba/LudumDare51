using UnityEngine;

namespace NPCs
{
    public class ScientistNPC : LivingNPC
    {
        protected override void Awake()
        {
            base.Awake();
            Detector.Detected += types =>
            {
                Debug.Log(types);
            };
        }
    }
}