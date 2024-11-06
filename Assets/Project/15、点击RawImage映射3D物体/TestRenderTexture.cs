using UnityEngine;
using UnityEngine.UI;

public class TestRenderTexture : MonoBehaviour
{
    // Ԥ��ӳ�����
    public Camera previewCamera;
    public RawImage previewImage;
    // ���߳���
    public float rayLength = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ��ȡ���������Ļ����
            Vector2 mousePosition = Input.mousePosition;

            // �ж�����Ƿ����� RawImage ��
            if (RectTransformUtility.RectangleContainsScreenPoint(previewImage.rectTransform, mousePosition, null))
            {
                // ת����Ļ����Ϊ RawImage �ľֲ�����
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(previewImage.rectTransform, mousePosition, null, out Vector2 localPoint))
                {
                    // ��ȡ RawImage �Ŀ�Ⱥ͸߶�
                    float imageWidth = previewImage.rectTransform.rect.width;
                    float imageHeight = previewImage.rectTransform.rect.height;

                    // ת��Ϊ Viewport ���� (0~1)
                    float viewportX = (localPoint.x / imageWidth);
                    float viewportY = (localPoint.y / imageHeight);

                    // ʹ��Ԥ������� Viewport ���괴������
                    Ray ray = previewCamera.ViewportPointToRay(new Vector2(viewportX, viewportY));

                    // ���������ײ
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength))
                    {
                        Debug.Log("��������������: " + hitInfo.transform.name);
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
                    }
                    else
                    {
                        // ���û���������壬�����̶����ȵ�����
                        Vector3 endPoint = ray.origin + ray.direction * rayLength;
                        Debug.DrawLine(ray.origin, endPoint, Color.blue, 1.0f);
                        Debug.Log("����δ�����κ����壬�����̶����ȵ�����");
                    }
                }
            }
        }
    }
}
