using UnityEngine;
using UnityEngine.UI;

public class TestRenderTexture : MonoBehaviour
{
    // 预览映射相机，负责从特定视角渲染场景
    public Camera previewCamera;
    // 用于显示相机渲染结果的 RawImage 组件
    public RawImage previewImage;
    // 射线的最大长度，用于射线检测
    public float rayLength = 10f;

    void Update()
    {
        // 检测是否按下鼠标左键
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标点击时的屏幕坐标
            Vector2 mousePosition = Input.mousePosition;

            // 判断鼠标点击是否在 RawImage 区域内
            if (RectTransformUtility.RectangleContainsScreenPoint(previewImage.rectTransform, mousePosition, null))
            {
                // 将鼠标的屏幕坐标转换为 RawImage 内部的局部坐标
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(previewImage.rectTransform, mousePosition, null, out Vector2 localPoint))
                {
                    // 获取 RawImage 的宽度和高度
                    float imageWidth = previewImage.rectTransform.rect.width;
                    float imageHeight = previewImage.rectTransform.rect.height;

                    // 将局部坐标转换为 Viewport 坐标 (0~1)
                    float viewportX = (localPoint.x / imageWidth);
                    float viewportY = (localPoint.y / imageHeight);

                    // 使用预览相机的 Viewport 坐标生成一条射线
                    Ray ray = previewCamera.ViewportPointToRay(new Vector2(viewportX, viewportY));

                    // 检测射线是否与场景中的物体发生碰撞
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength))
                    {
                        // 如果射线碰到物体，输出碰撞物体的信息
                        Debug.Log("射线碰到的物体: " + hitInfo.transform.name);
                        // 可视化射线的碰撞路径
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
                    }
                    else
                    {
                        // 如果射线没有碰到任何物体，绘制一个固定长度的射线
                        Vector3 endPoint = ray.origin + ray.direction * rayLength;
                        Debug.DrawLine(ray.origin, endPoint, Color.blue, 1.0f);
                        Debug.Log("射线未碰到任何物体，画出固定长度的射线");
                    }
                }
            }
        }
    }
}
