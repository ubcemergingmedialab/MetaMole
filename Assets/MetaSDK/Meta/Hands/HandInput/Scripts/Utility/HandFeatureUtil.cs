using System;

namespace Meta.HandInput
{
    public class HandFeatureUtil
    {
        public const int Any = -1;
        public const int CenterHandFeature = 0;
        public const int TopHandFeature = 1;

        public static Type ParseInt(int i)
        {
            switch (i)
            {
                case Any:
                    return null;
                case CenterHandFeature:
                    return typeof(CenterHandFeature);
                case TopHandFeature:
                    return typeof(TopHandFeature);
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Index: {0} is out of range.", i));
            }
        }

        public static HandFeatureType Parse(int i)
        {
            switch (i)
            {
                case Any:
                    return HandFeatureType.Any;
                case CenterHandFeature:
                    return HandFeatureType.PalmFeature;
                case TopHandFeature:
                    return HandFeatureType.TopFeature;
                default:
                    throw new ArgumentOutOfRangeException(string.Format("Index: {0} is out of range.", i));
            }
        }
    }
}