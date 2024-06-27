using UnityEngine;

namespace RPGGame
{
    // 몬스터의 모든 상태 스크립트의 기반(Base, 부모-Parent) 클래스.
    // 스테이트는 크게 3가지 기능을 제공해야함.
    // 진입점 기능.
    // 업데이트 기능.
    // 탈출(종료) 기능.
    public class MonsterState : MonoBehaviour
    {
        // 필드.
        protected MonsterStateManager manager;
        protected CharacterController characterController;
        protected Transform refTransform;

        // 스테이트 매니저 설정 메소드.
        public void SetStateManager(MonsterStateManager manager)
        {
            this.manager = manager;
        }

        // 캐릭터 컨트롤러 설정 메소드.
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
        }

        // 업데이트 기능.
        protected virtual void Update()
        {
            // 중력 적용.
            characterController.Move(Physics.gravity * Time.deltaTime);
        }

        // 탈출(종료) 기능.
        protected virtual void OnDisable()
        {

        }
    }
}