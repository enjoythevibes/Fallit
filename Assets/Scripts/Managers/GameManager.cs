using UnityEngine;

namespace enjoythevibes.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static float TimeScale { set; get; } = 1f;
        public static float RecordGameTimer { private set; get; }
        public static float CurrentGameTimer { private set; get; }

        private void Awake() 
        {
            EventsManager.AddListener(Events.GameInitialization, OnLoadPlayerData);
            EventsManager.AddListener(Events.PlayGame, OnStartTimer);
            EventsManager.AddListener(Events.RestartGame, OnStartTimer);
            EventsManager.AddListener(Events.GameOver, OnStopTimer);      
            EventsManager.AddListener(Events.GameOver, OnSavePlayerData);            
            EventsManager.AddListener(Events.LoadPlayerData, OnLoadPlayerData);
            EventsManager.AddListener(Events.SavePlayerData, OnSavePlayerData);
        }

        private void Start() 
        {
            Application.targetFrameRate = 65;
            EventsManager.CallEvent(Events.GameInitialization);    
            enabled = false;
        }

        private void OnLoadPlayerData()
        {
            Data.DataSaver.LoadData();
            RecordGameTimer = Data.DataSaver.playerData.timeRecord;
        }

        private void OnSavePlayerData()
        {
            Data.DataSaver.playerData.timeRecord = RecordGameTimer;
            Data.DataSaver.SaveData();            
        }

        private void OnStartTimer()
        {
            enabled = true;
            CurrentGameTimer = 0f;
            TimeScale = 1f;
        }

        private void OnStopTimer()
        {
            enabled = false;
            if (CurrentGameTimer > RecordGameTimer)
            {
                RecordGameTimer = CurrentGameTimer;
            }
        }
        
        private void Update()
        {
            CurrentGameTimer += Time.deltaTime;
            if (TimeScale < EngineSettings.Game.MaxTimeScale)
            {
                TimeScale += Time.deltaTime * EngineSettings.Game.TimeScaleMultiplier;
            }
            else
            {
                TimeScale = EngineSettings.Game.MaxTimeScale;
            }
        }
    }
}