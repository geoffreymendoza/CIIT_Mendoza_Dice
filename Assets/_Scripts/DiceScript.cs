using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceScript : MonoBehaviour {
    public static event Action<int> OnDiceRoll;
    
    [Header("Dice Side")]
    private DiceSide[] dsObj;
    private int diceValue;
    
    [Header("Roll Force")]
    [SerializeField] private Vector2 torqueValue;

    private Rigidbody rb;
    private Vector3 initialPos;
    private bool hasLanded;
    private bool hasThrown;
    private bool readyToRoll;

    private void Awake() {
        PlayerMovement.OnChangeView += OnChangeView;
    }
    
    private void OnDestroy() {
        PlayerMovement.OnChangeView -= OnChangeView;
    }
    
    private void OnChangeView(bool changeView) {
        if (changeView) return;
        ResetDice();
    }

    private void Start() {
        rb = this.GetComponent<Rigidbody>();
        initialPos = transform.position;
        dsObj = GetComponentsInChildren<DiceSide>();
        rb.useGravity = false;
        readyToRoll = true;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1))
            RollDice();

        if (rb.IsSleeping())
            CheckDiceState();
    }

    private void CheckDiceState() {
        if (!hasLanded && hasThrown) {
            hasLanded = true;
            rb.isKinematic = true;
            rb.useGravity = false;
            return;
        }

        if (hasLanded && diceValue == 0) {
            // Debug.Log("stopped");
            readyToRoll = false;
            CheckDiceOnGround();
            OnDiceRoll?.Invoke(diceValue);
            //RollAgain();
        }
    }

    void CheckDiceOnGround() {
        foreach (var ds in dsObj) {
            if (!ds.CheckGround())
                continue;
            diceValue = ds.GetSideValue();
            break;
        }
    }

    private void RollDice() {
        if (!readyToRoll) return;
        // if (hasThrown && hasLanded) {
        //     ResetDice();
        //     return;
        // }
        Roll();
    }

    private void RollAgain() {
        ResetDice();
        Roll();
    }

    private void Roll() {
        readyToRoll = false;
        hasThrown = true;
        rb.useGravity = true;
        rb.AddTorque(RandomTorqueAmount(torqueValue), RandomTorqueAmount(torqueValue), RandomTorqueAmount(torqueValue));
    }

    private void ResetDice() {
        transform.position = initialPos;
        hasThrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
        readyToRoll = true;
        diceValue = 0;
    }

    private float RandomTorqueAmount(Vector2 torque) {
        return Random.Range(torque.x, torque.y);
    }
}