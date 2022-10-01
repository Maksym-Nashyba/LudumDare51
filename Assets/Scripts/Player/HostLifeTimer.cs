using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class HostLifeTimer : MonoBehaviour
    {
        public void BeginCountdown(Action callback)
        {
            StartCoroutine(LifeTime(callback));
        }
        
        private IEnumerator LifeTime(Action callback)
        {
            for (int i = 10; i >= 1; i--)
            {
                yield return new WaitForSeconds(1);
                transform.localScale = new Vector3(0.05f, i * 0.01f, 0.05f);
            }
            callback();
        }
    }
}