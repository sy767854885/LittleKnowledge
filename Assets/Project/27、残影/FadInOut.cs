using UnityEngine;

// 控制 GameObject 在指定生命周期内逐渐变透明并销毁
public class FadInOut : MonoBehaviour
{
    // 生命周期：单位为秒，代表对象存在的时间
    public float lifeCycle = 2.0f;

    // 记录开始时间
    private float startTime;

    // 当前物体的材质，用于控制颜色透明度
    private Material mat;

    // 初始化函数，在对象激活时调用一次
    void Start()
    {
        // 记录当前时间作为起始点
        startTime = Time.time;

        // 获取物体上的 MeshRenderer 组件
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // 如果没有 MeshRenderer 或没有材质，禁用此脚本
        if (!meshRenderer || meshRenderer.material == null)
        {
            enabled = false;
            return;
        }

        // 获取该材质的实例，避免修改共享材质
        mat = meshRenderer.material;

        // 设置材质为可透明混合的模式
        SetMaterialToFadeMode(mat);
    }

    // 每帧调用一次，控制透明度变化与销毁逻辑
    void Update()
    {
        // 计算已过的时间
        float time = Time.time - startTime;

        // 如果超过生命周期，销毁该 GameObject
        if (time > lifeCycle)
        {
            Destroy(gameObject);
        }
        else
        {
            // 剩余时间占总生命周期的比例（用于透明度）
            float remainderTime = 1 - (time / lifeCycle);

            // 如果材质有效，则更新颜色透明度
            if (mat != null)
            {
                // 获取当前颜色，调整 alpha 值（透明度）
                Color col = mat.color;
                col.a = remainderTime;
                mat.color = col; // 应用修改后的颜色
            }
        }
    }

    // 设置材质为 Fade 模式，使其支持透明渐变效果
    void SetMaterialToFadeMode(Material material)
    {
        // "_Mode" 是 Unity 标准着色器中的混合模式参数
        // 0: Opaque, 1: Cutout, 2: Fade, 3: Transparent
        material.SetFloat("_Mode", 2); // 设置为 Fade 模式

        // 设置混合参数，实现 alpha 混合效果
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

        // 禁用深度写入，防止遮挡
        material.SetInt("_ZWrite", 0);

        // 关闭 Alpha 测试关键词（不裁剪透明区域）
        material.DisableKeyword("_ALPHATEST_ON");

        // 启用 Alpha 混合关键词
        material.EnableKeyword("_ALPHABLEND_ON");

        // 禁用 Alpha 预乘关键词
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

        // 设置渲染队列为透明（比不透明物体后渲染）
        material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}
