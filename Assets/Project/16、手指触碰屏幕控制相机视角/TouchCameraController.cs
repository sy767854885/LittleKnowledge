
using UnityEngine;
//1��	��ָ�ڽ����а��£������϶�������ת����ӽǡ�
//2��	˫ָ�ڽ����а��£������϶�����ƽ������ӽǡ�
//3��	˫ָ�ڽ����а��£����ڽ����϶�������������ӽǣ���������϶����ɷŴ�����ӽǡ�

public class TouchCameraController : MonoBehaviour
{
    // �������ǰ���ٶ�
    public float forwardSpeed = 1.0f;

    // ������ĺ����ٶ�
    public float backwardSpeed = 0.5f;

    // ���������ת�ٶ�
    public float rotationSpeed = 0.2f;


    // ÿ֡������������ƶ�����ת
    void Update()
    {
        // ����Ƿ���������ָ������Ļ
        if (Input.touchCount == 2)
        {
            // ��ȡ������ָ�Ĵ�������
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // ����������ָǰһ֡��λ��
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // ����ǰһ֡�͵�ǰ֡��ָ��ľ���
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // ���������Կ�����������ƶ�
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // ǰ���ƶ������
            transform.Translate(Vector3.forward * deltaMagnitudeDiff * Time.deltaTime * 0.5f * -1);

            // ���㲢Ӧ���������ƽ�Ʒ���
            Vector2 moveDirection = (touchZero.deltaPosition + touchOne.deltaPosition) / 2f;
            transform.Translate(new Vector3(moveDirection.x * -1, moveDirection.y * -1, 0) * 0.6f * Time.deltaTime, Space.Self);
        }
        // ����Ƿ���һ����ָ������Ļ���ƶ�
        else if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            // ��ȡ��ָ�ƶ�������λ��
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            // ������ת�Ƕ�
            float rotationX = touchDeltaPosition.y * rotationSpeed;
            float rotationY = touchDeltaPosition.x * rotationSpeed;

            // ��X����ת���������ֱ��ת��
            transform.Rotate(Vector3.left, rotationX, Space.Self);

            // ��Y����ת�������ˮƽ��ת��
            transform.Rotate(Vector3.up, rotationY, Space.World);
        }
    }
}
