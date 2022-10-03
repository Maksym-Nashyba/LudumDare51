using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class HostLifeTimer : MonoBehaviour
    {
        public event Action Began;
        public event Action Ended;

        public event Action SecondPassed;
        
        public void BeginCountdown(Action callback)
        {
            StartCoroutine(LifeTime(callback));
        }
        
        private IEnumerator LifeTime(Action callback)
        {
            Began?.Invoke();
            for (int i = 0; i < 10; i++)
            {
                SecondPassed?.Invoke();
                yield return new WaitForSeconds(1f);
            }
            callback();
            Ended?.Invoke();
        }
    }
}