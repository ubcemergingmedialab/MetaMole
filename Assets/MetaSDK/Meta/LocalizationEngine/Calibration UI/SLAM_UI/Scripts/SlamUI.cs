using System;
using System.Collections;
using UnityEngine;

namespace Meta.SlamUI
{
    /// <summary>
    /// Slam UI controller
    /// </summary>
    public class SlamUI : BaseSlamUI
    {
        [SerializeField, Tooltip("Controller for messages content and animations")]
        private BaseSlamUIMessages _slamUIMessages;
        [SerializeField, Tooltip("Animation states controller for the SLAM UI")]
        private BaseSlamAnimation _slamAnimation;

        [SerializeField, Tooltip("Time in seconds between messages for readability")]
        private float _delayBetweenMessages = 3f;

        /// <summary>
        /// Change the current UI stage based on the calibration process
        /// </summary>
        /// <param name="calibrationStage"></param>
        /// <returns></returns>
        public override IEnumerator ChangeUIStage(CalibrationStage calibrationStage)
        {
            MetaCompositor compositor = null;
            switch (calibrationStage)
            {
                case CalibrationStage.WaitingForSensors:
                    compositor = FindObjectOfType<MetaCompositor>();
                    if (compositor && compositor.OcclusionEnabledAtStart)
                    {
                        compositor.EnableHandOcclusion = false;
                    }

                    _slamUIMessages.CurrentMessage = SLAMUIMessageType.WaitingForSensors;
                    _slamAnimation.PlayAnimation(calibrationStage);

                    break;

                case CalibrationStage.Mapping:
                    // if is already running the animation
                    if (_slamUIMessages.CurrentMessage == SLAMUIMessageType.TurnAround)
                    {
                        _slamAnimation.PlayAnimation(calibrationStage);
                    }
                    // if it is the first time
                    else
                    {
                        _slamUIMessages.CurrentMessage = SLAMUIMessageType.TurnAround;
                        _slamAnimation.StartAnimation();
                    }
                    break;

                case CalibrationStage.Completed:
                    _slamUIMessages.CurrentMessage = SLAMUIMessageType.MappingSuccess;
                    _slamAnimation.PlayAnimation(CalibrationStage.Completed);
                    yield return new WaitForSeconds(_delayBetweenMessages);

                    // fades out
                    _slamUIMessages.CurrentMessage = SLAMUIMessageType.None;
                    _slamAnimation.StopAnimation();

                    compositor = FindObjectOfType<MetaCompositor>();
                    if (compositor && compositor.OcclusionEnabledAtStart)
                    {
                        compositor.EnableHandOcclusion = true;
                    }

                    yield return new WaitForSeconds(_delayBetweenMessages);
                    Destroy(gameObject);
                    break;

                case CalibrationStage.HoldStill:
                    _slamUIMessages.CurrentMessage = SLAMUIMessageType.HoldStill;
                    _slamAnimation.PlayAnimation(calibrationStage);
                    break;

                case CalibrationStage.Fail:
                    _slamUIMessages.CurrentMessage = SLAMUIMessageType.MappingFail;
                    yield return new WaitForSeconds(_delayBetweenMessages);
                    break;

                case CalibrationStage.WaitingForTracking:
                    _slamUIMessages.CurrentMessage = SLAMUIMessageType.WaitingForTracking;
                    break;

                case CalibrationStage.Remapping:
                    _slamUIMessages.CurrentMessage = SLAMUIMessageType.Relocalization;
                    _slamAnimation.PlayAnimation(calibrationStage);
                    break;

                default:
                    throw new Exception("Calibration stage not implemented: " + calibrationStage);
            }
        }
    }
}