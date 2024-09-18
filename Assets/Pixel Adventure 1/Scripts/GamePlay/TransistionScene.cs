using System;
using DG.Tweening;
using GameTool;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pixel_Adventure_1.Scripts
{
    public class TransistionScene : SingletonMonoBehavior<TransistionScene>
    {
        #region Member Fields

        [SerializeField] private Image m_Image;
        [SerializeField] private float m_Duration;
        private bool isLoading = false;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void EndScene(Action _action)
        {
            if (isLoading)
            {
                return;
            }
            isLoading = true;
            m_Image.DOFade(1, m_Duration).OnComplete(() =>
            {
                _action.Invoke();
                isLoading = false;
            });
        }

        public void StartScene(Action _action)
        {
            m_Image.color = Color.black;
            m_Image.DOFade(0, m_Duration);
            _action.Invoke();
        }
        
        public void StartScene()
        {
            m_Image.color = Color.black;
            m_Image.DOFade(0, m_Duration);
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            this.PostEvent(eEventType.SceneLoaded);
            StartScene();
        }
    }
}