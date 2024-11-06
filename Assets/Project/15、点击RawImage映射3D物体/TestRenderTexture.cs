using UnityEngine;
using UnityEngine.UI;

public class TestRenderTexture : MonoBehaviour
{
    // 预览映射相机
    public Camera previewCamera;
    public RawImage previewImage;
    // 射线长度
    public float rayLength = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标点击的屏幕坐标
            Vector2 mousePosition = Input.mousePosition;

            // 判断鼠标是否点击在 RawImage 上
            if (RectTransformUtility.RectangleContainsScreenPoint(previewImage.rectTransform, mousePosition, null))
            {
                // 转换屏幕坐标为 RawImage 的局部坐标
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(previewImage.rectTransform, mousePosition, null, out Vector2 localPoint))
                {
                    // 获取 RawImage 的宽度和高度
                    float imageWidth = previewImage.rectTransform.rect.width;
                    float imageHeight = previewImage.rectTransform.rect.height;

                    // 转换为 Viewport 坐标 (0~1)
                    float viewportX = (localPoint.x / imageWidth);
                    float viewportY = (localPoint.y / imageHeight);

                    // 使用预览相机从 Viewport 坐标创建射线
                    Ray ray = previewCamera.ViewportPointToRay(new Vector2(viewportX, viewportY));

                    // 检测射线碰撞
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength))
                    {
                        Debug.Log("射线碰到的物体: " + hitInfo.transform.name);
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
                    }
                    else
                    {
                        // 如果没有碰到物体，画出固定长度的射线
                        Vector3 endPoint = ray.origin + ray.direction * rayLength;
                        Debug.DrawLine(ray.origin, endPoint, Color.blue, 1.0f);
                        Debug.Log("射线未碰到任何物体，画出固定长度的射线");
                    }
                }
            }
        }
    }
}
