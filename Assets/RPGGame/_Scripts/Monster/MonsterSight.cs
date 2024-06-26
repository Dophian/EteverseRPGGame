using UnityEngine;

namespace RPGGame
{
    // 치환.
    using State = MonsterStateManager.State;

    // 몬스터의 시야를 담당하는 스크립트.
    // 할일: 몬스터의 상태에 따라 시야를 판정하고 상태를 변경.
    // 데이터1: 몬스터의 상태.
    // 데이터2: 시야? - 각도, 거리.
    public class MonsterSight : MonoBehaviour
    {
        // 몬스터의 상태 값을 읽어올 때 사용할 관리자 참조 변수.
        [SerializeField] private MonsterStateManager manager;

        // 시야에 필요한 변수.
        //[SerializeField] private float sightAngle = 60f;        // 각도.
        //[SerializeField] private float sightRange = 5f;         // 거리.

        // 내 트랜스폼 - 성능 개선을 위해 저장해서 사용.
        private Transform refTransform;

        private void Awake()
        {
            refTransform = transform;

            // 관리자 검색.
            manager = GetComponentInParent<MonsterStateManager>();
        }

        private void Update()
        {
            // 시야 판정.
            // 플레이어가 죽었으면 무시.
            if (manager.AttackTarget != null && manager.AttackTarget.IsPlayerDead)
            {
                return;
            }

            // 몬스터가 죽어도 종료.
            if(manager.IsMonsterDead)
            {
                // 게임 오브젝트/컴포넌트 끄기
                return ;
            }

            // 정지/정찰일 때.
            // 시야에 들어오면 추격으로 전환.
            if (manager.CurrentState == State.Idle ||
                manager.CurrentState == State.Patrol)
            {
                // 시야에 들어왔는지 확인.
                if (Utils.IsInSight(refTransform,manager.PlayerTransform,manager.Data.sightAngle,manager.Data.sightRange))
                {
                    manager.SetState(State.Chase);
                }
            }

            // 추격/공격일 때.
            // 시야에 벗어나면 정지로 전환.
            if (manager.CurrentState == State.Chase ||
                manager.CurrentState == State.Attack)
            {
                // 시야에 들어왔는지 확인.
                if (!Utils.IsInSight(refTransform, manager.PlayerTransform, manager.Data.sightAngle, manager.Data.sightRange))
                {
                    manager.SetState(State.Idle);
                }
            }
        }
    }
}