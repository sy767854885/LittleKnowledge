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
    /// 变换矩阵  局部坐标转换为世界坐标
    /// </summary>
    /// <returns></returns>
    private Vector3 TransformPointWithRotation(Vector3 axiseulerAngles, Vector3 axisPos, Vector3 initialLocalPosition)
    {
        // 构建变换矩阵
        Quaternion axisRotation = Quaternion.Euler(axiseulerAngles);
        Matrix4x4 transformationMatrix = Matrix4x4.TRS(axisPos, axisRotation, Vector3.one);

        // 将局部坐标点表示为齐次坐标
        Vector4 homogeneousPoint = new Vector4(initialLocalPosition.x, initialLocalPosition.y, initialLocalPosition.z, 1);

        // 将局部坐标点的齐次坐标与变换矩阵相乘
        Vector4 transformedHomogeneousPoint = transformationMatrix * homogeneousPoint;

        // 将结果的前三个分量作为最终的世界坐标
        Vector3 finalPosition = new Vector3(transformedHomogeneousPoint.x, transformedHomogeneousPoint.y, transformedHomogeneousPoint.z);

        return finalPosition;
    }
}
