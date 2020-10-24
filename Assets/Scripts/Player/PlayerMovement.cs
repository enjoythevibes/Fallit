using enjoythevibes.Managers;
using UnityEngine;

namespace enjoythevibes.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            EventsManager.AddListener(Events.PlayGame, OnEnableMovement);
            EventsManager.AddListener(Events.RestartGame, OnDisableMovement);
            EventsManager.AddListener(Events.RestartGame, OnEnableMovement);
            OnDisableMovement();
        }

        private void OnEnableMovement()
        {
            rb.isKinematic = false;
        }

        private void OnDisableMovement()
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;
        }

        public void Move(float xAxis)
        {
            rb.AddForce(new Vector2(xAxis / 5f, 0f), ForceMode2D.Impulse);
        }

        public bool Fall()
        {
            if (transform.position.y < -10f)
            {
                OnDisableMovement();
                return true;
            }
            return false;
        }
    }
}