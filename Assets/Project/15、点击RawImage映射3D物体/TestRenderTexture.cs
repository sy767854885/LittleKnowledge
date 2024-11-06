using UnityEngine;
using UnityEngine.UI;

public class TestRenderTexture : MonoBehaviour
{
    // Ԥ��ӳ�������������ض��ӽ���Ⱦ����
    public Camera previewCamera;
    // ������ʾ�����Ⱦ����� RawImage ���
    public RawImage previewImage;
    // ���ߵ���󳤶ȣ��������߼��
    public float rayLength = 10f;

    void Update()
    {
        // ����Ƿ���������
        if (Input.GetMouseButtonDown(0))
        {
            // ��ȡ�����ʱ����Ļ����
            Vector2 mousePosition = Input.mousePosition;

            // �ж�������Ƿ��� RawImage ������
            if (RectTransformUtility.RectangleContainsScreenPoint(previewImage.rectTransform, mousePosition, null))
            {
                // ��������Ļ����ת��Ϊ RawImage �ڲ��ľֲ�����
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(previewImage.rectTransform, mousePosition, null, out Vector2 localPoint))
                {
                    // ��ȡ RawImage �Ŀ�Ⱥ͸߶�
                    float imageWidth = previewImage.rectTransform.rect.width;
                    float imageHeight = previewImage.rectTransform.rect.height;

                    // ���ֲ�����ת��Ϊ Viewport ���� (0~1)
                    float viewportX = (localPoint.x / imageWidth);
                    float viewportY = (localPoint.y / imageHeight);

                    // ʹ��Ԥ������� Viewport ��������һ������
                    Ray ray = previewCamera.ViewportPointToRay(new Vector2(viewportX, viewportY));

                    // ��������Ƿ��볡���е����巢����ײ
                    if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength))
                    {
                        // ��������������壬�����ײ�������Ϣ
                        Debug.Log("��������������: " + hitInfo.transform.name);
                        // ���ӻ����ߵ���ײ·��
                        Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 1.0f);
                    }
                    else
                    {
                        // �������û�������κ����壬����һ���̶����ȵ�����
                        Vector3 endPoint = ray.origin + ray.direction * rayLength;
                        Debug.DrawLine(ray.origin, endPoint, Color.blue, 1.0f);
                        Debug.Log("����δ�����κ����壬�����̶����ȵ�����");
                    }
                }
            }
        }
    }
}
