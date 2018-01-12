using System;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.SlamUI
{
    /// <summary>
    /// Animation states controller for the SLAM UI
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class SlamAnimation : BaseSlamAnimation
    {
        private Animator _animator;
        private Dictionary<CalibrationStage, string> _calibrationStageToAnimationName;

        private void Awake()
        {
            _animator = gameObject.GetComponent<Animator>();
        }

        /// <summary>
        /// Start the UI animation
        /// </summary>
        public override void StartAnimation()
        {
            _animator.Play("FadeIn");
        }

        /// <summary>
        /// Stop the UI animation
        /// </summary>
        public override void StopAnimation()
        {
            _animator.SetTrigger("FadeOut");
        }

        /// <summary>
        /// Play animation track related to the current calibration stage
        /// </summary>
        /// <param name="calibrationStage"></param>
        public override void PlayAnimation(CalibrationStage calibrationStage)
        {
            switch (calibrationStage)
            {
                case CalibrationStage.Mapping:
                    // removed cycle so animation is not going to wait for the user
                    //_animator.SetTrigger("Cycle");
                    break;
                case CalibrationStage.Remapping:
                    _animator.Play("ThreeDots");
                    break;
                case CalibrationStage.HoldStill:
                    _animator.SetTrigger("HoldStill");
                    break;
                case CalibrationStage.WaitingForSensors:
                    // TODO animation for waiting for sensors stage
                    break;
                case CalibrationStage.Completed:
                    _animator.SetTrigger("GreenCheck");
                    break;
                case CalibrationStage.Fail:
                    // TODO animation for fail stage
                    break;
                default:
                    throw new Exception("Calibration stage not implemented: " + calibrationStage);
            }
        }
    }
}