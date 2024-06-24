using UnityEngine;

namespace RPGGame
{
    // 몬스터 슬라임의 쫓아가기 상태 스크립트.
    // 할일: 플레이어를 쫓아가기.
    // 플레이어와의 거리가 공격 가능 거리 안으로 줄어들면
    // 공격 상태로 전환.
    public class SlimeChase : MonsterState
    {
        // 공격 가능 거리 (단위: 미터).
        [SerializeField] private float attackRange = 1.5f;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 예외 처리.
            // 플레이어가 시야에서 벗어나면 정지 상태로 전환.
            if (Utils.IsInSight(refTransform, manager.PlayerTransform, 60f, 3f) == false)
            {
                manager.SetState(MonsterStateManager.State.Idle);
            }
        }

        protected override void Update()
        {
            base.Update();

            // 플레이어가 시야에서 벗어나면 다시 정지로 전환.
            if (Utils.IsInSight(refTransform, manager.PlayerTransform, 60f, 3f) == false)
            {
                manager.SetState(MonsterStateManager.State.Idle);
            }

            // 쫓아가기 (이동/회전).
            // 회전
            Utils.RotateToward(refTransform,manager.PlayerTransform.position, 360f);

            // 이동.
            if (Utils.MoveToward(
                refTransform,
                characterController,
                manager.PlayerTransform.position,
                3f) <= attackRange )
            {
                manager.SetState(MonsterStateManager.State.Attack);
            }
        }
    }
}
