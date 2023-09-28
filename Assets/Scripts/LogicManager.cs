using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dagher.syloetest
{
    public static class DataContainer 
    {
        private static int m_VideoViewCount = default;
        public static int VideoViewCount
        {
            get { return m_VideoViewCount; }
            set { m_VideoViewCount = value; }
        }

        private static string m_EpisodeName = default;
        public static string EpisodeName
        {
            get { return m_EpisodeName; }
            set { m_EpisodeName = value; }
        }
    }

    public class LogicManager : MonoBehaviour
    {
        private void Start()
        {
            ViewManager.CalledSave += UpdateStoredValues;
            ViewManager.SwipedToNewPage += UpdateViewPage;
        }

        /// <summary>
        /// Updates the view count and name of the episode in the data container
        /// </summary>
        ///<param name = "pString" >The new episode name</ param >
        ///<param name = "pViewCount" >The new view count</ param >
        private void UpdateStoredValues(string pString, int pViewCount)
        {
            DataContainer.EpisodeName = pString;
            DataContainer.VideoViewCount = pViewCount;
        }

        /// <summary>
        /// Updates which page is being viewed
        /// </summary>
        private void UpdateViewPage(int pPageToShow) 
        {
            if (pPageToShow == -1)
            {
                ViewManager.Instance.ShowEditor();
            } else if (pPageToShow == 1) 
            {
                ViewManager.Instance.ShowClient();
            }
        }

        private void OnDestroy()
        {
            ViewManager.CalledSave -= UpdateStoredValues;
            ViewManager.SwipedToNewPage -= UpdateViewPage;
        }
    }
}
