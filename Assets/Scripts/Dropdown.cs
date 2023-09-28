using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace dagher.syloetest
{
    public class Dropdown : MonoBehaviour, IPointerDownHandler
    {
        private RectTransform m_TitleRect;
        [SerializeField] private GameObject m_AdditionalInfo;
        private bool m_AdditionalInfoToggle = false;

        void Start()
        {
            m_TitleRect = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            Vector2 localPoint;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_TitleRect, eventData.position, null, out localPoint)) 
            {
                ToggleAdditionalInfo();
            }
        }

        private void ToggleAdditionalInfo() 
        {
            m_AdditionalInfoToggle = !m_AdditionalInfoToggle;

            m_AdditionalInfo.SetActive(m_AdditionalInfoToggle);
        }
    }
}
