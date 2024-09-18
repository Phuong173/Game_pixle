using GameTool;
using UnityEngine;

namespace Pixel_Adventure_1.Scripts
{
    public class FruitCollected : MonoBehaviour
    {
        #region Member Fields

        [SerializeField] private float m_TimeDisable = 0.2f;

        #endregion


        private void OnEnable()
        {
            Invoke(nameof(Disable), m_TimeDisable);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}