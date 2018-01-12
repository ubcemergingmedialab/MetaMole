

namespace Meta.Internal.Playback
{
    internal static class PCDExtensions {

        /// <summary>
        /// Returns a print friendly format syntax.
        /// </summary>
	    public static string FieldToOutputFormat(PointCloudDataType field)
        {
            switch (field) {
                case PointCloudDataType.XYZ:
                    return "x y z";
                case PointCloudDataType.XYZRGB:
                    return "x y z rgb";
                case PointCloudDataType.XYZRGBA:
                    return "x y z rgba";
                case PointCloudDataType.XYZCONFIDENCE:
                    return "x y z confidence";
                case PointCloudDataType.XYZNORMALS:
                    return "x y z normal_x normal_y normal_z";
            }
            return null;
        }
    }
}
