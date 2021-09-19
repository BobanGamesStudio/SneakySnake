    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.EventSystems;
    using System.Collections.Generic;
    using UnityEditor;
    
    public class ShowLevelStats : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject leftStatsAllBackground;
        public GameObject leftStatsBackground;
        public int levelNum = 0;

        public void OnPointerEnter(PointerEventData eventData)
        {
            //GameObject.Find("LeftStatsBackgrund").SetActive(true);
            leftStatsAllBackground.SetActive(false);
            leftStatsBackground.GetComponent<StatsPanelData>().levelNum = levelNum;
            leftStatsBackground.GetComponent<StatsPanelData>().PanelUpdate();
            leftStatsBackground.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            leftStatsBackground.SetActive(false);
            leftStatsAllBackground.SetActive(true);
        }
    }