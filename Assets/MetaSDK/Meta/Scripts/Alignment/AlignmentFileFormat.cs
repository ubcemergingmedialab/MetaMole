using System.Collections.Generic;

namespace Meta
{
    public static class AlignmentFileFormat
    {
        public enum FieldType
        {
            SerialNumber = 0,
            Name = 1,
            Index = 2
        }

        public const string CurrentVersion = "v1.1";

        public const int HeaderLines = 1;

        public static int GetSingleProfileLines()
        {
            return System.Enum.GetNames(typeof(FieldType)).Length;
        }

        public static int GetIndexInProfile(FieldType fieldType)
        {
            return (int)fieldType;
        }
    }
}
