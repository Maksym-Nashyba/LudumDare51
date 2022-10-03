using System.Collections;
using Interactables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UserInterface;

namespace Misc
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private GateOpeningLever _lever;
        [SerializeField] private EntryCutscene _entryCutscene;
        [SerializeField] private Cutscene _leverCutscene;
        [SerializeField] private AlarmBox _alarmBox;
        private bool _isPlayerControlled;
        private bool _startedReboot;

        public bool IsPlayerControlled => _isPlayerControlled;

        private void Awake()
        {
            _alarmBox.AlarmActivated.AddListener(ActivateAlarm);
            _lever.Pulled.AddListener(ShowLeverCutscene);
        }

        private void Start()
        {
            ShowEntryCutscene();
            
        }

        private void ShowEntryCutscene()
        {
            _isPlayerControlled = false;
            _entryCutscene.Show(()=>_isPlayerControlled=true);
        }
        
        private void ShowLeverCutscene()
        {
            _isPlayerControlled = false;
            _leverCutscene.Show(()=>_isPlayerControlled=true);
        }

        public void ActivateAlarm()
        {
            if(_startedReboot)return;
            _startedReboot = true;
            _isPlayerControlled = false;
            
            ServiceLocator.UI.DisplayAlarmMessage(); 
            ServiceLocator.Siren.Activate();
            StartCoroutine(nameof(RestartGame));
        }
        
        public void PlayerDied()
        {
            if(_startedReboot)return;
            _startedReboot = true;
            _isPlayerControlled = false;
            
            StartCoroutine(nameof(RestartGame));
        }

        private IEnumerator RestartGame()
        {
            while (Time.timeScale > 0.2f)
            {
                Time.timeScale -= Time.unscaledDeltaTime;
                yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
            }
            ServiceLocator.UI.FadeToBlack();
            yield return new WaitForSecondsRealtime(1.5f);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}