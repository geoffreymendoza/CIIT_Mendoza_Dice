using UnityEngine;

public class DiceSide : MonoBehaviour {
    private bool onGround;
    [SerializeField] private int sideValue;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Ground"))
            onGround = true;
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Ground"))
            onGround = false;
    }

    public bool CheckGround() {
        return onGround;
    }
}