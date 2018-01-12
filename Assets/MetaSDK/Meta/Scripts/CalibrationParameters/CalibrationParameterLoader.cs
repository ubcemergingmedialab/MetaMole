using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Meta;
using SimpleJSON;


namespace Meta
{
    public class CalibrationParameterLoader : ICalibrationParameterLoader
    {

        private static string ParseDllInput()
        {
            string jsonString = null;
            CalibrationParameterLoaderInterop.GetJsonData(ref jsonString);
            if (jsonString != null)
            {
                if (jsonString.Length != 0)
                {
                    return jsonString;
                }
            }
            return null;
        }

        public virtual Dictionary<string, CalibrationProfile> Load()
        {
            string jsonString = ParseDllInput();

            if (jsonString == null)
            {
                return null;
            }

            var JsonRootNode = JSON.Parse(jsonString);

            if (JsonRootNode == null)
            {
                return null;
            }

            var nodes = JsonRootNode.AsArray;

            Dictionary<string, CalibrationProfile> profiles = new Dictionary<string, CalibrationProfile>();

            int nodeCounter = 0;
            foreach (JSONNode n in nodes)
            {
                string name = null;
                try
                {
                    name = n["name"];
                    Matrix4x4 poseMat = Matrix4x4.zero;
                    double[] r = n["relative_pose"].AsArray.Childs.Select(d => Double.Parse(d)).ToArray();
                    if (r.Length < 12)
                    {
                        Debug.LogError("CalibrationParameterLoader: array was too short.");
                    }
                    else
                    {
                        poseMat = CalibrationParameters.MatrixFromArray(r);

                    }

                    double[] cameraModel = n["camera_model"].AsArray.Childs.Select(d => Double.Parse(d)).ToArray();

                    profiles.Add(name, new CalibrationProfile { RelativePose = poseMat, CameraModel = cameraModel });

                    // Debug.Log(profiles[name].RelativePose + "|||" + 
                    //          string.Join(" ", (profiles[name].CameraModel.Select(x => x.ToString())).ToArray()));

                }
                catch
                {
                    if (name != null)
                    {
                        Debug.LogError(
                            string.Format(
                                "CalibrationParameter parsing error: node named '{0}' was not formatted correctly.", name));
                    }
                    else
                    {
                        Debug.LogError(
                            string.Format("CalibrationParameter parsing error: node {0} was not formatted correctly.",
                                nodeCounter));
                    }
                }

                nodeCounter++;
            }

            return profiles;
        }

    }


}