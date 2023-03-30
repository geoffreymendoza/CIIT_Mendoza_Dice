using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager {
    public static event Action<SceneID> OnInitializeScene;
    public static void InvokeInitializeScene(SceneID sceneID) => OnInitializeScene?.Invoke(sceneID);
    
}