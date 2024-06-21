using UnityEngine;

namespace RPGGame
{
    // 플레이어의 애니메이션 전환 처리를 담당하는 스크립트.
    public class PlayerAnimationController : MonoBehaviour
    {
        // 필드.
        [SerializeField] private Animator refAnimator;

        private void Awake()
        {
            if (refAnimator == null)
            {
                //refAnimator = GetComponent<Animator>();
                refAnimator = transform.parent.GetComponentInChildren<Animator>();
            }

            // 이벤트 등록.
            var manager = FindFirstObjectByType<PlayerStateManager>();
            if (manager != null)
            {
                manager.SubscribeOnPlayerStateChanged(OnPlayerStateChanged);
            }
        }

        // 메소드.
        // 애니메이션 전환을 처리하는 기능.
        private void OnPlayerStateChanged(PlayerStateManager.State newState)
        {
            refAnimator.SetInteger("State", (int)newState);
        }
    }
}
