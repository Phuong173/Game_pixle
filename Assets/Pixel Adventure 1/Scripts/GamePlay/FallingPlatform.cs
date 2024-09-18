using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Pixel_Adventure_1.Scripts
{
    public class FallingPlatform : MonoBehaviour
    {
        [Serializable]
        private class Node
        {
            public Vector3 m_Pos;
            public float m_Duration = 0.5f;
        }

        #region Member Fields

        [SerializeField] private List<Node> m_Nodes;
        [SerializeField] private int m_NodeIndex = 0;
        [SerializeField] private float m_StartDuration = 0.5f;

        #endregion


        private void Start()
        {
            if (m_Nodes.Count == 0)
            {
                Debug.LogError("m_Nodes.Count == 0");
                return;
            }

            transform.DOMove(m_Nodes[m_NodeIndex].m_Pos, m_StartDuration).OnComplete(Loop);
        }

        private void Loop()
        {
            if (m_Nodes.Count == 1)
            {
                return;
            }

            if (m_NodeIndex == m_Nodes.Count - 1)
            {
                m_NodeIndex = 0;
            }
            else
            {
                m_NodeIndex++;
            }

            transform.DOMove(m_Nodes[m_NodeIndex].m_Pos, m_Nodes[m_NodeIndex].m_Duration).OnComplete(Loop);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.activeSelf)
            {
                other.transform.parent = transform;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.activeSelf)
            {
                other.transform.parent = null;
            }
        }
    }
}