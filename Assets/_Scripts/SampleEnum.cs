using UnityEngine;

public class SampleEnum : MonoBehaviour {
    public DiceState State;

    private void Update() {
        switch (State) {
            case DiceState.IsThrown:
                break;
            case DiceState.IsSleeping:
                break;
        }
    }
}

public enum DiceState {
    IsThrown,
    IsSleeping,
}