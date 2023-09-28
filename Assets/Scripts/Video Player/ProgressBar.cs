using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using UnityEngine.UI;

namespace dagher.syloetest
{
    public class ProgressBar : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        [SerializeField] private VideoPlayer m_Video;
        private RectTransform m_ProgressBarRect;
        private Image m_ProgressBarImage;

        private void Start()
        {
            m_ProgressBarRect = GetComponent<RectTransform>();
            m_ProgressBarImage = GetComponent<Image>();
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            TrySkip(eventData);
        }

        public void OnDrag(PointerEventData eventData) 
        {
            TrySkip(eventData);
        }

        private void TrySkip(PointerEventData eventData) 
        {
            Vector2 localPoint;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_ProgressBarRect, eventData.position, null, out localPoint))
            {
                Debug.Log(localPoint.x);
                float percent = Mathf.InverseLerp(m_ProgressBarRect.rect.xMin, m_ProgressBarRect.rect.xMax, localPoint.x);
                SkipTo(percent);
            }
        }

        private void SkipTo(float pPercent) 
        {
            Debug.Log(pPercent);
            var frame = m_Video.frameCount * pPercent;
            m_Video.frame = (long)frame;
        }

        private void Update()
        {
            if (m_Video.frameCount > 0) 
            {
                m_ProgressBarImage.fillAmount = (float)m_Video.frame/ (float)m_Video.frameCount;
            }
        }
    }
}
