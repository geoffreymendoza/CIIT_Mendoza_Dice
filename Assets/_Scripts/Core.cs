using System;
using UnityEngine;

public static partial class Core {
    public static void DebugKeyPress(KeyCode key, Action function) {
        if (!Input.GetKeyDown(key)) return;
        function?.Invoke();
    }
}