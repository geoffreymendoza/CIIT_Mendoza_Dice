using UnityEngine;

public static partial class Core {
    public static void ChangeAnimation(this Animator anim, int state) {
        anim.CrossFade(state, 0, 0);
    }
}
