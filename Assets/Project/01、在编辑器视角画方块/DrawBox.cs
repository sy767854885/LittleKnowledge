using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBox : MonoBehaviour
{
    [SerializeField]
    private float sizeX = 0;
    [SerializeField]
    private float sizeY = 0;
    [SerializeField]
    private float sizeZ = 0;
    [SerializeField]
    private float rotY = 0;

    private void OnDrawGizmos()
    {
        #region �ײ���
        //�ײ����Ͻǵĵ�
        Vector3 down_LeftTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, 0, sizeZ / 2);
        //�ײ����½ǵĵ�
        Vector3 down_LeftDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, 0, sizeZ / 2);
        //�����ײ���ߵ���
        Gizmos.DrawLine(down_LeftTop, down_LeftDown);


        //�ײ����Ͻǵĵ�
        Vector3 down_RightTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, 0, -sizeZ / 2);
        //�ײ����½ǵĵ�
        Vector3 down_RightDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, 0, -sizeZ / 2);
        //�����ײ��ұߵ���
        Gizmos.DrawLine(down_RightTop, down_RightDown);
        //�����ײ��ϱߵ���
        Gizmos.DrawLine(down_LeftTop, down_RightTop);
        //�����ײ��±ߵ���
        Gizmos.DrawLine(down_LeftDown, down_RightDown);
        #endregion
        #region ������
        //�����������Ͻǵĵ�
        Vector3 top_LeftTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, sizeY, sizeZ / 2);
        //�����������½ǵĵ�
        Vector3 top_LeftDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, sizeY , sizeZ / 2);
        //����������ߵ���
        Gizmos.DrawLine(top_LeftTop, top_LeftDown);
        //�����������Ͻǵĵ�
        Vector3 top_RightTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, sizeY, -sizeZ / 2);
        //�����������½ǵĵ�
        Vector3 top_RightDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, sizeY, -sizeZ / 2);
        //����������ߵ���
        Gizmos.DrawLine(top_RightTop, top_RightDown);
        //���������ϱߵ���
        Gizmos.DrawLine(top_LeftTop, top_RightTop);
        //���������±ߵ���
        Gizmos.DrawLine(top_LeftDown, top_RightDown);
        #endregion


        #region �в�������
        //�����м����Ͻǵ���
        Gizmos.DrawLine(down_LeftTop, top_LeftTop);
        //�����м����½ǵ���
        Gizmos.DrawLine(down_LeftDown, top_LeftDown);
        //�����м����Ͻǵ���
        Gizmos.DrawLine(down_RightTop, top_RightTop);
        //�����м����½ǵ���
        Gizmos.DrawLine(down_RightDown, top_RightDown);
        #endregion
    }
}
