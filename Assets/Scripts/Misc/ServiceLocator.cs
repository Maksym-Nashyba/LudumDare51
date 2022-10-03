using NPCs;
using NPCs.Navigation;
using UnityEngine;
using UserInterface;

namespace Misc
{
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _instance;

        [SerializeField] private Camera _camera;
        [SerializeField] private WaypointsContainer _waypointsContainer;
        [SerializeField] private GameObject _parasitePrefab;
        [SerializeField] private Particles _particles;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private UI _ui;
        private bool _isPlayerControlled;
        private Player.Player _playerInstance;

        public static Camera Camera => Instance._camera;
        
        public static WaypointsContainer WaypointsContainer => Instance._waypointsContainer;
        
        public static GameObject ParasitePrefab => Instance._parasitePrefab;
        

        public static Particles Particles => Instance._particles;
        
        public static UI UI => Instance._ui;
        
        public static bool IsPlayerControlled => Instance._isPlayerControlled;
        
        public static Player.Player PlayerInstance 
        {
            get => Instance._playerInstance;
            set => Instance._playerInstance = value;
        }

        public static ServiceLocator Instance
        {
            get
            {
                if (_instance is null)
                {
                    return FindObjectOfType<ServiceLocator>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _isPlayerControlled = _gameLoop.IsPlayerControlled;
        }
    }
}