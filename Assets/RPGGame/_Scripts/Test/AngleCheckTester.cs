using UnityEngine;

namespace Test
{
    // 테스트 이유: 시야를 구현할 때 구 로직이 필요함.
    // 테스트1: 두 물체 사이의 각도를 확인.
    // 테스트2: 두 물체 사이의 거리를 확인.
    public class AngleCheckTester : MonoBehaviour
    {
        // 비교하려는 대상.
        [SerializeField] private Transform target;

        // 디버깅용: 각도 값.
        [SerializeField] private float angle;

        // 디버깅용: 거리 값.
        [SerializeField] private float distance;

        private Transform refTransform;

        private void Awake()
        {
            refTransform = transform;
        }

        private void Update()
        {
            if (refTransform && target)
            {
                // 각도 계산: 내적 활용.
                // 두 벡터가 필요함
                // 벡터1: 물체의 앞방향 벡터(forward).
                // 벡터2: 물체의 위치에서 대상 물체로 향하는 벡터(direction).
                Vector3 direction = target.position - refTransform.position;

                // 벡터의 크기를 1로 만들기.
                direction.Normalize();

                // 라디안 단위의 각도 계산. (Vector3.Dot 함수는 두 벡터의 내적을 계산).
                angle = Mathf.Acos(Vector3.Dot(direction, refTransform.forward));

                // 라디안을 각도로 변환.
                angle = angle * Mathf.Rad2Deg;

                // 거리 계산.
                distance = (target.position - refTransform.position).magnitude;
            }
        }

        // 디버깅용: 기즈모 그리기.
        // 주의사항: Update 함수와 유사하게 매프레임 동작하는데,
        // 에디터 모드에서도 동작함.
        private void OnDrawGizmos()
        {
            // 예외 처리.
            if (refTransform == null)
            {
                return;
            }

            // 기즈모 색상 설정.
            Gizmos.color = Color.blue;

            // 게임 오브젝트의 앞방향을 선으로 그리기.
            Gizmos.DrawLine(
                refTransform.position,
                refTransform.position + refTransform.forward * 3f
                );

            // 타겟 오브젝트를 향해서 선 그리기.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(refTransform.position, target.position);
        }
    }
}
