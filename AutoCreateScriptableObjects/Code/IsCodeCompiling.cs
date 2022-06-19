#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Code
{
    public class IsCodeCompiling : EditorWindow
    {
        public static void Init(string name)
        {
            EditorPrefs.SetString("Key", name); // Save name
            var window = GetWindowWithRect(typeof(IsCodeCompiling), new Rect(0, 0, 300, 100));
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Compiling:", EditorApplication.isCompiling ? "Yes" : "No");
            Repaint();

            if (!EditorApplication.isCompiling)
            {
                CreateScriptableObject.CreateScriptableObj(EditorPrefs.GetString("Key"));
                EditorPrefs.DeleteKey("Key");
                Close();
            }
        }
    }
}
#endif