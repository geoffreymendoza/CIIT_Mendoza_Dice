using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private CameraViewData[] camData;
    private Dictionary<CameraView, CameraViewData> camDict;
    private CinemachineVirtualCamera currentCam;
    
    private void Awake() {
        camDict = new Dictionary<CameraView, CameraViewData>();
        foreach (var c in camData) 
            camDict[c.ViewType] = c;
        // HACK
        currentCam = camDict[CameraView.TopDown].Camera;
        PlayerMovement.OnChangeView += OnChangeView;
    }

    private void OnDestroy() {
        PlayerMovement.OnChangeView -= OnChangeView;
    }

    private void OnChangeView(bool toChange) {
        if (toChange) {
            ChangeView(CameraView.FollowCharacter);
            return;
        }
        ChangeView(CameraView.TopDown);
    }

    private void ChangeView(CameraView viewType) {
        bool hasCam = camDict.TryGetValue(viewType, out var cameraData);
        if (!hasCam) {
            Debug.Assert(!hasCam, $"{viewType.ToString()} is not registered");
            return;
        }
        CinemachineVirtualCamera prevCam = currentCam;
        currentCam = cameraData.Camera;
        prevCam.Priority = 0;
        currentCam.Priority = 1;
    }
}

[System.Serializable]
public class CameraViewData {
    public CameraView ViewType;
    public CinemachineVirtualCamera Camera;
}

public enum CameraView {
    None,
    TopDown,
    FollowCharacter
}
