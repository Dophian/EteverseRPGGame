using UnityEngine;

// 데모를 위한 플레이어 컨트롤 스크립트.
// 기능 확인 후 구조 변경 예정.
public class PlayerControl : MonoBehaviour
{
    // 정지 이동 처리를 위해 열거형 선언.
    public enum State
    {
        None = -1,
        Idle,
        Move
    }

    // 이동할 목표 지점을 표시할 게임 오브젝트.
    [SerializeField] private GameObject moveTarget;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private State state = State.None;

    [SerializeField] private CharacterController characterController;

    private void Idle()
    {
    }

    private void Move()
    {
        // 프레임 마다 조금씩 이동 처리.

        // 이동해야하는 방향 벡터를 구함.
        Vector3 direction = moveTarget.transform.position - transform.position;

        // 이번 프레임에 이동할 양 = 이동 방향 * 이동 빠르기 * 프레임 시간(DeltaTime).
        Vector3 position = direction.normalized * 3f * Time.deltaTime;
        //position += transform.position;

        characterController.Move(position);

        // 목표 지점에 도착했으면 Idle(정지)로 전환.
        // 내 위치와 이동해야하는 목표 위치 사이의 거리를 구하기.
        float distance = Vector3.Distance(transform.position,moveTarget.transform.position);

        // 남은 거리가 오차범위(약 20cm) 안쪽이라면 도착으로 판정.
        if (distance <= 0.2f)
        {
            // 상태를 정지로 전환하고, 이동 목표 GO 끄기.
            state = State.Idle;
            moveTarget.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        state = State.Idle;
        moveTarget.SetActive(false);
    }

    // 이동 로직 작성.
    private void Update()
    {
        // 마우스 클릭(모바일->터치로 변환)을 기반으로 이동.
        if (Input.GetMouseButtonDown(0))
        {

            // 마우스 위치(스크린(화면) 좌표계 사용)를 3차원 월드 위치로 변환.
            //Vector3 position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //transform.position = position;

            // 일단 카메라 방향 및 마우스 위치를 기준으로 Ray(반직선)을 생성.
            // Picking (픽킹).
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            // Ray 발사.
            bool result = Physics.Raycast(ray, out RaycastHit hitInfo);
            if (result == true)
            {
                // 충돌한 지점의 위치를 확인.
                Vector3 position = hitInfo.point;

                // y위치 보정.
                position.y = transform.position.y;

                // 위치 설정.
                //transform.position = position;
                moveTarget.transform.position = position;
                moveTarget.SetActive(true);

                // 상태 변경.
                state = State.Move;
            }
        }

        // FSM.
        if (state == State.Idle)
        {
            Idle();
        }
        else if (state == State.Move)
        {
            Move();
        }
    }
}