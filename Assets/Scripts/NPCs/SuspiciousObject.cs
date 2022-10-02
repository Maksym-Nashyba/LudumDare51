using UnityEngine;

namespace NPCs
{
    public class SuspiciousObject : MonoBehaviour
    {
        public Types Type => _type;
        [SerializeField] private Types _type;
        
        public enum Types
        {
            Parasite,
            Corpse,
            Blood,
            HiddenParasite
        }
    }
}