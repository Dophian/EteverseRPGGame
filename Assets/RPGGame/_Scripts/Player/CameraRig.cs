using UnityEngine;

namespace RPGGame
{
    // 플레이어 캐릭터를 쫓아다니는 카메라 스크립트.
    public class CameraRig : MonoBehaviour
    {
        // 필드.
        // 카메라가 쫓아다닐 타겟(대상) 트랜스폼.
        [SerializeField] private Transform target;
        
        // 쫓아가는 속도를 제어하는 값.
        // 값이 커지면 더 빨리 쫓아감.
        [SerializeField] private float lag = 5f;

        // 내 트랜스폼 컴포넌트.
        private Transform refTransform;

        // 메소드.
        // 초기화.
        private void Awake()
        {
            // 트랜스폼 저장.
            refTransform = transform;

            // 타겟 검색 후 설정.
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }


        // 일정 거리를 유지하며 타겟을 쫓아다니는 기능. (매 프레임).
        // 약간의 지연(lag) 효과를 주고, 부드럽게 이동하도록 구현.
        private void LateUpdate()
        {
            // 약간의 딜레이(지연 효과)를 적용하면서 부드럽게 이동 처리.
            refTransform.position = Vector3.Lerp(
                refTransform.position,
                target.position,
                lag * Time.deltaTime
            );
        }

    }
}