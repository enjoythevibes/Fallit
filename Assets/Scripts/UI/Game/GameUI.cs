using enjoythevibes.Managers;
using UnityEngine;

namespace enjoythevibes.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField]
        private UIField timerField = default;

        private void Awake()
        {
            EventsManager.AddListener(Events.PlayGame, OnEnableUI);
            EventsManager.AddListener(Events.GameOver, OnDisableUI);
            EventsManager.AddListener(Events.RestartGame, OnEnableUI);
            timerField.SetUp();
            OnDisableUI();
        }

        private void OnEnableUI()
        {
            gameObject.SetActive(true);
            SetTimerText();
        }

        private void OnDisableUI()
        {
            gameObject.SetActive(false);
        }

        private void SetTimerText()
        {
            var timerText = GameManager.CurrentGameTimer.ToString("0.000");
            timerField.SetText(timerText);
        }

        private void Update()
        {
            SetTimerText();
        }
    }
}