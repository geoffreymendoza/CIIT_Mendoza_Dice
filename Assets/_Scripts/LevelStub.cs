using System;
using UnityEngine;

public class LevelStub : MonoBehaviour {
    [SerializeField] private GameState sceneState;

    private void Start() {
        GameManager.ChangeState(sceneState);
    }

    [ContextMenu("TestChangeState")]
    public void TestChangeState() {
        GameManager.ChangeState(sceneState);
    }
}

