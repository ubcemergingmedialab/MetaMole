using UnityEngine;
using System.Collections;

namespace Meta
{
    using System.IO;

    public class PointCloudDataLogging
    {

        string outputFile;

        public PointCloudDataLogging(string recordDataFolder)
        {
            outputFile = recordDataFolder + "\\pointCloudOutputData.txt";
            if (System.IO.File.Exists(outputFile))
            {
                outputFile = recordDataFolder + "\\pointCloudOutputData_" + MetaUtils.GetCurrentSystemTime("{0:HHmmssffff}") + ".txt";
            }

        }

        // Update is called once per frame
        internal void Update <TPoint>(PointCloudData<TPoint> pointCloudData) where TPoint : PointXYZ, new()
        {
            string pointcloudDataString = pointCloudData.frameId.ToString() + ", ";

            pointcloudDataString += pointCloudData.arrivalOfCleanSensorDataTimeStamp.ToString() + ", "; 
            pointcloudDataString += pointCloudData.completionOfPointCloudGenerationTimeStamp.ToString() + ", "; 

            string systemTime = Meta.MetaUtils.GetCurrentSystemTime();
            string handDataString = systemTime + ", " + pointcloudDataString + "\n";         
            File.AppendAllText(outputFile, handDataString);
        }
    }
}