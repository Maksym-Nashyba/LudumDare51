using System.Collections;
using Interactables;
using UnityEngine;

namespace Misc
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private GateOpeningLever _lever;
        [SerializeField] private EntryCutscene _entryCutscene;
        [SerializeField] private LeverCutscene _leverCutscene;
        private bool _isPlayerControlled;

        public bool IsPlayerControlled => _isPlayerControlled;

        private void Awake()
        {
            _lever.Pulled.AddListener(ShowLeverCutscene);
        }

        private void Start()
        {
            ShowEntryCutscene();
        }

        private void ShowEntryCutscene()
        {
            _isPlayerControlled = false;
            //_entryCutscene.Show();
        }
        
        private void ShowLeverCutscene()
        {
            _isPlayerControlled = false;
            //_leverCutscene.Show();
        }

        public void ActivateAlarm()
        {
            _isPlayerControlled = false;
            StartCoroutine(nameof(RestartGame));

        }
        
        public void PlayerDied()
        {
            
        }

        private IEnumerator RestartGame()
        {
            yield break;
        }
    }
}