using UnityEngine;

public static class Data {
    public static readonly int FLOATING_ANIM = Animator.StringToHash("Floating");
    public static readonly int CRAWLING_ANIM = Animator.StringToHash("Crawling");
    public static readonly int RUNNING_ANIM = Animator.StringToHash("Running");
    
    public static readonly int IDLE_ANIM = Animator.StringToHash("Idle");
    public static readonly int TAKEOFF_ANIM = Animator.StringToHash("Take Off");
    public static readonly int FLYFLOAT_ANIM = Animator.StringToHash("Fly Float");
    public static readonly int LAND_ANIM = Animator.StringToHash("Land");
    
    private static readonly int InteractableLayer = LayerMask.NameToLayer("Interactable");
    public static readonly int InteractableLayerMask = 1 << InteractableLayer;
}



[System.Serializable]
public class AnimationData {
    public string AnimNameString;
    public float Duration;
    public int AnimName;

    public AnimationData(string name, float duration) {
        AnimNameString = name;
        Duration = duration;
        AnimName = Animator.StringToHash(AnimNameString);
    }
}

public enum SceneID {
    Unassigned,
    MainMenu,
    InGame,
    Result,
}