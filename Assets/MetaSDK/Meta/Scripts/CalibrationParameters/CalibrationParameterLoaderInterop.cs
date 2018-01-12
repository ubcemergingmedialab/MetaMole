using System.Runtime.InteropServices;
using UnityEngine;

namespace Meta
{
    public class CalibrationParameterLoaderInterop
    {
        [DllImport(DllReferences.MetaVisionDLLName, EntryPoint = "getJsonData")]
        protected static extern int getJsonData([MarshalAs(UnmanagedType.BStr), In, Out] ref string json);

        public static void GetJsonData(ref string json)
        {
            getJsonData(ref json);
        }
    }
}