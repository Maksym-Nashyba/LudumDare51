using System.Collections;
using UnityEngine;

namespace Misc
{
    public class Siren : MonoBehaviour
    {
        private Light[] _alarmLights;

        private void Awake()
        {
            _alarmLights = GetComponentsInChildren<Light>();
        }

        public void Activate()
        {
            StartCoroutine(nameof(AlarmLightsBlinding));
        }

        private IEnumerator AlarmLightsBlinding()
        {
            float intensity = 0;
            while (true)
            {
                while (intensity < 6.5f)
                {
                    yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
                    foreach (Light light in _alarmLights)
                    {
                        intensity += Time.unscaledDeltaTime;
                        light.intensity = intensity;
                    }
                }
                
                while (intensity > 0.1f)
                {
                    yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
                    foreach (Light light in _alarmLights)
                    {
                        intensity -= Time.unscaledDeltaTime;
                        light.intensity = intensity;
                    }
                }
            }
        }
    }
}