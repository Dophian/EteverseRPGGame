using UnityEngine;

namespace RPGGame
{
    // 플레이어의 스킬 상태 스크립트.
    // 할일: 스킬이 재생되는 시간동안 상태를 유지한 뒤에
    // 시간이 모두 지나면 다시 정지 상태로 전환.
    public class PlayerSkill : PlayerState
    {
        // 경과 시간 계산용 변수.
        private float elapsedTime = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 경과 시간 초기화(초시계 세팅).
            elapsedTime = 0f;

            // 예외처리.
            manager.SetAttackMarkerActive(false);
            manager.SetMoveMarkerActive(false);
        }

        protected override void Update()
        {
            base.Update();

            elapsedTime += Time.deltaTime;
            if (elapsedTime > manager.Data.skillCoolTime)
            {
                manager.SetState(PlayerStateManager.State.PlayerIdle);
                elapsedTime = 0f;
            }
        }
    }
}