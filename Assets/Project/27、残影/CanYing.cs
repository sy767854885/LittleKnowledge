using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CanYing : MonoBehaviour
{
    // 残影生成的时间间隔（秒）
    public float interval = 0.1f;

    // 每个残影的生命周期（秒）
    public float lifeCycle = 2.0f;

    // 上次生成残影的时间
    float lastCombinedTime = 0.0f;

    // 普通网格过滤器数组（适用于静态模型）
    MeshFilter[] meshFilters = null;

    // 蒙皮网格渲染器数组（适用于动画角色）
    SkinnedMeshRenderer[] skinedMeshRenderers = null;

    // 当前生成的残影 GameObject 列表
    List<GameObject> objs = new List<GameObject>();

    // 初始化组件，获取当前对象和子对象的网格信息
    void Start()
    {
        // 获取所有静态 MeshFilter 组件
        meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();

        // 获取所有带动画的 SkinnedMeshRenderer 组件
        skinedMeshRenderers = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // 当对象被禁用时，销毁所有残影并清空列表
    void OnDisable()
    {
        foreach (GameObject go in objs)
        {
            // 立即销毁残影对象
            DestroyImmediate(go);
        }
        // 清空列表
        objs.Clear();
        objs = null;
    }

    // 每帧检查是否需要生成新的残影
    void Update()
    {
        // 超过设定时间间隔则生成残影
        if (Time.time - lastCombinedTime > interval)
        {
            // 记录当前生成时间
            lastCombinedTime = Time.time;

            // 处理带动画的模型（SkinnedMeshRenderer）
            for (int i = 0; skinedMeshRenderers != null && i < skinedMeshRenderers.Length; ++i)
            {
                // 烘焙出当前帧的静态网格
                Mesh mesh = new Mesh();
                skinedMeshRenderers[i].BakeMesh(mesh);

                // 创建残影对象
                GameObject go = new GameObject();
                go.hideFlags = HideFlags.HideAndDontSave;

                // 添加 MeshFilter 并赋值烘焙后的网格
                MeshFilter meshFilter = go.AddComponent<MeshFilter>();
                meshFilter.mesh = mesh;

                // 添加 MeshRenderer，并使用原始材质
                MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();
                meshRenderer.material = skinedMeshRenderers[i].material;

                // 初始化残影对象，设置位置、旋转、生命周期
                InitFadeInObj(go, skinedMeshRenderers[i].transform.position, skinedMeshRenderers[i].transform.rotation, lifeCycle);
            }

            // 处理静态模型（MeshFilter）
            for (int i = 0; meshFilters != null && i < meshFilters.Length; ++i)
            {
                // 克隆一个新的残影对象
                GameObject go = Instantiate(meshFilters[i].gameObject) as GameObject;

                // 初始化残影对象
                InitFadeInObj(go, meshFilters[i].transform.position, meshFilters[i].transform.rotation, lifeCycle);
            }
        }
    }

    // 初始化残影对象的位置、旋转，并挂载淡出组件
    private void InitFadeInObj(GameObject go, Vector3 position, Quaternion rotation, float lifeCycle)
    {
        // 设置不保存和不显示于层级面板
        go.hideFlags = HideFlags.HideAndDontSave;

        // 设置位置和旋转
        go.transform.position = position;
        go.transform.rotation = rotation;

        // 添加控制透明度渐变并销毁的组件
        FadInOut fi = go.AddComponent<FadInOut>();
        fi.lifeCycle = lifeCycle;

        // 加入残影列表，便于销毁
        objs.Add(go);
    }
}
