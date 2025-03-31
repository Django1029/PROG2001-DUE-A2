using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [Header("移动参数")]
    public float moveStep = 1.0f;          // 水平移动步长
    public float verticalStep = 1.0f;      // 垂直移动步长

    [Header("旋转参数")]
    public float rotationStep = 30.0f;     // 单次旋转角度
    public float maxRollAngle = 360f;      // 最大翻转角度

    [Header("缩放参数")]
    public float scaleFactor = 1.1f;       // 缩放系数
    public float minScale = 0.5f;          // 最小缩放
    public float maxScale = 3.0f;          // 最大缩放

    // 初始状态记录
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;

    void Start()
    {
        // 记录初始状态
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
    }

    //======== 移动控制 ========//
    public void MoveLeft() => transform.position -= transform.right * moveStep;
    public void MoveRight() => transform.position += transform.right * moveStep;
    public void MoveUp() => transform.position += transform.up * verticalStep;
    public void MoveDown() => transform.position -= transform.up * verticalStep;

    //======== 旋转控制 ========//
    public void RollLeft() => ApplyRotation(new Vector3(0, 0, rotationStep));
    public void RollRight() => ApplyRotation(new Vector3(0, 0, -rotationStep));
    public void PitchUp() => ApplyRotation(new Vector3(-rotationStep, 0, 0));
    public void PitchDown() => ApplyRotation(new Vector3(rotationStep, 0, 0));

    void ApplyRotation(Vector3 angles)
    {
        Vector3 newEuler = transform.rotation.eulerAngles + angles;

        // 限制翻转角度
        if (angles.z != 0)
            newEuler.z = Mathf.Clamp(newEuler.z, -maxRollAngle, maxRollAngle);

        transform.rotation = Quaternion.Euler(newEuler);
    }

    //======== 缩放控制 ========//
    public void ScaleUp()
    {
        Vector3 newScale = transform.localScale * scaleFactor;
        if (newScale.magnitude <= maxScale)
            transform.localScale = newScale;
    }

    public void ScaleDown()
    {
        Vector3 newScale = transform.localScale / scaleFactor;
        if (newScale.magnitude >= minScale)
            transform.localScale = newScale;
    }

    //======== 复原功能 ========//
    public void ResetAll()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;
    }
}