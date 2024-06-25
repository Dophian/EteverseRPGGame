using UnityEngine;
using UnityEngine.Events;

namespace RPGGame
{
    // 몬스터의 대미지 처리를 담당하는 스크립트.
    internal class MonsterDamageController : MonoBehaviour
    {
        // 필드.
        [SerializeField] private float hp = 50f;
        [SerializeField] private UnityEvent OnMonsterDead;

        // 몬스터의 체력이 변경되면 발행되는 이벤트.
        [SerializeField] private UnityEvent<float,float> OnMonsterHPChanged;

        // 이벤트에 구독하는 메소드.
        public void SubscribeOnMonsterDead(UnityAction action)
        {
            OnMonsterDead?.AddListener(action);
        }

        public void SubscribeOnMonsterHPChanged(UnityAction<float,float> action)
        {
            OnMonsterHPChanged?.AddListener(action);
        }

        private void Awake()
        {
            // 이벤트 구독.
            var manager = GetComponentInParent<MonsterStateManager>();
            if (manager != null)
            {
                manager.SubscribeOnMonsterDamaged(OnDamaged);
            }
        }

        // 메소드.
        // 대미지 이벤트를 구독하는 메소드.
        private void OnDamaged(float damage)
        {
            //Debug.Log($"몬스터가 대미지를 입음: {damage}");

            // 대미지 처리.
            // 전잘 받은 대미지의 90% - 110% 사이의 대미지를 랜덤으로 적용.
            hp = hp - Random.Range(damage * 0.9f, damage * 1.1f);
            hp = Mathf.Max(0f,hp);

            // 체력 변경 이벤트 발행.
            OnMonsterHPChanged?.Invoke(hp, 50f);

            // hp가 0이면 죽음.
            if (hp == 0f)
            {
                OnMonsterDead?.Invoke();
            }
        }
    }
}
