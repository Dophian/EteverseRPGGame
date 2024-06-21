using UnityEngine;
using UnityEngine.Events;

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
            Length
        }

        // 플레이어의 스테이트를 나타내는 변수.
        [SerializeField] private State state = State.None;

        // 플레이어 스테이트의 배열.
        [SerializeField] private PlayerState[] states;

        // 스테이가 변경될 떄 발행되는 이벤트.
        [SerializeField] private UnityEvent<State> OnPlayerStateChanged;

        // 캐릭터 컨트롤러 컴포넌트.
        [SerializeField] private CharacterController characterController;

        // 이동 목표 지점을 표시해주는 이동 마커.
        [SerializeField] private GameObject moveMarker;

        // 레이어 값 - Ray와 충돌하는 LayerMask 생성에 사용.
        //[SerializeField] private string[] layerNames;
        [SerializeField] private string blockLayerName = "Block";
        [SerializeField] private string movableLayerName = "Movable";

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

        // 마커를 켜고 끌 때 사용할 공개 메소드 (메시지).
        public void SetMoveMarkerActive(bool isActive)
        {
            moveMarker.SetActive(isActive);
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

            // 레이어 마스크 설정.
            //layerMask = (1 << 8) | (1 << 9);
            //layerMask = 1 << LayerMask.NameToLayer(layerNames[0]);
            //layerMask += 1 << LayerMask.NameToLayer(layerNames[1]);
            //layerMask = LayerMask.GetMask(layerNames);
            layerMask = LayerMask.GetMask(blockLayerName, movableLayerName);

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
                    return;
                }
                else if (layer.Equals(LayerMask.NameToLayer(movableLayerName)))
                {
                    // 이동 마커 위치 옮김.
                    Vector3 point = hitInfo.point;
                    //point.y = transform.position.y;
                    moveMarker.transform.position = point;

                    // 상태 변경.
                    SetState(State.PlayerMove);
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
            if (state == newState)
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
            OnPlayerStateChanged.AddListener(action);
            // 등록 해제.
            //OnPlayerStateChanged.RemoveListener(action);
        }


    }
}
