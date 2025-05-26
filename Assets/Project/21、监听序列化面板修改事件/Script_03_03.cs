
using UnityEditor;
using UnityEngine;

/// <summary>
/// Script_03_03 是一个 MonoBehaviour 脚本，包含两个序列化字段：Id 和 Name。
/// 并在 Unity 编辑器中通过自定义 Inspector 的方式，显示自定义标签和变更日志。
/// </summary>
public class Script_03_03 : MonoBehaviour
{
    [SerializeField]
    private int Id;        // 主键 ID，序列化后在 Inspector 中可见（但字段本身是 private）

    [SerializeField]
    private string Name;   // 名称，同样是私有但可在 Inspector 中显示

#if UNITY_EDITOR
    /// <summary>
    /// 自定义 Inspector 类，必须继承自 UnityEditor.Editor。
    /// 该类必须放在 UNITY_EDITOR 条件编译中，避免在构建项目时编译错误。
    /// </summary>
    [CustomEditor(typeof(Script_03_03))] // 指定这个编辑器类用于 Script_03_03 脚本
    public class ScriptInsector : Editor
    {
        /// <summary>
        /// 重写 OnInspectorGUI 方法来自定义 Inspector 面板的显示内容。
        /// </summary>
        public override void OnInspectorGUI()
        {
            // 更新序列化对象的状态（必须先调用）
            serializedObject.Update();

            // 查找序列化字段：Id 和 Name
            SerializedProperty propertyId = serializedObject.FindProperty(nameof(Id));
            SerializedProperty propertyName = serializedObject.FindProperty(nameof(Name));

            // 开始监听 Id 字段是否发生变化
            EditorGUI.BeginChangeCheck();
            // 使用中文标签绘制一个整数输入框，绑定到 propertyId.intValue
            propertyId.intValue = EditorGUILayout.IntField("主键", propertyId.intValue);
            if (EditorGUI.EndChangeCheck())
            {
                // 当 Id 值发生变化时输出一条日志
                Debug.Log($"主键发生变化:{propertyId.intValue}");
            }

            // 开始监听 Name 字段是否发生变化
            EditorGUI.BeginChangeCheck();
            // 使用中文标签绘制一个文本输入框，绑定到 propertyName.stringValue
            propertyName.stringValue = EditorGUILayout.TextField("名字", propertyName.stringValue);
            if (EditorGUI.EndChangeCheck())
            {
                // 当 Name 值发生变化时输出一条日志
                Debug.Log($"名字发生变化:{propertyName.stringValue}");
            }

            // GUI.changed 在旧版本中用于检测 GUI 内容是否变化
            // 在这个上下文中未做额外处理，保留空逻辑
            if (GUI.changed)
            {
                // 如果有变化，可以在此处添加额外逻辑（例如刷新界面、触发事件等）
            }

            // 应用属性的修改结果到目标对象
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
