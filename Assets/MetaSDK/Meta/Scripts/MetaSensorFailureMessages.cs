using System.Collections;
using UnityEngine;

namespace Meta
{
    /// <summary>
    /// Creates Sensor Failure messages at predefined intervals.
    /// </summary>
    internal class MetaSensorFailureMessages : IEventReceiver
    {
        private const float IntervalToCheckSensorsSeconds = 1f;
        private const string SensorMessage01 = "Sensors not yet started.\nPlease wait ...";
        private const string SensorMessage02 = "Sensors taking unusually long to start.\nIf this is your first use, this might be normal.\nPlease wait ...";
        private const string SensorMessage03 = "Please exit, restart your device and\nlaunch the application again.";

        private MetaSensorUiController _controller;

        public void Init(IEventHandlers eventHandlers)
        {
            eventHandlers.SubscribeOnStart(CheckSensors);
            eventHandlers.SubscribeOnApplicationQuit(()=> { _controller.Destroy(); });
        }

        private void CheckSensors()
        {
            var manager = GameObject.FindObjectOfType<MetaManager>();
            _controller = new MetaSensorUiController();
            _controller.SetTitleVisibility(false);
           
            if (!manager)
            {
                Debug.LogError("Could not get MetaManager");
                return;
            }

            manager.StartCoroutine(CheckSensorsAtIntervals());
            manager.StartCoroutine(CheckSensorsRepeatedly());
        }

        private IEnumerator CheckSensorsRepeatedly() {

            const string SensorFailureMessage = "Please restart the application";
            SlamLocalizer slamLocalizer = GameObject.FindObjectOfType<SlamLocalizer>();

            //If the SLAM localizer is not being used then the sensor status cannot be read.
            if (!slamLocalizer)
            {
                yield break;
            }

            for(; ; )
            {
                //Wait until the sensors should be ready.
                if (!slamLocalizer.SlamFeedback.CameraReady)
                {
                    yield return null;
                    continue;
                }

                //The IMU should be ready before the Camera is ready. If not then this is reported as an error.
                if (!slamLocalizer.SlamFeedback.HasFirstImu)
                {
                    _controller.ChangeMinorMessage(SensorFailureMessage);
                    _controller.SetTitleVisibility(true);
                }

                yield break;
            }
        }

        /// <summary>
        /// Check the sensors at the intervals defined.
        /// </summary>
        /// <returns></returns>
        private IEnumerator CheckSensorsAtIntervals()
        {
            if (SensorsInitialized())
            {
                yield break;
            }

            yield return CheckSensorFrequently(10, IntervalToCheckSensorsSeconds);

            if (CheckSensorRecovery(_controller))
            {
                yield break;
            }
            
            _controller.ChangeMessage(SensorMessage01);

            yield return CheckSensorFrequently(20, IntervalToCheckSensorsSeconds);

            if (CheckSensorRecovery(_controller))
            {
                yield break;
            }

            _controller.ChangeMessage(SensorMessage02);

            yield return CheckSensorFrequently(60, IntervalToCheckSensorsSeconds);

            if (CheckSensorRecovery(_controller))
            {
                yield break;
            }

            _controller.ChangeMessage(SensorMessage03);
            _controller.SetTitleVisibility(true);
        }

        /// <summary>
        /// Checks for sensor recovery.
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        private bool CheckSensorRecovery(MetaSensorUiController controller)
        {
            if (SensorsInitialized())
            {
                if (!DepthSensorWorking())
                {
                    //Not all the sensors have recovered. Show a message indefinitely.
                    controller.ChangeMessage("We've encountered issues starting sensors. Hands might not track.\nExit the application and run Headset Diagnostics.");
                    return true;
                }
                controller.ChangeMessage(string.Empty);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Check the sensors for a number of times at a given interval. 
        /// </summary>
        /// <param name="numberOfChecks">number of times to check</param>
        /// <param name="checkIntervalSeconds">interval to wait in seconds.</param>
        /// <returns></returns>
        private IEnumerator CheckSensorFrequently(int numberOfChecks, float checkIntervalSeconds)
        {
            for (int i = 0; i < numberOfChecks; ++i)
            {
                if (SensorsInitialized())
                {  
                    yield break;
                }
                yield return new WaitForSeconds(checkIntervalSeconds);
            }
        }

        /// <summary>
        /// Check if the important sensors have been initialized
        /// </summary>
        /// <returns></returns>
        private bool SensorsInitialized()
        {
            bool rightImuInitialized = false;
            bool leftMonoInitialized = false;
            bool rightMonoInitialized = false;
            bool unused = false;

            MetaSensors.GetSensorConnectionInfo(SensorType.IMU, 0, out unused, out rightImuInitialized);
            MetaSensors.GetSensorConnectionInfo(SensorType.Monochrome, 0, out unused, out rightMonoInitialized);
            MetaSensors.GetSensorConnectionInfo(SensorType.Monochrome, 1, out unused, out leftMonoInitialized);

            return (rightImuInitialized && leftMonoInitialized && rightMonoInitialized);
        }

        /// <summary>
        /// Checks if the depth sensor is working.
        /// </summary>
        /// <returns></returns>
        private bool DepthSensorWorking()
        {
            bool depthSensorConnected;
            bool depthSensorInitialized;
            MetaSensors.GetSensorConnectionInfo(SensorType.IMU, 0, out depthSensorConnected, out depthSensorInitialized);
            return (depthSensorConnected && depthSensorInitialized);
        }
    }

}