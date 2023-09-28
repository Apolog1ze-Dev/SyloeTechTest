using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using TMPro;
using System;

namespace dagher.syloetest
{
    public class FeatureVideoPlayer : MonoBehaviour, IPointerDownHandler
    {

        private bool m_ShowControlsToggle = true;
        [SerializeField] private CanvasGroup m_VideoControlsGroup;
        [SerializeField] private float m_ControlsFadeOutTime = 1f;
        [SerializeField] private float m_ControlsInactivityTime = 1f;
        private VideoPlayer m_Video;

        [SerializeField] private GameObject m_PlayButton;
        [SerializeField] private GameObject m_PauseButton;

        [SerializeField] private TMP_Text m_VideoTime;

        private void Start()
        {
            m_Video = GetComponent<VideoPlayer>();
            StartCoroutine(InactivityFadeOut());
        }

        public void OnPointerDown(PointerEventData pointerData) 
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), pointerData.position, null, out localPoint)) 
            {
                ToggleShowControls();
            }
        }

        /// <summary>
        /// Toggles between Show/Hide states for the Video Player Controls Group
        /// If the group is to be hidden, it will fade away
        /// </summary>
        private void ToggleShowControls() 
        {
            m_ShowControlsToggle = !m_ShowControlsToggle;
            if (m_ShowControlsToggle)
            {
                m_VideoControlsGroup.alpha = 1;
                m_VideoControlsGroup.gameObject.SetActive(true);
                StartCoroutine(InactivityFadeOut());
            }
            else 
            {
                StartCoroutine(FadeOutControls());
            }
        }

        bool inactivityCheck = false;
        IEnumerator InactivityFadeOut()
        {
            if (inactivityCheck)
            {
                yield break;
            }

            inactivityCheck = true;

            while (inactivityCheck)
            {
                float time = 0f;
                while (time < 3)
                {
                    time += Time.deltaTime;
                    yield return null;
                }

                StartCoroutine(FadeOutControls());
                inactivityCheck = false;
                yield return null;
            }
        }

        bool fadeCheck = false;
        /// <summary>
        /// Does Fade Animation for the Video Controls
        /// </summary>
        /// <returns></returns>
        IEnumerator FadeOutControls() 
        {
            if (fadeCheck) 
            {
                yield break;
            }

            fadeCheck = true;

            while (fadeCheck) 
            {
                float time = 0f;
                while (time < m_ControlsFadeOutTime)
                {
                    time += Time.deltaTime;

                    m_VideoControlsGroup.alpha -= Time.deltaTime / m_ControlsFadeOutTime;//do lerp
                    yield return null;
                }

                m_VideoControlsGroup.gameObject.SetActive(false);
                
                m_ShowControlsToggle = false;
                fadeCheck = false;
                yield return null;
            }
            
        }

        /// <summary>
        /// Plays the video
        /// </summary>
        public void PlayVideo() 
        {
            StartCoroutine(FadeOutControls());
            m_Video.Play();
            m_PlayButton.SetActive(false);
            m_PauseButton.SetActive(true);
        }

        /// <summary>
        /// Pauses the video
        /// </summary>
        public void PauseVideo() 
        {
            m_Video.Pause();
            m_PlayButton.SetActive(true);
            m_PauseButton.SetActive(false);
        }

        /// <summary>
        /// Sets the video to fullscreen mode
        /// </summary>
        public void Fullscreen() 
        {
            //Rescale Render Texture and force phone flip to landscape
        }

        public void PlayTogether() 
        {
            Debug.Log("PLAYING TOGETHER");
        }

        private void Update()
        {
            //Updates the timer for the video //could optimize by only running code when video is playing with a bool check at the beginning
            var ts = TimeSpan.FromSeconds(m_Video.time);
            m_VideoTime.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }
    }
}
