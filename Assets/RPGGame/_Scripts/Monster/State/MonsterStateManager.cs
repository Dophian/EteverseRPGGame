using UnityEngine;
using UnityEngine.Events;

namespace RPGGame
{
    // 몬스터의 상태(스테이트)를 관리하는 스크립트.
    public class MonsterStateManager : MonoBehaviour
    {
       // 상태 열거형.
        public enum State
        {
            None = -1,
            Idle,
            Patrol,
            Chase,
            Attack,
            Length
        }

        // 몬스터 캐릭터의 현재 상태를 나타내는 변수.
        [SerializeField] private State state = State.None;

        // MonsterState 컴포넌트의 배열.
        [SerializeField] private MonsterState[] states;

        // 몬스터의 이름 값. (슬라임, 멧돼지, 고블린 등의 몬스터 이름).
        [SerializeField] private string monsterName;

        // 적 게임 오브젝트가 움직일 때 활용할 컴포넌트.
        [SerializeField] private CharacterController characterController;

        // 상태 변경 이벤트.
        [SerializeField] private UnityEvent<State> OnMonsterStateChanged;

        // 플레이어의 트랜스폼 컴포넌트.
        public Transform PlayerTransform { get; private set; }

        // 상대 변경 이벤트에 등록(구독)하는 공개 메소드.
        public void SubscribeOnMonsterStateChanged(UnityAction<State> action)
        {
            OnMonsterStateChanged?.AddListener(action);
        }

        // 초기화.
        private void Awake()
        {
            // 캐릭터 컨트롤러 설정.
            if(characterController == null)
            {
                characterController = GetComponent<CharacterController>();  
            }

            // 플레이어 트랜스폼 설정.
            PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            // 상태 컴포넌트 배열 초기화.
            states = new MonsterState[(int)State.Length];

            for(int ix = 0; ix < (int)State.Length; ++ix)
            {
                // 컴포넌트 이름 설정.
                string componentName = $"{monsterName}{(State)ix}";
                //states[ix] = (MonsterState)GetComponent(((State)ix).ToString());
                states[ix] = (MonsterState)GetComponent(componentName);

                // 각 스테이트 객체에 전파해야하는 값을 전달.
                states[ix].SetCharacterController(characterController);
                states[ix].SetStateManager(this);
            }
        }

        private void OnEnable()
        {
            SetState(State.Idle);
        }

        // 상태 변경 메시지(공개 메소드).
        public void SetState(State newState)
        {
            // 예외처리.
            if (state == newState)
            {
                return;
            }

            // 현재 상태 비활성화.
            if (state != State.None)
            {
                states[(int)state].enabled = false;
            }

            // 새로운 상태 활성화.
            if (newState != State.None)
            {
                states[(int)newState].enabled = true;
            }

            // 상태 값 업데이트.
            state = newState;

            // 상태 변경 이벤트 발행.
            OnMonsterStateChanged?.Invoke(state);
        }

    }
}