using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ����ʵ��2DͼƬ��ƽ�ƺ����ŵĿ�������
/// �����קʵ��ƽ�ƣ�������ʵ�����š�
/// </summary>
public class ImageMovementAndZoomController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    // Ŀ��ͼƬ��RectTransform�����ڵ���ͼƬ��λ�ú�����
    private RectTransform targetImage;

    // ���ڴ洢��갴��ʱ��ƫ����
    private Vector2 offset;

    /// <summary>
    /// ��ʼ���������ڽű�����ʱ��ȡĿ��ͼƬ��RectTransform�����
    /// </summary>
    private void Start()
    {
        // ��ȡ��ǰ�����ϵ� RectTransform ���
        targetImage = GetComponent<RectTransform>();
    }

    /// <summary>
    /// ��갴��ʱ��������¼�����ͼƬ�ĳ�ʼƫ������
    /// </summary>
    /// <param name="eventData">Pointer�¼����ݣ�������갴�µ���ϸ��Ϣ��</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��������Ļ����ת��ΪͼƬ�ı������꣬����¼ƫ����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        targetImage,          // ����1��Ŀ��RectTransform��������Ļ����Ҫת�����ı�������ϵ�����ο����Σ���
        eventData.position,   // ����2����굱ǰ����Ļ���꣨PointerEventData�ṩ�����λ�ã���λΪ���أ���
        eventData.pressEventCamera, // ����3������������ڴ���UI��ͶӰ�����Ϊnull�������Canvas��Screen Space Overlayģʽ��
        out offset            // ����4�����������ת����ı������꣨�����targetImage�����½ǣ���λ��RectTransformһ�£���
    );
    }

    /// <summary>
    /// �����קʱ������ʵʱ����ͼƬ��λ�á�
    /// </summary>
    /// <param name="eventData">Pointer�¼����ݣ����������ק����ϸ��Ϣ��</param>
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localMousePosition;
        // ��������Ļ����ת��ΪͼƬ�ı�������
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
         targetImage,                      // ����1��Ŀ��RectTransform��������Ļ����ת�����ı�������ϵ����ͼƬ�Ĳο����Σ���
         eventData.position,               // ����2����굱ǰ����Ļ���꣨��PointerEventData�ṩ����λΪ���أ���
         eventData.pressEventCamera,       // ����3������������ڴ���UI��ͶӰ�����Ϊnull�������Canvas��Screen Space Overlayģʽ��
         out localMousePosition))          // ����4���������������ת����ı������꣨�����targetImage�����½ǣ���λ��RectTransformһ�£���
        {
            // ���ת���ɹ�������ִ�������߼�

            // ������굱ǰλ�����¼��ƫ�����Ĳ�ֵ���õ������ƶ���
            targetImage.anchoredPosition += (localMousePosition - offset);
            // ���ƶ����ӵ�Ŀ��RectTransform��ê��λ�ã��Ӷ�����ͼƬ��Canvas�е�λ��
        }
    }

    /// <summary>
    /// ������ʵ�����Ź��ܡ�
    /// </summary>
    private void Update()
    {
        // ��ȡ�����ֵ�����ֵ
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            // �������ű���
            float scaleFactor = 1 + scroll;

            // Ӧ�����ŵ�Ŀ��ͼƬ
            targetImage.localScale *= scaleFactor;
        }
    }

  
}
