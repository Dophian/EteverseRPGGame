using UnityEngine;

namespace RPGGame
{
    // 슬라임 몬스터의 Idle(대기) 상태 스크립트.
    // 할일: 지정한 시간만큼 기다렸다가 정찰(Patrol) 스테이트로 전환.
    public class SlimeIdle : MonsterState
    {
        // 필드.

        // 대기할 시간 (단위: 초).
        //[SerializeField] private float waitTime = 3f;

        // 경과시간을 계산하기 위한 변수.
        private float elapsedTime = 0f;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 초기 설정.
            elapsedTime = 0f;
        }

        protected override void Update()
        {
            base.Update();

            // 타이머 업데이트.
            elapsedTime += Time.deltaTime;
            if (elapsedTime > manager.Data.patrolWaitTime)
            {
                manager.SetState(MonsterStateManager.State.Patrol);
                elapsedTime = 0f;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            elapsedTime = 0f;
        }
    }
}