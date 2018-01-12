namespace Meta
{
    /// <summary>
    /// Base class for simple access to Meta context.
    ///  For better testing, your class can extend BaseMetaBehaviour and use a custom IMetaContextInternal and BaseMetaContextBridge.
    /// </summary>
    internal class MetaBehaviourInternal : BaseMetaBehaviour<IMetaContextInternal>
    {
    }
}