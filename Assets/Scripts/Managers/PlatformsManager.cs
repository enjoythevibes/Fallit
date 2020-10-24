using enjoythevibes.Platforms;
using UnityEngine;

namespace enjoythevibes.Managers
{
    public class PlatformsManager : MonoBehaviour
    {
        private float spawnTimer;

        private void Awake()
        {
            EventsManager.AddListener(Events.PlayGame, OnActivatePlatforms);
            EventsManager.AddListener(Events.GameOver, OnDeacttivePlatforms);
            EventsManager.AddListener(Events.RestartGame, OnActivatePlatforms);
            enabled = false;
        }

        private void OnActivatePlatforms()
        {
            enabled = true;
            SpawnPlatform(0);
        }

        private void OnDeacttivePlatforms()
        {
            enabled = false;
            spawnTimer = 0f;
        }

        private void Update()
        {
            var timeMultiplier = Functions.Lerp(1f, EngineSettings.Game.MaxTimeScale, 1f, 1.7f, GameManager.TimeScale);
            if (spawnTimer > EngineSettings.Platforms.SpawnEachTime)
            {
                spawnTimer -= EngineSettings.Platforms.SpawnEachTime;
                var randomXPosition = Random.Range(-EngineSettings.Platforms.MinMaxXAxisFrame + transform.localScale.x, EngineSettings.Platforms.MinMaxXAxisFrame - transform.localScale.x);
                SpawnPlatform(randomXPosition);
            }
            spawnTimer += Time.deltaTime * timeMultiplier;
        }

        private void SpawnPlatform(float xPosition)
        {
            var platformGameObject = PoolsManager.GetGameObjectsPool(EngineSettings.Platforms.PlatformsPoolTagName).Take();
            platformGameObject.transform.position = new Vector3(xPosition, EngineSettings.Platforms.MinimumHeight, 0f);
            platformGameObject.GetComponent<Platform>().Activate();
        }
    }
}