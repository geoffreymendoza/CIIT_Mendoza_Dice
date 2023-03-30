using Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")] // Hide in menu
public class LockCameraAxis : CinemachineExtension {
    [Tooltip("Lock the camera's Z position to this value")]
    public float xPosition = 0f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime) {
        if (stage == CinemachineCore.Stage.Body) {
            var pos = state.RawPosition;
            pos.x = xPosition;
            state.RawPosition = pos;
        }
    }
}