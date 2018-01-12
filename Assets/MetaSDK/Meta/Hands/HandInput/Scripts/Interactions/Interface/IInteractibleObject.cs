using Meta.HandInput;

namespace Meta
{
    public interface IInteractibleObject
    {
        void OnGrabEngaged(Hand hand);
        void OnGrabDisengaged(Hand hand);
    }

}