namespace EchoBound
{
    public interface IInteractable
    {
        string InteractionId { get; }
        void Interact();
    }
}
