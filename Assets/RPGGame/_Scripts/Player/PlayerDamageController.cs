using UnityEngine;

namespace RPGGame
{
    // 플레이어의 공격 및 대미지 처리를 담당하는 스크립트.
    // 할일1: 애니메이션에서 공격 이벤트를 발행하면, 적절하게 대응.
    // 데이터: 대미지(공격력).

    // 할일2: 대미지를 전달 받으면 대미지 처리. 죽으면 죽음 이벤트 발행.
    // 데이터: 체력(HP).
    public class PlayerDamageController : MonoBehaviour
    {
        // 필드.
        // 플레이어 상태 관리자.
        [SerializeField] private PlayerStateManager manager;

        private void Awake()
        {
            manager = GetComponentInParent<PlayerStateManager>();
        }
        // 공격 애니메이션에서 ApllyDamage 이벤트가 발생하면 실행될 메소드.

        private void ApplyDamage()
        {
            DamageManager.SendDamageToMonster(manager, manager.AttackTarget, 20f);
        }
    }
}