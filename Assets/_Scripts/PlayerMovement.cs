using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Movement of players using tiles
public class PlayerMovement : MonoBehaviour {
    public static event Action<bool> OnChangeView;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSmoothFactor = 4f;
    [SerializeField] private float speedFactor = 1f;
    [SerializeField] private float yOffset = 1.5f;
    private Dictionary<int, AnimationData> _animDict = new Dictionary<int, AnimationData>();

    private void Awake() {
        TilePlacement.OnStartTravel += OnStartTravel;
    }

    private void OnDestroy() {
        TilePlacement.OnStartTravel -= OnStartTravel;
    }

    private void Start() {
        List<AnimationData> animDatas = new List<AnimationData>();
        var clips = anim.runtimeAnimatorController.animationClips;
        foreach (var c in clips) {
            AnimationData data = new( c.name, c.length );
            animDatas.Add(data);
        }
        foreach (var ad in animDatas.ToArray()) {
            _animDict[ad.AnimName] = ad;
        }
    }

    private void OnStartTravel(Tile[ ] path) {
        StartCoroutine(MoveAlongPath(path));
    }

    IEnumerator MoveAlongPath(Tile[ ] path) {
        float t = 0.75f;
        OnChangeView?.Invoke(true);
        yield return new WaitForSeconds(t);
        anim.ChangeAnimation(Data.TAKEOFF_ANIM);
        t = GetAnimationDuration(Data.TAKEOFF_ANIM);
        yield return new WaitForSeconds(t);
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
        anim.ChangeAnimation(Data.LAND_ANIM);
        t = GetAnimationDuration(Data.LAND_ANIM, 0.65f);
        yield return new WaitForSeconds(t);
        anim.CrossFade(Data.IDLE_ANIM, 0);
        foreach (var tile in path)
            tile.ChangeToRed();
        OnChangeView?.Invoke(false);
    }

    private float GetAnimationDuration(int state, float speed = 1) {
        float result = 0;
        var data = _animDict[state];
        result = data.Duration;
        result *= speed;
        return result;
    }

    bool AnimatorIsPlaying() {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }
}