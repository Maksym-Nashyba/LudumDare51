using System;
using System.Collections;
using UnityEngine;

namespace Misc
{
    public class EntryCutscene: Cutscene
    {
        private bool _isButtonPressed;
        
        public override void Show(Action callback)
        {
            StartCoroutine(nameof(Play), callback);
        }

        public override void Abort()
        {
            StopCoroutine(nameof(Play));
        }

        private IEnumerator Play(Action callback)
        {
            ServiceLocator.UI.FadeFromBlack();
            yield return new WaitUntil(() => _isButtonPressed);
            callback?.Invoke();
        }
        
        public void OnPlayButtonPressed()
        {
            _isButtonPressed = true;
        }
    }
}