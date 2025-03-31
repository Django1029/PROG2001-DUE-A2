using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleController : MonoBehaviour
{
    [Header("移动参数")]
    public float baseMoveSpeed = 5f;      // 基础移动速度（已加倍）
    public float rotationSpeed = 90f;     // 旋转速度(度/秒)
    public float zoomSpeed = 0.5f;        // 缩放速度
    public float maxZoom = 2f;            // 最大放大倍数
    public float minZoom = 0.5f;          // 最小缩小倍数

    private Vector3 originalPosition;     // 初始位置
    private Quaternion originalRotation;  // 初始旋转
    private Vector3 originalScale;        // 初始缩放

    void Start()
    {
        // 记录初始状态
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
    }

    // ========== 统一移动控制 ==========
    public void Move(Vector3 direction)
    {
        // 移动幅度加倍（可根据需求调整倍数）
        float boostedSpeed = baseMoveSpeed * 2f;
        transform.Translate(direction * boostedSpeed * Time.deltaTime, Space.World);
    }

    // ========== 按钮调用方法 ==========
    public void MoveForward() => Move(Vector3.forward);
    public void MoveBack() => Move(Vector3.back);
    public void MoveLeft() => Move(Vector3.left);
    public void MoveRight() => Move(Vector3.right);

    // ========== 旋转控制 ==========
    public void TurnLeft()
    {
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    // ========== 缩放控制 ==========
    public void ZoomIn()
    {
        Vector3 newScale = transform.localScale * (1 + zoomSpeed * Time.deltaTime);
        newScale = Vector3.Min(newScale, originalScale * maxZoom);
        transform.localScale = newScale;
    }

    public void ZoomOut()
    {
        Vector3 newScale = transform.localScale * (1 - zoomSpeed * Time.deltaTime);
        newScale = Vector3.Max(newScale, originalScale * minZoom);
        transform.localScale = newScale;
    }

    // ========== 重置功能 ==========
    public void ResetPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    public void ResetScale()
    {
        transform.localScale = originalScale;
    }

    public void ResetAll()
    {
        ResetPosition();
        ResetScale();
    }

    // ========== 场景控制 ==========
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}