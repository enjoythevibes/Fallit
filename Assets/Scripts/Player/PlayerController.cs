using enjoythevibes.Managers;
using UnityEngine;

namespace enjoythevibes.Player
{
    [RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerMovement playerMovement;
        private PlayerState playerState;
        
        private bool isGround;
        private ContactFilter2D contactFilter2D;
        private RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
        private Rigidbody2D rb;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
            rb = GetComponent<Rigidbody2D>();
            EventsManager.AddListener(Events.PlayGame, OnSetMoveState);
            EventsManager.AddListener(Events.GameOver, OnSetGameOverState);
            EventsManager.AddListener(Events.RestartGame, OnResetPlayer);
            EventsManager.AddListener(Events.RestartGame, OnSetMoveState);            
            contactFilter2D.layerMask = (1 << 8);
            contactFilter2D.useLayerMask = true;
        }

        private void OnSetMoveState()
        {
            playerState = PlayerState.Move;    
        }

        private void OnSetGameOverState()
        {
            playerState = PlayerState.GameOver;
        }

        private void OnSetIdleState()
        {
            playerState = PlayerState.Idle;
        }

        private void OnResetPlayer()
        {
            transform.rotation = Quaternion.identity;
            transform.position = EngineSettings.Player.DefaultPlayerPosition;
            isGround = false;
        }

        private void Update()
        {
            switch (playerState)
            {
                case PlayerState.Move:
                    PlayerMoveState();
                    Collision();
                    break;
                case PlayerState.GameOver:
                    PlayerGameOverState();
                    break;
            }
        }

        private void Collision()
        {
            if (transform.position.y < EngineSettings.Game.CollisionBelow + transform.localScale.y) return;
            var collisions = Physics2D.BoxCast(transform.position, transform.localScale * 1.9f, transform.eulerAngles.z, Vector2.down, contactFilter2D, raycastHit2D, 0.05f);
            if (collisions > 0)
            {
                if (!isGround)
                {
                    EventsManager.CallEvent(Events.HitPlatform);
                }
                isGround = true;
            }
            else
            {
                isGround = false;
            }
        }

        private void PlayerMoveState()
        {
            var topCollision = transform.position.y > EngineSettings.Game.CollisionHeight - transform.localScale.y;
            var bottomCollision = transform.position.y < EngineSettings.Game.CollisionBelow + transform.localScale.y;
            if (topCollision || bottomCollision)
            {
                EventsManager.CallEvent(Events.GameOver);
            }
            playerMovement.Move(playerInput.xAxis);
        }

        private void PlayerGameOverState()
        {
            if (playerMovement.Fall())
                OnSetIdleState();
        }
    }
}