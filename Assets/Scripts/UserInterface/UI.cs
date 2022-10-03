using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _black;
        [SerializeField] private GameObject _alarmPanel;
        [SerializeField] private GameObject _victoryPanel;
        
        public void FadeToBlack()
        {
            StartCoroutine(ToBlack());
        }
        
        public void FadeFromBlack()
        {
            StartCoroutine(FromBlack());
        }

        public void OnPlayButton()
        {
            _button.interactable = false;
            StartCoroutine(RemovePlayButton());
        }
        
        public void UISetActive(bool state)
        {
            gameObject.SetActive(state);
        }

        private IEnumerator ToBlack()
        {
            _black.enabled = true;
            while (_black.color.a < 0.95f)
            {
                _black.color += new Color(0f, 0f, 0f, Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        
        private IEnumerator FromBlack()
        {
            _black.enabled = true;
            while (_black.color.a > 0.05f)
            {
                _black.color -= new Color(0f, 0f, 0f, Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            _black.enabled = false;
        }

        private IEnumerator RemovePlayButton()
        {
            while (_button.image.color.a > 0.05f)
            {
                _button.image.color -= new Color(0f, 0f, 0f, 3f * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            Destroy(_button.gameObject);
        }
        
        public void DisplayAlarmMessage()
        {
            _alarmPanel.SetActive(true);
        }

        public void DisplayVictoryMessage()
        {
            _victoryPanel.SetActive(true);
        }
    }
}