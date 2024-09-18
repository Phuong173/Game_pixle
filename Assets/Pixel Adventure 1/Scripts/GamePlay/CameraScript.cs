using System;
using UnityEngine;

namespace Pixel_Adventure_1.Scripts
{
    public class CameraScript : MonoBehaviour
    {
        [SerializeField] private Transform m_Character;
        [SerializeField] private float m_LeftBound;
        [SerializeField] private float m_RightBound;
        [SerializeField] private float m_MinMagnituzeSmoothDamp = 1f;
        [SerializeField] private float m_SmoothTime = 0.25f;

        [SerializeField] private Vector3 m_PosLock;
        private Vector3 m_VeloSmoothDamp;

        private void Update()
        {
            if (!m_Character)
            {
                return;
            }

            Vector3 _vector3 = m_Character.position;
            _vector3.y = 0;
            _vector3.z = -10;
            
            if (m_Character.position.x < m_LeftBound)
            {
                _vector3.x = m_LeftBound - m_MinMagnituzeSmoothDamp;
            }

            if (m_Character.position.x > m_RightBound)
            {
                _vector3.x = m_RightBound + m_MinMagnituzeSmoothDamp;
            }

            if (Mathf.Abs(transform.position.x - _vector3.x) >= m_MinMagnituzeSmoothDamp)
            {
                transform.position = Vector3.SmoothDamp(transform.position, _vector3, ref m_VeloSmoothDamp, m_SmoothTime);
            }
        }
    }
}