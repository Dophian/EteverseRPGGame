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
        //[SerializeField] private float attackRange = 1.5f;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();

            // 플레이어가 죽었으면 중단.
            if (manager.AttackTarget != null && manager.AttackTarget.IsPlayerDead)
            {
                manager.SetState(MonsterStateManager.State.Idle);
                return;
            }

            // 쫓아가기 (이동/회전).
            // 회전
            Utils.RotateToward(refTransform,manager.PlayerTransform.position, manager.Data.rotateSpeed);

            // 이동.
            if (Utils.MoveToward(
                refTransform,
                characterController,
                manager.PlayerTransform.position,
                manager.Data.chaseSpeed) <= manager.Data.attackRange)
            {
                manager.SetState(MonsterStateManager.State.Attack);
            }
        }
    }
}
