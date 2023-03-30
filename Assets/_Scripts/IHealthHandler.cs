public interface IHealthHandler {
    int CurrentHealth { get; }
    int MaxHealth { get; }
    bool IsAlive { get; }
    void Apply(ApplyType type);
}

public enum ApplyType {
    Damage,
    Regen
}