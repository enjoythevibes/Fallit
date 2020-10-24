using enjoythevibes.Managers;
using UnityEngine;

namespace enjoythevibes.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private UIField gameOverTimerField = default;
        [SerializeField]
        private UIField recordField = default;

        private void Awake()
        {
            EventsManager.AddListener(Events.GameOver, OnEnableUI);
            EventsManager.AddListener(Events.RestartGame, OnDisableUI);    
            gameOverTimerField.SetUp();
            recordField.SetUp();
            OnDisableUI();
        }

        private void OnEnableUI()
        {
            gameObject.SetActive(true);
            SetTimerField();
            SetRecordField();
        }

        private void OnDisableUI()
        {
            gameObject.SetActive(false);
        }

        private void SetRecordField()
        {
            var timerText = GameManager.RecordGameTimer.ToString("0.000");
            recordField.SetText(timerText);
        }

        private void SetTimerField()
        {
            var timerText = GameManager.CurrentGameTimer.ToString("0.000");
            gameOverTimerField.SetText(timerText);
        }

        public void OnTryAgainButton()
        {
            EventsManager.CallEvent(Events.RestartGame);
        }
    }
}