using Misc;
using UnityEngine;

namespace NPCs
{
    public class ScientistAnimations : NPCAnimations
    {
        [SerializeField] private GameObject _lightheadedPrefab;
        [SerializeField] private GameObject _glassesPrefab;
        private bool _playerDeathAnimation = false;
        
        public override void PlayStartInfection()
        {
            GFXAnimator.Play("ScientistStartInfection");
        }

        public override void PlayDeath(LivingNPC.DeathCauses cause)
        {
            if(_playerDeathAnimation)return;
            _playerDeathAnimation = true;
            
            if (cause == LivingNPC.DeathCauses.ParasiteLeaving)
            {
                GFXAnimator.gameObject.SetActive(false);
                Vector3 position = transform.position;
                ServiceLocator.Particles.Spawn(Particles.Type.Ketchup, position + Vector3.up);
                Instantiate(_lightheadedPrefab, position + Vector3.up * 0.05f, Quaternion.identity);
                Instantiate(_glassesPrefab, position + Vector3.one, Quaternion.identity)
                    .GetComponent<Rigidbody>()
                    .AddForce(Vector3.one * 0.2f + Vector3.right * Random.Range(0f, 0.1f), ForceMode.Impulse);
            }
        }

        public override void PlayInteractAnimation(Vector3 target)
        {
            Vector3 cameraPosition = ServiceLocator.Camera.transform.position;
            if ((target - cameraPosition).sqrMagnitude < (transform.position - cameraPosition).sqrMagnitude)
            {
                GFXAnimator.Play("ScientistInteractDown");
                return;
            }
            GFXAnimator.Play("ScientistInteractUp");
        }
    }
}