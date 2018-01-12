using System;
using UnityEngine;

namespace Meta.Reconstruction
{
    /// <summary>
    /// Triggers the creation of a new slam map.
    /// </summary>
    public class EnvironmentNewSlamMapInitializerStep : EnvironmentInitializationStep
    {
        private readonly ISlamLocalizer _slamLocalizer;

        /// <summary>
        /// Creates an instance of <see cref="EnvironmentNewSlamMapInitializerStep"/> class.
        /// </summary>
        /// <param name="slamLocalizer">Slam type localizer.</param>
        public EnvironmentNewSlamMapInitializerStep(ISlamLocalizer slamLocalizer)
        {
            if (slamLocalizer == null)
            {
                throw new ArgumentNullException("slamLocalizer");
            }
            _slamLocalizer = slamLocalizer;
        }

        protected override void Initialize()
        {
            Debug.Assert(_slamLocalizer != null, "SlamLocalizer is null");

            if (_slamLocalizer.IsFinished)
            {
                Finish();
            }
            else
            {
                _slamLocalizer.SlamMappingCompleted.AddListener(SlamCompleted);
                _slamLocalizer.CreateSlamMap();
            }
        }

        private void SlamCompleted()
        {
            _slamLocalizer.SlamMappingCompleted.RemoveListener(SlamCompleted);
            Finish();
        }
    }
}