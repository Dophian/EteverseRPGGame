using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RPGGame
{
    // 플레이어의 스테이트 관리자.
    public class PlayerStateManager : MonoBehaviour
    {
        // 스테이트 열거형.
        public enum State
        {
            None = -1,
            PlayerIdle,
            PlayerMove,
            PlayerChase,
            PlayerAttack,
            PlayerDead,
            PlayerSkill,
            Length
        }

        // 플레이어의 스테이트를 나타내는 변수.
        [SerializeField] private State state = State.None;

        // 플레이어의 현재 상태를 전달하는 Getter.
        public State CurrentState { get { return state; } }

        // 플레이어 스테이트의 배열.
        [SerializeField] private PlayerState[] states;

        // 스테이가 변경될 떄 발행되는 이벤트.
        [SerializeField] private UnityEvent<State> OnPlayerStateChanged;

        // 대미지를 받을 때 발행하는 이벤트.
        [SerializeField] private UnityEvent<float> OnPlayerDamaged;

        // 캐릭터 컨트롤러 컴포넌트.
        [SerializeField] private CharacterController characterController;

        // 이동 목표 지점을 표시해주는 이동 마커.
        [SerializeField] private GameObject moveMarker;

        // 적 캐릭터를 공격할 때 표시할 마커.
        [SerializeField] private GameObject attackMarker;

        // 레이어 값 - Ray와 충돌하는 LayerMask 생성에 사용.
        //[SerializeField] private string[] layerNames;
        [SerializeField] private string blockLayerName = "Block";
        [SerializeField] private string movableLayerName = "Movable";
        [SerializeField] private string monsterLayerName = "Monster";
        
        // 플레이어 데이터 Scriptable Object(파일) 참조 변수.
        //[SerializeField] private PlayerData data;
        public PlayerData Data 
        {
            get 
            {
                return DataManager.GetPlayerData();
            } 
        }

        // 레이어 마스크.
        private int layerMask;

        // 메인 카메라.
        private Camera mainCamera;

        // 프로퍼티.
        public Vector3 MovePosition 
        { 
            get 
            {
                return moveMarker.transform.position; 
            } 
        }

        public Vector3 AttackPosition
        {
            get
            {
                return attackMarker.transform.position;
            }
        }

        // 현재 공격을 대상으로 하는 몬스터.
        public MonsterStateManager AttackTarget
        {
            get
            {
                return attackMarker.GetComponentInParent<MonsterStateManager>();
            }
        }

        // 플레이어가 주겅ㅆ는지 알려주는 프로퍼티.
        public bool IsPlayerDead
        {
            get
            {
                return state == State.PlayerDead;
            }
        }

        // 마커를 켜고 끌 때 사용할 공개 메소드 (메시지).
        public void SetMoveMarkerActive(bool isActive)
        {
            // 예외 처리.
            if (moveMarker == null)
            {
                return;
            }

            moveMarker.SetActive(isActive);
        }

        // 공격 마커를 켜고 끌 때 사용할 공개 메소드 (메세지).
        public void SetAttackMarkerActive(bool isActive)
        {
            // 예외 처리.
            if (attackMarker == null)
            {
                return;
            }
            attackMarker.SetActive(isActive);
            
            // 마커를 끌 때는 트랜스폼 계층 해제.
            if (isActive == false)
            {
                attackMarker.transform.SetParent(null);
            }
        }

        // 초기 설정.
        private void Awake()
        {
            // 입력 관리자를 찾아서 클릭 이벤트에 등록.
            var inputManager = FindFirstObjectByType<InputManager>();
            if (inputManager != null)
            {
                inputManager.SubscribeOnMouseClicked(OnMousClicked);
            }

            // 이벤트 구독 - 플레이어의 죽음 이벤트 구독.
            var damageController = GetComponentInChildren<PlayerDamageController>();
            if (damageController != null)
            {
                damageController.SubscribeOnPlayerDead(OnPlayerDead);
            }

            // 플레이어 데이터 파일 검색.
            //Data = Resources.Load("GameData/Player Data") as PlayerData;
            //if (Data == null)
            //{
            //    Debug.LogWarning("플레이어 데이터 로드 실패.");
            //}
            //Data.ToJson();

            //Data =(PlayerData)Resources.Load("GameData/Player Data");


            // 레이어 마스크 설정.
            //layerMask = (1 << 8) | (1 << 9);
            //layerMask = 1 << LayerMask.NameToLayer(layerNames[0]);
            //layerMask += 1 << LayerMask.NameToLayer(layerNames[1]);
            //layerMask = LayerMask.GetMask(layerNames);
            layerMask = LayerMask.GetMask(blockLayerName, movableLayerName,monsterLayerName);

            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }

            // 스테이트 컴포넌트를 검색해서 states 배열에 추가.
            //states = new PlayerState[1];
            states = new PlayerState[(int)State.Length];

            // 컴포넌트 검색 후 설정.
            // 문자열로 타입을 구분하는 방법도 있음.
            //states[0] = GetComponent<PlayerIdle>();
            //states[0] = (PlayerState)GetComponent("PlayerIdle");
            //states[0] = (PlayerState)GetComponent(State.PlayerIdle.ToString());
            //states[0] = (PlayerState)GetComponent( ((State)0).ToString() );

            for (int ix = 0; ix < (int)State.Length; ++ix)
            {
                states[ix] = (PlayerState)GetComponent(((State)ix).ToString());
                states[ix].SetCharacterController(characterController);
            }
        }

        private void OnEnable()
        {
            SetState(State.PlayerIdle);
        }

        // 이벤트 리스너 메소드.
        private void OnMousClicked(Vector2 mousePosition)
        {
            // 플레이어가 죽으면 입력 막기.
            // EventSystem.current.IsPointerOverGameObject() 함수는 마우스 포인터가 UI 위에 있는지
            // 확인해주는 함수.

            if (IsPlayerDead || EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            // 스킬 상태일 때는 무시.
            if (state == State.PlayerSkill)
            {
                return;
            }

            // 카메라를 기준으로 Ray(반직선) 생성.
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            // 반직선 발사(방출). hitInfo에는 충돌한 물체의 정보가 저장됨.
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
            {
                // 테스트.
                //Debug.Log(hitInfo.collider.gameObject.layer);

                int layer = hitInfo.collider.gameObject.layer;
                if (layer.Equals(LayerMask.NameToLayer(blockLayerName)))
                {
                    //Debug.Log("Block이어서 이동 불가.");

                    // 마커 모두 끄기.
                    SetMoveMarkerActive(false);
                    SetAttackMarkerActive(false);
                    return;
                }

                // Movable 일 때.
                else if (layer.Equals(LayerMask.NameToLayer(movableLayerName)))
                {
                    // 이동 마커 위치 옮김.
                    Vector3 point = hitInfo.point;
                    //point.y = transform.position.y;
                    moveMarker.transform.position = point;

                    // 공격 마커 끄기.
                    SetAttackMarkerActive(false);

                    // 이동 마커 켜기.
                    SetMoveMarkerActive(true);

                    // 상태 변경.
                    SetState(State.PlayerMove);
                }

                // Monster 일 때.
                else if (layer.Equals(LayerMask.NameToLayer(monsterLayerName)))
                {
                    // 공격 마커 위치 설정.
                    // 충돌한 객체의 트랜스폼을 부모로 설정.
                    attackMarker.transform.SetParent(hitInfo.collider.transform);

                    // 부모 트랜스폼과 위치 동기화(맞추기).
                    attackMarker.transform.localPosition = Vector3.zero;

                    // 공격 마커 켜기.
                    SetAttackMarkerActive(true);

                    // 이동 마커 끄기.
                    SetMoveMarkerActive(false);

                    // 상태 변경 (쫓아가기).
                    SetState(State.PlayerChase);
                }
            }
        }

        // 상태 관리에 필요한 로직 작성(매 프레임 처리가 필요한 부분).
        //private void Update()
        //{
        //    // 입력 폴링(Polling).
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        // 카메라를 기준으로 Ray(반직선) 생성.
        //        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //        // 반직선 발사(방출). hitInfo에는 충돌한 물체의 정보가 저장됨.
        //        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, layerMask))
        //        {
        //            // 테스트.
        //            //Debug.Log(hitInfo.collider.gameObject.layer);

        //            int layer = hitInfo.collider.gameObject.layer;
        //            if (layer.Equals(LayerMask.NameToLayer(blockLayerName)))
        //            {
        //                //Debug.Log("Block이어서 이동 불가.");
        //                return;
        //            }
        //            else if (layer.Equals(LayerMask.NameToLayer(movableLayerName)))
        //            {
        //                // 이동 마커 위치 옮김.
        //                Vector3 point = hitInfo.point;
        //                //point.y = transform.position.y;
        //                moveMarker.transform.position = point;

        //                // 상태 변경.
        //                SetState(State.PlayerMove);
        //            }
        //        }
        //    }
        //}

        // 상태 설정 메소드
        public void SetState(State newState)
        {
            // 예외 처리.
            if (state == newState || state == State.PlayerDead)
            {
                return;
            }

            // 현재 상태 비활성화.
            if (state != State.None)
            {
                states[(int)state].enabled = false;
            }

            // 변경할 상태 활성화.
            if (newState != State.None)
            {
                states[(int)newState].enabled = true;
            }

            // 상태 값 업데이트.
            state = newState;

            // 상태 변경 이벤트 발행.
            OnPlayerStateChanged?.Invoke(state);
        }

        // 스테이트 변경 이벤트에 등록하는 메소드.
        public void SubscribeOnPlayerStateChanged(UnityAction<State> action)
        {
            OnPlayerStateChanged?.AddListener(action);
            // 등록 해제.
            //OnPlayerStateChanged.RemoveListener(action);
        }

        // 대미지 받았을 때 잘행되는 이벤트에 등록(구독)하는 메소드.
        public void SubscribeOnPlayerDamaged(UnityAction<float> action)
        {
            OnPlayerDamaged?.AddListener(action);
        }

        // 대미지 관리자를 통해 대미지를 전달 받는 메소드.
        public void ReceiveDamage(float damage)
        {
            // 대매지를 받았다는 이벤트 발행.
            OnPlayerDamaged?.Invoke(damage);
        }

        // 플레이어가 죽었을 때 발행되는 이벤트에  구독할 메소드
        private void OnPlayerDead()
        {
            // 죽음 상태로 전환.
            SetState(State.PlayerDead);

            // 마커 끄기 - 고민거리. 이게 누가 할 일인가?
            SetAttackMarkerActive(false );
            SetMoveMarkerActive(false );
        }

        // 스킬 버튼이 눌렸을 때 실행될 메소드.
        public void OnSkillButtonClicked()
        {
            SetState(State.PlayerSkill);
        }
    }
}
