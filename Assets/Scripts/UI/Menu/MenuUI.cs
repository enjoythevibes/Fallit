using enjoythevibes.Managers;
using UnityEngine;

namespace enjoythevibes.UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField]
        private UIField recordField = default;

        private void Awake()
        {
            EventsManager.AddListener(Events.PlayGame, OnDisableUI);            
            EventsManager.AddListener(Events.GameInitialization, OnSetRecordText);
            recordField.SetUp();
        }

        private void OnSetRecordText()
        {
            var timerText = GameManager.RecordGameTimer.ToString("0.000");
            recordField.SetText(timerText);
        }

        public void OnPlayButton()
        {
            EventsManager.CallEvent(Events.PlayGame);    
        }

        private void OnDisableUI()
        {
            gameObject.SetActive(false);
        }
    }
}