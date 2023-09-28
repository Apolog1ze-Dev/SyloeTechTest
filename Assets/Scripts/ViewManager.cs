using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.EventSystems;

namespace dagher.syloetest
{
    public class ViewManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static ViewManager Instance;

        public static Action<string, int> CalledSave;

        public static Action<int> SwipedToNewPage;

        [SerializeField] private TMP_Text m_EpisodeNameInput;
        [SerializeField] private TMP_Text m_ViewCountInput;

        private float m_StartDragX;
        private float m_EndDragX;
        [SerializeField] private float m_MinimumDragDistance = 10f;

        [SerializeField] private GameObject m_EditorView;
        [SerializeField] private TMP_Text m_ClientEpisodeName;
        [SerializeField] private TMP_Text m_ClientViewCount;
        [SerializeField] private GameObject m_ClientView;
        [SerializeField] private Animator m_ViewAnimator;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Called Whenever the save button is pressed 
        /// </summary>
        public void Save() 
        {
            Debug.Log(m_ViewCountInput.text);
            int view;
            int.TryParse(m_ViewCountInput.text, out view);
            CalledSave.Invoke(m_EpisodeNameInput.text, view);
        }

        public void OnPointerDown(PointerEventData eventData) 
        {
            m_StartDragX = eventData.position.x;
            Debug.Log("Started Drag");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_EndDragX = eventData.position.x;
            Debug.Log("Finished Drag");
            SwitchViewPage();
        }

        private void SwitchViewPage() 
        {
            if (Mathf.Abs(m_StartDragX - m_EndDragX) > m_MinimumDragDistance) 
            {
                if (m_StartDragX > m_EndDragX)//swiped left
                {
                    SwipedLeft();
                }
                else //swiped right
                {
                    SwipedRight();
                }
            }
        }

        public void SwipedRight() 
        {
            SwipedToNewPage.Invoke(1);
        }

        public void SwipedLeft() 
        {
            SwipedToNewPage.Invoke(-1);
        }

        public void ShowEditor() 
        {
            m_ViewAnimator.Play("SwipeLeft");
        }

        public void ShowClient() 
        {
            LoadPageInfo();
            m_ViewAnimator.Play("SwipeRight");
        }

        private void LoadPageInfo() 
        {
            m_ClientEpisodeName.text = DataContainer.EpisodeName;
            m_ClientViewCount.text = DataContainer.VideoViewCount.ToString();
        }
    }
}
