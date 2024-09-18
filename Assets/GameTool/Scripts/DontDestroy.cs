using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameTool
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
