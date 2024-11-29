using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 用于实现2D图片的平移和缩放的控制器类
/// 鼠标拖拽实现平移，鼠标滚轮实现缩放。
/// </summary>
public class ImageMovementAndZoomController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    // 目标图片的RectTransform，用于调整图片的位置和缩放
    private RectTransform targetImage;

    // 用于存储鼠标按下时的偏移量
    private Vector2 offset;

    /// <summary>
    /// 初始化方法，在脚本启动时获取目标图片的RectTransform组件。
    /// </summary>
    private void Start()
    {
        // 获取当前对象上的 RectTransform 组件
        targetImage = GetComponent<RectTransform>();
    }

    /// <summary>
    /// 鼠标按下时触发，记录鼠标与图片的初始偏移量。
    /// </summary>
    /// <param name="eventData">Pointer事件数据，包含鼠标按下的详细信息。</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // 将鼠标的屏幕坐标转换为图片的本地坐标，并记录偏移量
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        targetImage,          // 参数1：目标RectTransform，定义屏幕坐标要转换到的本地坐标系（即参考矩形）。
        eventData.position,   // 参数2：鼠标当前的屏幕坐标（PointerEventData提供的鼠标位置，单位为像素）。
        eventData.pressEventCamera, // 参数3：摄像机，用于处理UI的投影。如果为null，则假设Canvas是Screen Space Overlay模式。
        out offset            // 参数4：输出参数，转换后的本地坐标（相对于targetImage的左下角，单位与RectTransform一致）。
    );
    }

    /// <summary>
    /// 鼠标拖拽时触发，实时更新图片的位置。
    /// </summary>
    /// <param name="eventData">Pointer事件数据，包含鼠标拖拽的详细信息。</param>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localMousePosition;
        // 将鼠标的屏幕坐标转换为图片的本地坐标
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
         targetImage,                      // 参数1：目标RectTransform，定义屏幕坐标转换到的本地坐标系（即图片的参考矩形）。
         eventData.position,               // 参数2：鼠标当前的屏幕坐标（由PointerEventData提供，单位为像素）。
         eventData.pressEventCamera,       // 参数3：摄像机，用于处理UI的投影。如果为null，则假设Canvas是Screen Space Overlay模式。
         out localMousePosition))          // 参数4：输出参数，接收转换后的本地坐标（相对于targetImage的左下角，单位与RectTransform一致）。
        {
            // 如果转换成功，继续执行以下逻辑

            // 计算鼠标当前位置与记录的偏移量的差值，得到鼠标的移动量
            targetImage.anchoredPosition += (localMousePosition - offset);
            // 将移动量加到目标RectTransform的锚点位置，从而更新图片在Canvas中的位置
        }
    }

    /// <summary>
    /// 鼠标滚轮实现缩放功能。
    /// </summary>
    private void Update()
    {
        // 获取鼠标滚轮的输入值
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // 计算缩放比例
            float scaleFactor = 1 + scroll;

            // 应用缩放到目标图片
            targetImage.localScale *= scaleFactor;
        }
    }

  
}
