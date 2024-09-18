using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace GameTool
{
    public class SplSceneManager : MonoBehaviour
    {
        [SerializeField] private float timeLoad = 3f;
        [SerializeField] private float widthFill;
        [SerializeField] private RectTransform fillRect;

        private void Awake()
        {
            StartCoroutine(nameof(Fill));
        }

        IEnumerator Fill()
        {
            fillRect.sizeDelta = new Vector2(100, fillRect.sizeDelta.y);
            while (fillRect.sizeDelta.x < widthFill)
            {
                fillRect.sizeDelta += Vector2.right * Time.deltaTime * widthFill / timeLoad;
                yield return null;
            }
            fillRect.sizeDelta = new Vector2(widthFill, fillRect.sizeDelta.y);
            LoadSceneManager.I.LoadSceneHome();
        }
    }
}