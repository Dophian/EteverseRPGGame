using UnityEngine;

namespace RPGGame
{
    // 몬스터 슬라임의 죽음 스테이트 스크립트.
    // 할일1: 충돌(클릭)이 안되도록 콜라이더 끄기.
    // 할일2: 일정 시간 뒤에 게임 오브젝트 제거.
    public class MonsterDead : MonsterState
    {
        // 게임 오브젝트 삭제 전까지 대기하는 시간 값(단위: 초).
        [SerializeField] private float deadWaitTime = 3f;

        // 죽을 때 비활성화 시킬 콜라이더.
        [SerializeField] private Collider refCollider;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 할일1 처리.
            if (refCollider != null)
            {
                refCollider.enabled = false;
            }

            // 할일2 처리.
            Destroy(gameObject, deadWaitTime);
        }
    }
}