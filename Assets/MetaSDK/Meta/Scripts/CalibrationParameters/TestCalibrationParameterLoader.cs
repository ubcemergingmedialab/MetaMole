using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Meta;
using SimpleJSON;

namespace Meta
{
    /// <summary>
    /// A calibration parameter loader used for testing purposes.
    /// A string has been embedded with JSON data. 
    /// </summary>
    public class TestCalibrationParameterLoader : ICalibrationParameterLoader
    {
        private int _delay = 100;

        public Dictionary<string, CalibrationProfile> Load()
        {
            _delay--;
            if (_delay > 0)
            {
                return null;
            }

            string jsonString = "[{\"relative_pose\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15], \"name\": \"rbg0\", \"camera_model\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]}, {\"relative_pose\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15], \"name\": \"rbg1\", \"camera_model\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]}, {\"relative_pose\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15], \"name\": \"rbg2\", \"camera_model\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]}, {\"relative_pose\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15], \"name\": \"rbg3\", \"camera_model\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]}, {\"relative_pose\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15], \"name\": \"rbg4\", \"camera_model\": [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15]}]";
            var JsonRootNode = JSON.Parse(jsonString);
            var nodes = JsonRootNode.AsArray;

            Dictionary<string, CalibrationProfile> profiles = new Dictionary<string, CalibrationProfile>();

            int nodeCounter = 0;
            foreach (JSONNode n in nodes)
            {
                string name = null;
                try
                {
                    name = n["name"];
                    double[] relativePose = n["relative_pose"].AsArray.Childs.Select(d => Double.Parse(d)).ToArray();
                    //double[] cameraModel = n["camera_model"].AsArray.Childs.Select(d => Double.Parse(d)).ToArray();
                    profiles.Add(name, new CalibrationProfile { /*CameraModel = cameraModel,*/ RelativePose = CalibrationParameters.MatrixFromArray(relativePose) });
                }
                catch
                {
                    if (name != null)
                    {
                        Debug.LogError(string.Format("CalibrationParameter parsing error: node named '{0}' was not formatted correctly.", name));
                    }
                    else
                    {
                        Debug.LogError(string.Format("CalibrationParameter parsing error: node {0} was not formatted correctly.", nodeCounter));
                    }
                }

                nodeCounter++;
            }

            return profiles;
        }
    }


}