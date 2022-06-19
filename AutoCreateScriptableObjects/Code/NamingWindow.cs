#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Code
{
    public class NamingWindow : EditorWindow
    {
        public static event Action<string> ReturnName;
        private string m_Name;

        public static void Init()
        {
            var window = GetWindowWithRect(typeof(NamingWindow), new Rect(0, 0, 400, 100));
            window.titleContent = new GUIContent("Naming Window");
            window.Show();
        }

        private void OnGUI()
        {
            m_Name = EditorGUILayout.TextField("Name: ", m_Name);

            if (GUILayout.Button("Set Name"))
            {
                if (m_Name != string.Empty)
                {
                    m_Name = m_Name.Replace(' ', '_');
                    ReturnName?.Invoke(m_Name);
                    Close();
                }
                else
                {
                    Debug.LogError("NamingWindow, name can not be empty!");
                }
            }
        }
    }
}
#endif