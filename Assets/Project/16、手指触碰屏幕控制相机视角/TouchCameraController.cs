
using UnityEngine;
//1、	单指在界面中按下，进行拖动，可旋转相机视角。
//2、	双指在界面中按下，进行拖动，可平移相机视角。
//3、	双指在界面中按下，向内进行拖动，可缩放相机视角，向外进行拖动，可放大相机视角。

public class TouchCameraController : MonoBehaviour
{
    // 摄像机的前进速度
    public float forwardSpeed = 1.0f;

    // 摄像机的后退速度
    public float backwardSpeed = 0.5f;

    // 摄像机的旋转速度
    public float rotationSpeed = 0.2f;


    // 每帧更新摄像机的移动和旋转
    void Update()
    {
        // 检测是否有两个手指触摸屏幕
        if (Input.touchCount == 2)
        {
            // 获取两个手指的触摸数据
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 计算两根手指前一帧的位置
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 计算前一帧和当前帧两指间的距离
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 计算距离差以控制摄像机的移动
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 前后移动摄像机
            transform.Translate(Vector3.forward * deltaMagnitudeDiff * Time.deltaTime * 0.5f * -1);

            // 计算并应用摄像机的平移方向
            Vector2 moveDirection = (touchZero.deltaPosition + touchOne.deltaPosition) / 2f;
            transform.Translate(new Vector3(moveDirection.x * -1, moveDirection.y * -1, 0) * 0.6f * Time.deltaTime, Space.Self);
        }
        // 检测是否有一个手指触摸屏幕并移动
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // 获取单指移动的增量位置
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // 计算旋转角度
            float rotationX = touchDeltaPosition.y * rotationSpeed;
            float rotationY = touchDeltaPosition.x * rotationSpeed;

            // 绕X轴旋转摄像机（垂直旋转）
            transform.Rotate(Vector3.left, rotationX, Space.Self);

            // 绕Y轴旋转摄像机（水平旋转）
            transform.Rotate(Vector3.up, rotationY, Space.World);
        }
    }
}
