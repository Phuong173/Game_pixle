using UnityEngine;

namespace GameTool
{
    public class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T I
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<T>();
                }

                return instance;
            }
        }

        private static T instance = null;

        protected virtual void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = FindObjectOfType<T>();
            }
        }

        public static bool Exists()
        {
            return instance;
        }
    }

    public class SingletonUI<T> : BaseUI where T : BaseUI
    {
        public static T I
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<T>();
                }

                return instance;
            }
        }

        private static T instance = null;

        protected virtual void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = FindObjectOfType<T>();
            }
        }

        public static bool Exists()
        {
            return instance;
        }
    }
}