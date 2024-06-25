using UnityEngine;

namespace RPGGame
{
    // 슬라임의 정찰 상태 스크립트.
    // 할일: 상태 시작할 때 랜덤으로 정찰 위치를 선정해서 그 위치로 이동.
    // 그 위치에 도착했으면 다시 정지(Idle) 상태로 전환.
    public class SlimePatrol : MonsterState
    {
        // 필드.

        // 랜덤으로 지정한 이동 위치를 저장하는데 사용.
        [SerializeField] private Vector3 movePosition;

        // 디버깅용: 이동 마커.
        [SerializeField] private Transform moveMarker;

        // 이동할 때 사용할 이동 속도 (단위: 미터/초).
        [SerializeField] private float moveSpeed = 3f;

        // 이동할 때 사용할 회전 속도 (단위: 각도/초).
        [SerializeField] private float rotateSpeed = 360f;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 초기 설정.
            // 반지름이 1인 구체에서 랜덤으로 한 위치를 설정한 후에
            // 5를 곱해서 반지름이 5인 구체에서 랜덤으로 위치를 선택한 효과를 얻음.
            movePosition = refTransform.position + Random.insideUnitSphere * 5f;

            // 높이 값은 몬스터의 높이를 사용하도록 설정(높이 보정).
            movePosition.y = refTransform.position.y;

            // 이동 마커 위치 설정.
            if (moveMarker != null)
            {
                moveMarker.gameObject.SetActive(true);
                moveMarker.position = movePosition;
            }
        }

        protected override void Update()
        {
            base.Update();

            // 회전.
            Utils.RotateToward(refTransform, movePosition, rotateSpeed);

            // 이동.
            if (Utils.MoveToward(refTransform, characterController, movePosition, moveSpeed) <= 0.5f)
            {
                manager.SetState(MonsterStateManager.State.Idle);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            // 위치 초기화.
            movePosition = Vector3.zero;
            // 마커가 설정돼있으면 마커 끄기.
            if (moveMarker != null)
            {
                moveMarker.gameObject.SetActive(false);
            }
        }
    }
}