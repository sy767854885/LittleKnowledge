
using UnityEditor;
using UnityEngine;

public class Script_03_01 : MonoBehaviour
{
    // 序列化字段，使其在 Inspector 面板中可见
    [SerializeField]
    private int Id; // 主键 ID

    [SerializeField]
    private string Name; // 名称

    [SerializeField]
    private GameObject Prefab; // 游戏对象预制体

    // 以下代码仅在 Unity 编辑器环境中编译
#if UNITY_EDITOR

    // 声明一个自定义 Inspector，用于编辑 Script_03_01 脚本的显示方式
    [CustomEditor(typeof(Script_03_01))]
    public class ScriptInsector : Editor // 自定义编辑器类需继承自 Editor
    {
        // 重写 OnInspectorGUI 方法，自定义 Inspector 面板内容
        public override void OnInspectorGUI()
        {
            // 更新序列化对象，准备绘制属性
            serializedObject.Update();

            // 获取序列化属性 Id
            SerializedProperty property = serializedObject.FindProperty(nameof(Id));
            // 使用 IntField 绘制整数输入框
            property.intValue = EditorGUILayout.IntField("主键", property.intValue);

            // 获取序列化属性 Name
            property = serializedObject.FindProperty(nameof(Name));
            // 使用 TextField 绘制文本输入框
            property.stringValue = EditorGUILayout.TextField("姓名", property.stringValue);

            // 获取序列化属性 Prefab
            property = serializedObject.FindProperty(nameof(Prefab));
            // 使用 ObjectField 绘制对象选择框，限定为 GameObject 类型
            property.objectReferenceValue = EditorGUILayout.ObjectField("游戏对象", property.objectReferenceValue, typeof(GameObject), true);

            // 应用修改，使更改生效
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
