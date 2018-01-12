using System;
using System.Runtime.InteropServices;

namespace Meta.Plugin
{
    internal static class Webcam
    {
        [DllImport("Meta2WebcamComposite", EntryPoint = "webcam_initialize")]
        internal static extern void Initalize(IntPtr texture, float targetFps);

        [DllImport("Meta2WebcamComposite", EntryPoint = "webcam_run")]
        internal static extern bool Run();

        [DllImport("Meta2WebcamComposite", EntryPoint = "webcam_stop")]
        internal static extern bool Stop();

        [DllImport("Meta2WebcamComposite", EntryPoint = "webcam_onRender")]
        internal static extern void OnRender();

        [DllImport("Meta2WebcamComposite", EntryPoint = "webcam_getRenderCallback")]
        internal static extern IntPtr GetRenderCallback();

        [DllImport("Meta2WebcamComposite", EntryPoint = "IsWebcamOn")]
        internal static extern bool IsWebcamOn();
    }
}
