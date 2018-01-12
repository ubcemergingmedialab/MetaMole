using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Meta
{
    /// <summary>
    /// Used for Lightpainting. Allows a particle system to be stopped and started at any time without motion traces.
    /// </summary>
    public class ResetParticles : MonoBehaviour
    {

        public ParticleSystem[] systems;
        public UnityEngine.Events.UnityEvent OnReset;

        private Dictionary<ParticleSystem, float> _originalSettings;

        void Start()
        {
            _originalSettings = new Dictionary<ParticleSystem, float>();
            foreach (var s in systems) {
                var emissionSettings = s.emission;
                _originalSettings.Add(s, emissionSettings.rateOverTime.constantMax);
            }
        }

        public void Reset()
        {
            foreach (var system in systems)
            {
                system.Simulate(0, true, true);
                system.Play(true);
            }
            
            OnReset.Invoke();
        }


        public void Pause()
        {
            foreach (var system in systems)
            {
                var se = system.emission;
                se.enabled = false;
                se.rateOverTime = new ParticleSystem.MinMaxCurve(0);
            }
        }

        private IEnumerator _Play(ParticleSystem system)
        {
            yield return null;

            var se = system.emission;
            se.enabled = true;

            se.rateOverTime = new ParticleSystem.MinMaxCurve(_originalSettings[system]);
        }

        public void Play()
        {
            foreach (var system in systems)
            {
                StartCoroutine(_Play(system));
            }
        }


        public void StartNewLine()
        {
            StartCoroutine(_StartNewLine());
        }

        private IEnumerator _StartNewLine()
        {
            foreach (var system in systems)
            {
                var se = system.emission;
                se.enabled = false;
                se.rateOverTime = new ParticleSystem.MinMaxCurve(0);
            }

            // We have to wait a couple of frames to allow the ParticleSystem to settle.
            // Assumption of why this is necessary:
            // - first frame is to apply the values to the system, 
            // - second frame allows the ParticleSystem to initialize with the new settings, 
            // - third frame allows them to go through a regular Update frame with the correct settings.
            yield return null;
            yield return null;
            yield return null;

            foreach (var system in systems)
            {
                var se = system.emission;
                se.enabled = true;
                se.rateOverTime = new ParticleSystem.MinMaxCurve(_originalSettings[system] / transform.lossyScale.x);
            }
        }
    }


}