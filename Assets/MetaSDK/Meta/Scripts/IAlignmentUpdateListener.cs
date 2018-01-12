using Meta;

/// <summary>
/// An Alignment Update listener. 
/// An instance of this type will perform an action when the alignment updates.
/// </summary>
public interface IAlignmentUpdateListener
{
    /// <summary>
    /// The actions to perform when the alignment updates.
    /// </summary>
    /// <param name="newProfile">The new profile, may be null if an invalid index was loaded.</param>
    void OnAlignmentUpdate(AlignmentProfile newProfile);
}
