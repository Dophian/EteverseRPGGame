using UnityEngine;

namespace RPGGame
{
    // 플레이어 스테이트의 기반 클래스. (다형성을 위해서 상속 구조를 사용).
    public class PlayerState : MonoBehaviour
    {
        // 필드.
        protected PlayerStateManager manager;
        protected CharacterController characterController;
        protected Transform refTransform;

        // 캐릭터 컨트롤러 컴포넌트는 외부에서 전달 받아서 설정.
        public void SetCharacterController(CharacterController characterController)
        {
            this.characterController = characterController;
        }

        // 진입점(Entry Point).
        protected virtual void OnEnable()
        {
            if (refTransform == null)
            {
                refTransform = transform;
            }

            if (manager == null)
            {
                manager = GetComponent<PlayerStateManager>();
            }
        }

        // 업데이트.
        protected virtual void Update()
        {
            // 중력 적용.
            characterController.Move(Physics.gravity * Time.deltaTime);
        }

        // 종료.
        protected virtual void OnDisable()
        {

        }
    }
}