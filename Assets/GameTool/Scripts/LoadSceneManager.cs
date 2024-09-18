using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace GameTool
{
    public class LoadSceneManager : SingletonMonoBehavior<LoadSceneManager>
    {
        public const string nameSceneSpl = "SplScene";
        public const string nameSceneHome = "HomeScene";
        public const string nameSceneGame = "GameScene";

        [Tooltip("Time between starting and ending scale animation")] [SerializeField]
        private float timeLoadScene = 1;

        [Tooltip("Delay after animated to run load scene method")] [SerializeField]
        private float timeDelay = 0.5f;

        [Tooltip("Time start show logo from starting scale")] [SerializeField]
        private float timeShowLogo = 0.5f;

        [Tooltip("Max - Start scale of background")] [SerializeField]
        private float maxScale = 10;

        [Tooltip("Min - End scale of background")] [SerializeField]
        private float minScale = 1;

        [Tooltip("Speed of animation")] [SerializeField]
        private float speed = 3;

        [SerializeField] private RectTransform transistionPanel;
        [SerializeField] private Image logo;

        #region LoadScene

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadAsyncScene(sceneName));
        }

        public void LoadSceneSpl()
        {
            StartCoroutine(LoadAsyncScene(nameSceneSpl));
        }

        public void LoadSceneHome()
        {
            StartCoroutine(LoadAsyncScene(nameSceneHome));
        }

        public void LoadSceneGame()
        {
            StartCoroutine(LoadAsyncScene(nameSceneGame));
        }

        IEnumerator LoadAsyncScene(string nameScene)
        {
            CloseScreen(speed);
            yield return new WaitForSecondsRealtime(timeLoadScene / speed + timeDelay / speed);
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nameScene);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            OpenScreen(speed);
        }

        #endregion


        public void OpenScreen(float multiplierSpeed = 3, UnityAction action = null)
        {
            StartCoroutine(HideLogo(multiplierSpeed, action));
        }

        public void CloseScreen(float multiplierSpeed = 3, UnityAction action = null)
        {
            StartCoroutine(ShowLogo(multiplierSpeed, action));
        }


        IEnumerator ShowLogo(float multiplierSpeed = 3, UnityAction action = null)
        {
            transistionPanel.DOScale(minScale, timeLoadScene / multiplierSpeed);
            yield return new WaitForSecondsRealtime(timeShowLogo / multiplierSpeed);
            logo.DOFade(1, timeLoadScene / multiplierSpeed - timeShowLogo / multiplierSpeed);
            yield return new WaitForSecondsRealtime(timeLoadScene / multiplierSpeed - timeShowLogo / multiplierSpeed);
            action?.Invoke();
        }

        IEnumerator HideLogo(float multiplierSpeed = 3, UnityAction action = null)
        {
            //transistionImg.DOFade(1, 0.2f);
            logo.DOFade(0, timeLoadScene / multiplierSpeed - timeShowLogo / multiplierSpeed).OnComplete(() =>
            {
                transistionPanel.DOScale(maxScale, timeLoadScene / multiplierSpeed);
            });
            yield return new WaitForSecondsRealtime(timeLoadScene / multiplierSpeed - timeShowLogo / multiplierSpeed);

            action?.Invoke();
        }
    }
}