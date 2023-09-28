using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dagher.syloetest
{
    public class SafeArea : MonoBehaviour
    {
        private RectTransform m_RectTransform;
        private Rect m_SafeArea;
        private Vector2 m_MinAnchor;
        private Vector2 m_MaxAnchor;


        void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            m_SafeArea = Screen.safeArea;
            m_MinAnchor = m_SafeArea.position;
            m_MaxAnchor = m_MinAnchor + m_SafeArea.size;
            m_MinAnchor.x /= Screen.width;
            m_MinAnchor.y /= Screen.height;
            m_MaxAnchor.x /= Screen.width;
            m_MaxAnchor.y /= Screen.height;
            m_RectTransform.anchorMin = m_MinAnchor;
            m_RectTransform.anchorMax = m_MaxAnchor;
        }
    }
}
