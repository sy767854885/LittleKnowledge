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
        #region 底部线
        //底部左上角的点
        Vector3 down_LeftTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, 0, sizeZ / 2);
        //底部左下角的点
        Vector3 down_LeftDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, 0, sizeZ / 2);
        //画出底部左边的线
        Gizmos.DrawLine(down_LeftTop, down_LeftDown);


        //底部右上角的点
        Vector3 down_RightTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, 0, -sizeZ / 2);
        //底部右下角的点
        Vector3 down_RightDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, 0, -sizeZ / 2);
        //画出底部右边的线
        Gizmos.DrawLine(down_RightTop, down_RightDown);
        //画出底部上边的线
        Gizmos.DrawLine(down_LeftTop, down_RightTop);
        //画出底部下边的线
        Gizmos.DrawLine(down_LeftDown, down_RightDown);
        #endregion
        #region 顶部线
        //画出顶部左上角的点
        Vector3 top_LeftTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, sizeY, sizeZ / 2);
        //画出顶部左下角的点
        Vector3 top_LeftDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, sizeY , sizeZ / 2);
        //画出顶部左边的线
        Gizmos.DrawLine(top_LeftTop, top_LeftDown);
        //画出顶部左上角的点
        Vector3 top_RightTop = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(sizeX / 2, sizeY, -sizeZ / 2);
        //画出顶部左下角的点
        Vector3 top_RightDown = transform.position + Quaternion.Euler(0, rotY, 0) * new Vector3(-sizeX / 2, sizeY, -sizeZ / 2);
        //画出顶部左边的线
        Gizmos.DrawLine(top_RightTop, top_RightDown);
        //画出顶部上边的线
        Gizmos.DrawLine(top_LeftTop, top_RightTop);
        //画出顶部下边的线
        Gizmos.DrawLine(top_LeftDown, top_RightDown);
        #endregion


        #region 中部连接线
        //画出中间左上角的线
        Gizmos.DrawLine(down_LeftTop, top_LeftTop);
        //画出中间左下角的线
        Gizmos.DrawLine(down_LeftDown, top_LeftDown);
        //画出中间右上角的线
        Gizmos.DrawLine(down_RightTop, top_RightTop);
        //画出中间右下角的线
        Gizmos.DrawLine(down_RightDown, top_RightDown);
        #endregion
    }
}
