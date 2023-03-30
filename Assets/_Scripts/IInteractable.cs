using UnityEngine;

public interface IInteractable {
    InteractableType Type { get; }
    void Interact(IHealthHandler handler);
}

public enum InteractableType {
    Unassigned,
}
