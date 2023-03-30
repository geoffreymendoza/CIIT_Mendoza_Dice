using UnityEngine;

public class TestGetComponent : MonoBehaviour {
    private const int numOfTest = 5000;
    [SerializeField] private Transform testTransform;

    private const string transformString = "Transform";

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Test_01();
            Test_02();
            Test_03();
        }
    }

    void Test_01() {
        for (int i = 0; i < numOfTest; ++i) {
            testTransform = GetComponent<Transform>();
        }
    }

    void Test_02() {
        for (int i = 0; i < numOfTest; ++i) {
            //testTransform = FindObjectOfType<Transform>();
            //testTransform = GetComponent("Transform") as Transform;
            testTransform = GetComponent(nameof(Transform)) as Transform;
        }
    }

    void Test_03() {
        for (int i = 0; i < numOfTest; ++i) {
            //testTransform = GameObject.Find("Test_01").transform;
            testTransform = (Transform)GetComponent(typeof(Transform));
        }
    }
}
