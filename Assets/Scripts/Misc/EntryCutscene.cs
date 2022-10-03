using System;
using System.Collections;
using UnityEngine;

namespace Misc
{
    public class EntryCutscene: Cutscene
    {
        [SerializeField] private Transform _startCameraPostition;
        [SerializeField] private Transform _defaultCameraPostition;
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
            ServiceLocator.Camera.transform.position = _startCameraPostition.position;
            ServiceLocator.UI.FadeFromBlack();
            yield return new WaitUntil(() => _isButtonPressed);
            _mover.LerpCameraTo(_defaultCameraPostition);
            yield return new WaitUntil(() => _mover.DistanceToCurrentTarget() < 0.5f);
            callback?.Invoke();
        }
        
        public void OnPlayButtonPressed()
        {
            _isButtonPressed = true;
        }
    }
}