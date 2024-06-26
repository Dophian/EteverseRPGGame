using UnityEngine;

namespace RPGGame
{
    // 플레이어가 공격 상태일 때 사용할 스크립트.
    // 할일1: 계속해서 몬스터를 바라보도록 회전 처리.
    // 할일2: 거리를 확인해서 공격 가능 거리를 벗어나면 다시 쫓아가기.
    public class PlayerAttack : PlayerState
    {
        protected override void Update()
        {
            base.Update();

            // 공격하던 몬스터가 죽으면 그만 공격.
            var target
                = manager.AttackTarget.GetComponent<MonsterStateManager>();
            
            // 타겟이 null이 아니고, 몬스터가 죽으면,
            if (target && target.IsMonsterDead)
            {
                // 공격마커 끄고.
                manager.SetAttackMarkerActive(false);

                // 상태는 정지로 전환.
                manager.SetState(PlayerStateManager.State.PlayerIdle);
            }

            // 회전 (한 번에 회전).
            Utils.RotateToBurst(refTransform, manager.AttackPosition);

            // 거리확인 및 쫓아가기.
            if (Vector3.Distance(
                refTransform.position,
                manager.AttackPosition) > manager.Data.attackRange)
            {
                manager.SetState(PlayerStateManager.State.PlayerChase);
            }
        }
    }
}