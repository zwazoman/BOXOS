using System.Collections.Generic;
using System.Reflection;
using PurrNet.Contributors;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PurrNet.Editor
{
    struct Contributor
    {
        public string name;
        public string url;
    }

    [CustomEditor(typeof(NetworkIdentity), true)]
    [CanEditMultipleObjects]
#if TRI_INSPECTOR_PACKAGE
    public class NetworkIdentityInspector : TriInspector.Editors.TriEditor
#elif ODIN_INSPECTOR
    public class NetworkIdentityInspector : Sirenix.OdinInspector.Editor.OdinEditor
#else
    public class NetworkIdentityInspector : UnityEditor.Editor
#endif
    {
        private SerializedProperty _networkRules;
        private SerializedProperty _visitiblityRules;
        private readonly List<Contributor> _contributors = new ();

#if TRI_INSPECTOR_PACKAGE || ODIN_INSPECTOR
        protected override void OnEnable()
#else
        protected virtual void OnEnable()
#endif
        {
#if TRI_INSPECTOR_PACKAGE
            base.OnEnable();
#endif
            try
            {
                _networkRules = serializedObject.FindProperty("_networkRules");
                _visitiblityRules = serializedObject.FindProperty("_visitiblityRules");
            }
            catch
            {
                // ignored
            }

            if (target)
            {
                var targetType = target.GetType();
                var attributes = targetType.GetCustomAttributes(typeof(ContributorAttribute), true);

                for (var i = 0; i < attributes.Length; i++)
                {
                    var attr = (ContributorAttribute)attributes[i];

                    if (attr == null)
                        continue;

                    _contributors.Add(new Contributor
                    {
                        name = attr.name,
                        url = attr.url
                    });
                }
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            return null;
        }

        public override void OnInspectorGUI()
        {
            var identity = (NetworkIdentity)target;
            bool hasNetworkManagerAsChild = identity && identity.GetComponentInChildren<NetworkManager>();

            if (hasNetworkManagerAsChild)
                EditorGUILayout.HelpBox("NetworkIdentity is a child of a NetworkManager. This is not supported.",
                    MessageType.Error);

            base.OnInspectorGUI();

            DrawIdentityInspector();
            GUI.enabled = true;
            DrawPurrButtons(identity);
            DrawContributors();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawContributors()
        {
            if (_contributors.Count == 0)
                return;

            GUILayout.Space(5);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Contributors");
            GUILayout.FlexibleSpace();
            foreach (var contributor in _contributors)
            {
                if (string.IsNullOrEmpty(contributor.url))
                    continue;

                GUILayout.Label("@", GUILayout.ExpandWidth(false));
                if (GUILayout.Button(contributor.name, EditorStyles.linkLabel, GUILayout.ExpandWidth(false)))
                    Application.OpenURL(contributor.url);
            }
            EditorGUILayout.EndHorizontal();
        }

        protected void DrawIdentityInspector()
        {
            GUILayout.Space(5);

            var identities = targets.Length;
            var identity = (NetworkIdentity)target;

            if (!identity)
            {
                EditorGUILayout.LabelField("Invalid identity");
                return;
            }

            HandleOverrides(identity, identities > 1);
            HandleStatus(identity, identities > 1);
        }

        private bool _foldoutVisible;

        private void HandleOverrides(NetworkIdentity identity, bool multi)
        {
            if (multi || identity.isSpawned)
                GUI.enabled = false;

            string label = "Override Defaults";

            if (!multi)
            {
                bool isNetworkRulesOverridden = _networkRules.objectReferenceValue != null;
                bool isVisibilityRulesOverridden = _visitiblityRules.objectReferenceValue != null;

                int overridenCount = (isNetworkRulesOverridden ? 1 : 0) + (isVisibilityRulesOverridden ? 1 : 0);

                if (overridenCount > 0)
                {
                    label += " (";

                    if (isNetworkRulesOverridden)
                    {
                        label += overridenCount > 1 ? "P," : "P";
                    }

                    if (isVisibilityRulesOverridden)
                        label += "V";

                    label += ")";
                }
            }
            else
            {
                label += " (...)";
            }

            var old = GUI.enabled;
            GUI.enabled = !multi;
            _foldoutVisible = EditorGUILayout.BeginFoldoutHeaderGroup(_foldoutVisible, label);
            GUI.enabled = old;
            if (!multi && _foldoutVisible)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_networkRules, new GUIContent("Permissions Override"));
                EditorGUILayout.PropertyField(_visitiblityRules, new GUIContent("Visibility Override"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        private bool _debuggingVisible;
        private bool _observersVisible;

        private void HandleStatus(NetworkIdentity identity, bool multi)
        {
            if (multi)
            {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("...");
                EditorGUILayout.EndHorizontal();
            }
            else if (identity.isSpawned)
            {
                if (identity.isServer)
                {
                    var old = GUI.enabled;
                    GUI.enabled = true;
                    PrintObserversDropdown(identity);
                    GUI.enabled = old;
                }

                EditorGUILayout.BeginHorizontal("box", GUILayout.ExpandWidth(false));
                GUILayout.Label($"ID: {identity.id}", GUILayout.ExpandWidth(false));
                GUILayout.FlexibleSpace();
                GUILayout.Label($"Owner ID: {(identity.owner.HasValue ? identity.owner.Value.ToString() : "None")}",
                    GUILayout.ExpandWidth(false));
                GUILayout.FlexibleSpace();
                GUILayout.Label(
                    $"Local Player: {(identity.localPlayer.HasValue ? identity.localPlayer.Value.ToString() : "None")}",
                    GUILayout.ExpandWidth(false));
                EditorGUILayout.EndHorizontal();
            }
            else if (Application.isPlaying)
            {
                EditorGUILayout.BeginHorizontal("box");
                EditorGUILayout.LabelField("Not Spawned");
                EditorGUILayout.EndHorizontal();
            }

#if PURRNET_DEBUG_NETWORK_IDENTITY
            var old2 = GUI.enabled;
            GUI.enabled = false;

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandWidth(false));

            EditorGUILayout.LabelField($"prefabId: {identity.prefabId}");
            EditorGUILayout.LabelField($"componentIndex: {identity.componentIndex}");
            EditorGUILayout.LabelField($"shouldBePooled: {identity.shouldBePooled}");
            EditorGUILayout.ObjectField("parent", identity.parent, typeof(NetworkIdentity), true);

            string path = "";

            if (identity.invertedPathToNearestParent != null)
            {
                for (var index = 0; index < identity.invertedPathToNearestParent.Length; index++)
                {
                    var parent = identity.invertedPathToNearestParent[index];
                    bool isLast = index == identity.invertedPathToNearestParent.Length - 1;
                    path += parent + (isLast ? ";" : " -> ");
                }
            }

            EditorGUILayout.LabelField($"pathToNearestParent: {path}");
            EditorGUILayout.LabelField($"Direct Children ({identity.directChildren?.Count ?? 0}):");

            if (identity.directChildren != null)
            {
                EditorGUI.indentLevel++;
                foreach (var child in identity.directChildren)
                {
                    EditorGUILayout.ObjectField(child, typeof(NetworkIdentity), true);
                }
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndVertical();
            GUI.enabled = old2;
#endif
        }

        private void PrintObserversDropdown(NetworkIdentity identity)
        {
            _observersVisible =
                EditorGUILayout.BeginFoldoutHeaderGroup(_observersVisible, $"Observers ({identity.observers.Count})");

            if (_observersVisible)
            {
                EditorGUI.indentLevel++;
                foreach (var observer in identity.observers)
                {
                    EditorGUILayout.BeginHorizontal("box");
                    EditorGUILayout.LabelField(observer.ToString());
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        protected void DrawPurrButtons(NetworkIdentity targetIdentity)
        {
            if (targetIdentity == null)
                return;

            GUILayout.Space(5);

            var methods = targetIdentity.GetType().GetMethods(
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            bool foundAnyButtons = false;

            foreach (var method in methods)
            {
                var buttonAttr = method.GetCustomAttribute<PurrButtonAttribute>();

                if (buttonAttr != null)
                {
                    if (!foundAnyButtons)
                    {
                        EditorGUILayout.LabelField("PurrButtons", EditorStyles.boldLabel);
                        foundAnyButtons = true;
                    }

                    string buttonName = !string.IsNullOrEmpty(buttonAttr.ButtonName)
                        ? buttonAttr.ButtonName
                        : ObjectNames.NicifyVariableName(method.Name);

                    if (GUILayout.Button(buttonName))
                    {
                        var parameters = method.GetParameters();

                        if (parameters.Length == 0)
                        {
                            method.Invoke(targetIdentity, null);
                        }
                        else
                        {
                            Debug.LogWarning($"Cannot invoke method '{method.Name}' with PurrButton attribute because it has parameters. PurrButton only works with parameterless methods.");
                        }
                    }
                }
            }
        }
    }
}
