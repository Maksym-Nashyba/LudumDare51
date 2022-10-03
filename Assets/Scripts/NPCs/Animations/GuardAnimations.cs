using Misc;
using UnityEngine;

namespace NPCs
{
    public class GuardAnimations : NPCAnimations
    {
        [SerializeField] private GameObject _lightheadedPrefab;
        [SerializeField] private GameObject _helmetPrefab;
        [SerializeField] private GameObject _riflePrefab;
        private bool _playerDeathAnimation = false;
        
        public override void PlayStartInfection()
        {
            GFXAnimator.Play("GuardStartInfection");
        }

        public override void PlayDeath(LivingNPC.DeathCauses cause)
        {
            if (_playerDeathAnimation) return;
            _playerDeathAnimation = true;

            if (cause == LivingNPC.DeathCauses.ParasiteLeaving)
            {
                GFXAnimator.gameObject.SetActive(false);
                Vector3 position = transform.position;
                ServiceLocator.Particles.Spawn(Particles.Type.Ketchup, position + Vector3.up);
                Instantiate(_lightheadedPrefab, position + Vector3.up * 0.075f, Quaternion.identity);
                Instantiate(_riflePrefab, position + new Vector3(-0.15f, 0.5f,-0.15f), Quaternion.identity);
                Instantiate(_helmetPrefab, position + Vector3.one, Quaternion.identity)
                    .GetComponent<Rigidbody>()
                    .AddForce(Vector3.one * 0.2f + Vector3.right * Random.Range(0f, 0.1f), ForceMode.Impulse);
            }else if (cause == LivingNPC.DeathCauses.Shot)
            {
                GFXAnimator.enabled = false;
                GFXObject.transform.SetParent(null);
                GFXObject.AddComponent<Rigidbody>().AddTorque(Random.Range(0f,10f), Random.Range(0f,10f), Random.Range(0f,10f), ForceMode.Impulse);
                GFXObject.GetComponentInChildren<BoxCollider>(true).enabled = true;
                GFXObject.AddComponent<Debris>();
            }
        }

        public override void PlayInteractAnimation(Vector3 target)
        {
            Vector3 cameraPosition = ServiceLocator.Camera.transform.position;
            if ((target - cameraPosition).sqrMagnitude < (transform.position - cameraPosition).sqrMagnitude)
            {
                GFXAnimator.Play("GuardInteractDown");
                return;
            }
            GFXAnimator.Play("GuardInteractUp");
        }
    }
}