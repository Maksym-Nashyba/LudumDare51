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
            _leverCutscene.Show(()=>_isPlayerControlled=true);
        }

        public void ActivateAlarm()
        {
            _isPlayerControlled = false;
            StartCoroutine(nameof(RestartGame));

        }
        
        public void PlayerDied()
        {
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}