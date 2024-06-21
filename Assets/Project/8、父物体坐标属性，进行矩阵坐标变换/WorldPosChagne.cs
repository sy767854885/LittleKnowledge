using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPosChagne : MonoBehaviour
{

    public GameObject go;

    public GameObject go_2;

    void Start()
    {
        Debug.Log("TransformPointWithRotation:  " + TransformPointWithRotation(go.transform.eulerAngles, go.transform.position, go_2.transform.localPosition));
    }

    /// <summary>
    /// �任����  �ֲ�����ת��Ϊ��������
    /// </summary>
    /// <returns></returns>
    private Vector3 TransformPointWithRotation(Vector3 axiseulerAngles, Vector3 axisPos, Vector3 initialLocalPosition)
    {
        // �����任����
        Quaternion axisRotation = Quaternion.Euler(axiseulerAngles);
        Matrix4x4 transformationMatrix = Matrix4x4.TRS(axisPos, axisRotation, Vector3.one);

        // ���ֲ�������ʾΪ�������
        Vector4 homogeneousPoint = new Vector4(initialLocalPosition.x, initialLocalPosition.y, initialLocalPosition.z, 1);

        // ���ֲ����������������任�������
        Vector4 transformedHomogeneousPoint = transformationMatrix * homogeneousPoint;

        // �������ǰ����������Ϊ���յ���������
        Vector3 finalPosition = new Vector3(transformedHomogeneousPoint.x, transformedHomogeneousPoint.y, transformedHomogeneousPoint.z);

        return finalPosition;
    }
}
