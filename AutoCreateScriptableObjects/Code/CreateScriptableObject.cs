#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Code
{
    public static class CreateScriptableObject
    {
        // Path to your script template
        private const string c_PathToTemplate = "Assets/Templates/ScriptTemplate.cs";
        
        // Path to your folder you want the ScriptableObject to be placed in
        private const string c_FolderPath = "Assets/ScriptableObjects/";

        [MenuItem("Assets/Tools/CreateScriptableObject", false, 1)]
        public static void Create()
        {
            if (Selection.activeObject is null)
                return;

            NamingWindow.ReturnName += CreateScript;
            NamingWindow.Init();
        }

        private static void CreateScript(string name)
        {
            // Path of your current folder
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }

            if (string.IsNullOrEmpty(path))
                path = "Assets/";

            string newString;
            using (var sr = File.OpenText(c_PathToTemplate))
            {
                var text = sr.ReadToEnd();
                text = text.Replace("Template", name);
                text = text.Replace("//", "");
                newString = text;
            }

            // Create script file in current folder
            using (var outfile = new StreamWriter(File.Create(path + "/" + name + ".cs")))
            {
                outfile.Write(newString);
            }

            // If using NamingWindow for other uses, unsubscribe
            NamingWindow.ReturnName -= CreateScript;
            IsCodeCompiling.Init(name);
            AssetDatabase.Refresh();
            EditorUtility.RequestScriptReload(); // Assembly reload
        }

        public static void CreateScriptableObj(string name)
        {
            var objInstance = ScriptableObject.CreateInstance(name);
            AssetDatabase.CreateAsset(objInstance, c_FolderPath + name + ".asset");
        }
    }
}
#endif