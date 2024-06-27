using UnityEngine;
using UnityEngine.Events;

namespace RPGGame
{
    // 플레이어의 공격 및 대미지 처리를 담당하는 스크립트.
    // 할일1: 애니메이션에서 공격 이벤트를 발행하면, 적절하게 대응.
    // 데이터: 대미지(공격력).

    // 할일2: 대미지를 전달 받으면 대미지 처리. 죽으면 죽음 이벤트 발행.
    // 데이터: 체력(HP).
    public class PlayerDamageController : MonoBehaviour
    {
        // 필드.
        // 플레이어 상태 관리자.
        [SerializeField] private PlayerStateManager manager;
        [SerializeField] private float hp = 100f;

        // 이벤트.
        [SerializeField] private UnityEvent<float, float> OnPlayerHPChanged;
        [SerializeField] private UnityEvent OnPlayerDead;

        private void Awake()
        {
            manager = GetComponentInParent<PlayerStateManager>();

            // 플레이어가 피격됐을 때 처리할 이벤트에 구독.
            manager.SubscribeOnPlayerDamaged(OnDamaged);

            // 체력 값을 데이터에 저장된 값으로 설정.
            hp = manager.Data.maxHP;
        }

        //private void OnEnable()
        //{
        //    // 체력 값을 데이터에 저장된 값으로 설정.
        //    hp = manager.Data.maxHP;
        //}

        // 이벤트 구독 메소드.
        public void SubscribeOnPlayerDead(UnityAction action)
        {
            OnPlayerDead?.AddListener(action);
        }

        public void SubscribeOnPlayerHPChanged(UnityAction<float, float> action)
        {
            OnPlayerHPChanged?.AddListener(action);
        }


        private void OnDamaged(float damage)
        {
            // 대미지 처리(체력)
            // 80% ~ 120% 사이의 랜덤 대미지.
            // 공격 아이템, 방어구, 기본 공격력, 기본 방어력. 레벨.
            hp -= Random.Range(damage * 0.8f, damage * 1.2f);
            hp = Mathf.Max(0f, hp);     // saturate(0이하는 버림).

            // 체력 변경 이벤트 발행.
            OnPlayerHPChanged?.Invoke(hp, manager.Data.maxHP);

            // 죽었으면 죽음 이벤트 발행.
            if (hp == 0)
            {
                OnPlayerDead?.Invoke();
            }
        }

        // 공격 애니메이션에서 ApllyDamage 이벤트가 발생하면 실행될 메소드.

        private void ApplyDamage()
        {
            DamageManager.SendDamageToMonster(
                manager, 
                manager.AttackTarget, 
                manager.Data.damage
            );
        }

        // 스킬 공격.
        private void ApplySkillDamage()
        {
            //Debug.Log("스킬 발동");
            DamageManager.SendDamageToRadial(
                transform,
                manager.Data.skillAttackDamage,
                manager.Data.skillAttackRange,
                //1 << 10
                LayerMask.GetMask("Monster")
            );
        }
    }
}