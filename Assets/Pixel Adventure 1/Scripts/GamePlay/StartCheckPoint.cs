using GameTool;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pixel_Adventure_1.Scripts
{
    public class StartCheckPoint : MonoBehaviour
    {
        #region Member Fields

        private Animator m_Animator;
        private static readonly int TTrigged = Animator.StringToHash("t_Trigged");

        #endregion

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Đung đưa
                m_Animator.SetTrigger(TTrigged);

                if (!GameManager.Exists())
                {
                    // Sang scene game
                    TransistionScene.I.EndScene(() => { SceneManager.LoadScene("Level" + GameData.I.CurrentLevel); });
                    Debug.Log("man" + GameData.I.CurrentLevel);

                }
                else
                {
                    CanvasManager.I.Push(GlobalUIInfo.WinPopup);
                }
                
                AudioManager.I.Shot("NextLevel");
            }
        }
    }
}