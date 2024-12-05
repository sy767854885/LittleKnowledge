using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    // 旋转速度
    public float xRotationSpeed = 100f;  // x轴旋转速度
    public float yRotationSpeed = 100f;  // y轴旋转速度

    // 缩放速度
    public float scrollSpeed = 1f;

    // 相机移动速度
    public float moveSpeed = 1f;

    // 每帧更新函数
    void Update()
    {
        // 调用处理缩放、旋转和移动的函数
        HandleCameraZoom();
        HandleCameraRotation();
        HandleCameraMovement();
    }

    /// <summary>
    /// 处理相机的旋转。
    /// 按住右键时，控制相机的旋转。
    /// </summary>
    private void HandleCameraRotation()
    {
        // 检查是否按下右键
        if (Input.GetMouseButton(1))
        {
            // 获取鼠标的X和Y轴移动量
            float mouseXInput = Input.GetAxisRaw("Mouse X");
            float mouseYInput = Input.GetAxisRaw("Mouse Y");

            // 计算旋转增量
            float xRotationDelta = mouseXInput * xRotationSpeed * 0.02f;
            float yRotationDelta = -mouseYInput * yRotationSpeed * 0.02f;

            // 根据当前角度对相机进行旋转
            transform.Rotate(Vector3.up, xRotationDelta, Space.World);    // 绕世界坐标的Y轴旋转
            transform.Rotate(Vector3.right, yRotationDelta, Space.Self);  // 绕自身坐标的X轴旋转
        }
    }

    /// <summary>
    /// 处理相机的缩放。
    /// 使用鼠标滚轮进行前进或后退操作。
    /// </summary>
    private void HandleCameraZoom()
    {
        // 获取鼠标滚轮的输入
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // 根据滚轮输入调整相机的位置
        Vector3 newCameraPosition = transform.position + transform.forward * scrollInput * scrollSpeed;
        transform.position = newCameraPosition;
    }

    /// <summary>
    /// 处理相机的移动。
    /// 按住中键（鼠标左键）进行相机平移。
    /// </summary>
    private void HandleCameraMovement()
    {
        // 检查是否按下左键
        if (Input.GetMouseButton(0))
        {
            // 获取鼠标X和Y轴的输入值
            float mouseXInput = Input.GetAxis("Mouse X");
            float mouseYInput = Input.GetAxis("Mouse Y");

            // 计算相机的移动方向
            Vector3 moveDirection = (mouseXInput * -transform.right + mouseYInput * -transform.up);

            // 根据输入值移动相机
            transform.position += moveDirection * moveSpeed * 0.5f;
        }
    }
}
