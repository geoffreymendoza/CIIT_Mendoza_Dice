using System;
using System.Collections;
using UnityEngine;

// Movement of players using tiles
public class PlayerMovement : MonoBehaviour {
    public static event Action<bool> OnChangeView;
    
    [SerializeField] private float moveSmoothFactor = 4f;
    [SerializeField] private float speedFactor = 1f;
    [SerializeField] private float yOffset = 1.5f;

    private void Awake() {
        TilePlacement.OnStartTravel += OnStartTravel;
    }

    private void OnDestroy() {
        TilePlacement.OnStartTravel -= OnStartTravel;
    }

    private void OnStartTravel(Tile[ ] path) {
        StartCoroutine(MoveAlongPath(path));
    }

    IEnumerator MoveAlongPath(Tile[ ] path) {
        OnChangeView?.Invoke(true);
        yield return new WaitForSeconds(1.5f);
        Vector3 lastPosition = transform.position;
        int pathIdx = 0;
        while (pathIdx < path.Length) {
            Vector3 nextTile = path[pathIdx].transform.position;
            float lerpVal = 0;
            Vector3 nextTileOffset = nextTile + new Vector3(0, yOffset, 0);
            Vector3 vecToTarget = nextTileOffset - transform.position;
            Quaternion targetRot = Quaternion.LookRotation(vecToTarget, transform.up);
            while (lerpVal < 1) {
                float delta = Time.deltaTime;
                lerpVal += delta * speedFactor;
                transform.position = Vector3.Lerp(lastPosition, nextTileOffset, lerpVal);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, moveSmoothFactor * delta);
                yield return new WaitForEndOfFrame();
            }
            lastPosition = nextTileOffset;
            pathIdx++;
        }
        yield return new WaitForSeconds(1f);
        OnChangeView?.Invoke(false);
    }
}
