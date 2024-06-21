/****************************************************
    文件：CalcuateAngle.cs
	作者：Sy
    邮箱: 767854885@qq.com
    日期：#CreateTime#
	功能：Nothing
*****************************************************/

using UnityEngine;

public class CalcuateAngle : MonoBehaviour 
{
    public GameObject cube;
    public GameObject[] cubes;
    public GameObject duibi;
    private void Update()
    {
        // 检测鼠标左键是否按下
        if (Input.GetMouseButtonDown(0))
        {
            // 从摄像机发射一条射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果射线击中了物体
            if (Physics.Raycast(ray, out hit))
            {
                // 检测到的物体上有一个Collider
                if (hit.collider != null)
                {
                    // 计算相机与点击点的向量
                    Vector3 direction = hit.point - Camera.main.transform.position;
                    // 通过向量的方向，计算旋转角度
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    // 输出被点击的物体的名称
                    Debug.Log("Clicked on: " + hit.collider.gameObject.name);
                    GameObject go = Instantiate(cube);
                    go.transform.position = hit.point;
                    go.transform.localEulerAngles = rotation.eulerAngles;
                    Debug.Log("go localEulerAngles: " + rotation.eulerAngles);
                }
            }
        }
    }
}