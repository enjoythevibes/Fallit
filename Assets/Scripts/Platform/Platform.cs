using enjoythevibes.Managers;
using UnityEngine;

namespace enjoythevibes.Platforms
{
    public class Platform : MonoBehaviour
    {
        private void Awake() 
        {
            EventsManager.AddListener(Events.GameOver, OnDestorPlatform);
            enabled = false;
        }

        public void Activate()
        {
            enabled = true;
        }

        private void FixedUpdate()
        {
            var nextPosition = transform.position + Vector3.up * Time.deltaTime * GameManager.TimeScale;
            GetComponent<Rigidbody2D>().MovePosition(nextPosition);
            if (transform.position.y > EngineSettings.Platforms.MaximumHeight)
            {
                OnDestorPlatform();
            }
        }

        private void OnDestorPlatform()
        {
            enabled = false;
            Managers.PoolsManager.GetGameObjectsPool(EngineSettings.Platforms.PlatformsPoolTagName).Put(gameObject);
        }
    }
}