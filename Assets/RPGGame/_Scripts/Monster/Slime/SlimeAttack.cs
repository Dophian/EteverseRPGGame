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

            // 플레이어와의 거리가 공격 가능 범위를 벗어나면 다시 쫒아가기.

            if (Vector3.Distance(
                refTransform.position,
                manager.PlayerTransform.position) > 1.5f)
            {
                manager.SetState(MonsterStateManager.State.Chase);
            }
        }
    }
}