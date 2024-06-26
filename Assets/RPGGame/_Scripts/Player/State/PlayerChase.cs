using UnityEngine;

namespace RPGGame
{
    // 플레이어가 적을 쫒아갈 때 사용할 스크립트.
    // 할일: 몬스터를 쫓아가기.
    // 데이터: 몬스터 위치(공격 마커의 위치), 이동 속도, 회전 속도, 공격 가능 거리.
    public class PlayerChase : PlayerState
    {
        protected override void Update()
        {
            base.Update();

            // 적 쫓아가기 → 이동/회전.
            Utils.RotateToward(refTransform, manager.AttackPosition, manager.Data.rotateSpeed);
            
            // 공격 가능 범위는 2M
            if (Utils.MoveToward(
                refTransform,
                characterController,
                manager.AttackPosition,
                manager.Data.moveSpeed) <= manager.Data.attackRange)
            {
                manager.SetState(PlayerStateManager.State.PlayerAttack);
            }
        }
    }
}