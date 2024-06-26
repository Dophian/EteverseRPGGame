using UnityEngine;

namespace RPGGame
{
    // 몬스터 슬라임의 공격 상태 스크립트.
    // 할일: 공격 상태를 빠져나가야 하는지 판단.
    // 실제 공격 처리는 애니메이션 시스템에서 싱크에 맞춰서 진행할 것.
    public class SlimeAttack : MonsterState
    {
        protected override void Update()
        {
            base.Update();

            // 플레이어가 죽었으면 중단.
            if (manager.AttackTarget != null && manager.AttackTarget.IsPlayerDead)
            {
                manager.SetState(MonsterStateManager.State.Idle);
                return;
            }

            // 플레이어와의 거리가 공격 가능 범위를 벗어나면 다시 쫒아가기.

            if (Vector3.Distance(
                refTransform.position,
                manager.PlayerTransform.position) > manager.Data.attackRange)
            {
                manager.SetState(MonsterStateManager.State.Chase);
            }
        }
    }
}