using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Unity.Mathematics;

namespace Liquid.Entities.Editor {

[CustomEditor(typeof(World), true)]
public class WorldInspector : UnityEditor.Editor {
    const int EntityPageSize = 64;
    static string[] customProperties = new[] { "entities" };
    static bool[] entityFoldouts = new bool[World.EntityMax];
    static bool showEntities = false;
    static bool showSystems = false;
    static int entityPage = 0;

    public override void OnInspectorGUI() {
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, customProperties);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.Separator();
        var world = target as World;

        var systemsHeader = $"Systems ({world.Systems().Count})";
        showSystems = EditorGUILayout.BeginFoldoutHeaderGroup(
            showSystems, systemsHeader);
        if (showSystems) {
            DrawSystems(world);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        var entitiesHeader = $"Entities ({world.UnsafeViewAll().Count})";
        showEntities = EditorGUILayout.BeginFoldoutHeaderGroup(
            showEntities, entitiesHeader);
        if (showEntities) {
            DrawEntities(world);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    void DrawSystems(World world) {
        ++EditorGUI.indentLevel;
        var systems = world.Systems();
        for (int i = 0; i < systems.Count; ++i) {
            var system = systems[i];
            EditorGUILayout.LabelField(system.GetType().Name);
        }
        --EditorGUI.indentLevel;
    }

    void DrawEntities(World world) {
        ++EditorGUI.indentLevel;
        var hidden = new GUIStyle(EditorStyles.foldout);
        hidden.normal.textColor = Color.gray;
        hidden.active.textColor = Color.gray;
        hidden.focused.textColor = Color.gray;

        var componentsField =
            typeof(World).GetField(
                "components",
                BindingFlags.Instance|BindingFlags.NonPublic);

        var components = componentsField.GetValue(world) as IComponentArray[];
        var entities = world.UnsafeViewAll();

        EditorGUILayout.Separator();

        bool pageEnd = EntityPageSize * (entityPage + 1) > entities.Count;
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(entityPage <= 0);
        if (GUILayout.Button("<-")) {
            entityPage--;
        }
        EditorGUI.EndDisabledGroup();

        EditorGUI.BeginDisabledGroup(pageEnd);
        if (GUILayout.Button("->")) {
            entityPage++;
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();

        var start = entityPage * EntityPageSize;
        for (int i = start; i < start + EntityPageSize; ++i) {
            if (i >= entities.Count) {
                break;
            }
            var entity = entities[i];
            var active = world.Contains<Active>(entity)
                       ? EditorStyles.foldout : hidden;

            var pure = world.Contains<GameObject>(entity) ? "" : " (Pure)";
            if (entity == Entity.Null)
                pure = " (Null)";

            var text = $"Entity {entity}{pure}";
            active.fontStyle = pure == "" ? FontStyle.Bold : FontStyle.Normal;

            entityFoldouts[i] =
                EditorGUILayout.Foldout(entityFoldouts[i], text, active);

            if (entityFoldouts[i] && entity != Entity.Null) {
                ++EditorGUI.indentLevel;
                bool isActive = world.Contains<Active>(entity);
                bool modActive =
                    EditorGUILayout.ToggleLeft("Enabled", isActive);
                if (modActive != isActive) {
                    world.SetActive(entity, modActive);
                }
                for (int j = 0; j < components.Length; ++j) {
                    var a = components[j];
                    if (a == null) continue;
                    var type = a.GetType().GetGenericArguments()[0];

                    if (world.Contains(entity, type)) {
                        var pureComponent =
                            typeof(Component).IsAssignableFrom(type)
                                || type == typeof(GameObject)
                                ? "" : " (Pure)";
                        EditorGUILayout.LabelField(type.Name + pureComponent);
                        DrawObjectFields(a.ReadData(entity), type);
                    }
                }
                --EditorGUI.indentLevel;
            }
        }
        --EditorGUI.indentLevel;
    }

    void DrawObjectFields(object data, Type type) {
        EditorGUI.BeginDisabledGroup(true);
        ++EditorGUI.indentLevel;
        var fields = type.GetFields();
        foreach (var field in fields) {
            var val = field.GetValue(data);
            switch (val) {
            case int i32:
                EditorGUILayout.IntField(field.Name, i32);
                break;
            case long i64:
                EditorGUILayout.LongField(field.Name, i64);
                break;
            case string str:
                EditorGUILayout.TextField(field.Name, str);
                break;
            case bool b:
                EditorGUILayout.Toggle(field.Name, b);
                break;
            case Vector2 vec2:
                EditorGUILayout.Vector2Field(field.Name, vec2);
                break;
            case Vector3 vec3:
                EditorGUILayout.Vector3Field(field.Name, vec3);
                break;
            case Vector4 vec4:
                EditorGUILayout.Vector4Field(field.Name, vec4);
                break;
            case float2 vec2:
                EditorGUILayout.Vector2Field(field.Name, vec2);
                break;
            case float3 vec3:
                EditorGUILayout.Vector3Field(field.Name, vec3);
                break;
            case float4 vec4:
                EditorGUILayout.Vector4Field(field.Name, vec4);
                break;
            default:
                if (val == null) {
                    EditorGUILayout.TextField(field.Name, "(null)");
                    break;
                }
                EditorGUILayout.TextField(field.Name, val.ToString());
                break;
            }
        }
        --EditorGUI.indentLevel;
        EditorGUI.EndDisabledGroup();
    }
}

} // namespace Liquid.Entities.Editor
