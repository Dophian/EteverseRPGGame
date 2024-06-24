using UnityEngine;

namespace RPGGame
{
    // 몬스터의 애니메이션 처리 담당 스크립트.
    public class MonsterAnimationController : MonoBehaviour
    {
        // 애니메이터 컴포넌트 변수.
        [SerializeField] private Animator refAnimator;

        private void Awake()
        {
            // 컴포넌트 변수 초기화.
            refAnimator = GetComponent<Animator>();

            // 몬스터의 상태 변경 이벤트에 등록.
            var manager = transform.parent.GetComponent<MonsterStateManager>();
            if (manager != null)
            {
                manager.SubscribeOnMonsterStateChanged(OnMonsterStateChanged);
            }
        }

        // 애니메이터에 파라미터를 설정하는 메소드.
        public void OnMonsterStateChanged(MonsterStateManager.State newState)
        {
            refAnimator.SetInteger("State", (int)newState);
        }
    }
}