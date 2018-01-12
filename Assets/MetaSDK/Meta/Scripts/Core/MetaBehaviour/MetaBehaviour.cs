namespace Meta
{
    /// <summary>
    /// Base class for simple access to Meta context.
    /// For better testing, your class can extend BaseMetaBehaviour and use a custom IMetaContext and BaseMetaContextBridge.
    /// </summary>
    public class MetaBehaviour : BaseMetaBehaviour<IMetaContext>
    {
    }
}