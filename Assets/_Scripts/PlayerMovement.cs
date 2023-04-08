using System;
using System.Collections;
using UnityEngine;

// Movement of players using tiles
public class PlayerMovement : MonoBehaviour {
    public static event Action<bool> OnChangeView;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSmoothFactor = 4f;
    [SerializeField] private float speedFactor = 1f;
    [SerializeField] private float yOffset = 1.5f;

    private void Awake() {
        TilePlacement.OnStartTravel += OnStartTravel;
    }

    private void OnDestroy() {
        TilePlacement.OnStartTravel -= OnStartTravel;
    }

    private void Start() {
        // TODO save in dictionary then call it on animation
        var clips = anim.runtimeAnimatorController.animationClips;
        foreach (var s in clips) {
            Debug.Log($"Anim Name: {s.name}, Duration: {s.length}");
        }
    }

    private void OnStartTravel(Tile[ ] path) {
        StartCoroutine(MoveAlongPath(path));
    }

    IEnumerator MoveAlongPath(Tile[ ] path) {
        float t = 0.75f;
        OnChangeView?.Invoke(true);
        yield return new WaitForSeconds(t);
        // TODO get clip based on hash name
        anim.CrossFade(Data.TAKEOFF_ANIM, 0);
        yield return new WaitForSeconds(2.75f);
        anim.CrossFade(Data.FLYFLOAT_ANIM, 0);
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
            path[pathIdx].ChangeToGreen();
            pathIdx++;
        }
        anim.CrossFade(Data.LAND_ANIM, 0);
        yield return new WaitForSeconds(2.5f);
        anim.CrossFade(Data.IDLE_ANIM, 0);
        foreach (var tile in path) 
            tile.ChangeToRed();
        OnChangeView?.Invoke(false);
    }
    
    bool AnimatorIsPlaying(){
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }
}
