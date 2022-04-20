using UnityEngine;

namespace Code
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance)
                    return _instance;

                _instance = FindObjectOfType<T>();

                if (_instance) return _instance;

                _instance = (new GameObject
                {
                    name = nameof(T),
                    hideFlags = HideFlags.HideAndDontSave,
                }).AddComponent<T>();

                print("Singleton.cs, Created new Singleton of type: " + typeof(T) +
                    ", Script calling to Singleton instance during OnDestroy most likely.");
                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}