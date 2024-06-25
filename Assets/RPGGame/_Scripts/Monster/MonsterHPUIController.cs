using UnityEngine;
using UnityEngine.UI;

namespace RPGGame
{
    // 몬스터의 HP Bar를 제어하는 스크립트.
    public class MonsterHPUIController : MonoBehaviour
    {
        // HPBar 이미지 컴포넌트.
        [SerializeField] private Image hpBar;

        private void Awake()
        {
            if (hpBar == null)
            {
                hpBar = transform.parent.GetComponentInChildren<Image>();
            }
        
            // 이벤트 구독.
            var damageController =
                transform.parent.GetComponentInChildren<MonsterDamageController>();
            if (damageController != null )
            {
                damageController.SubscribeOnMonsterHPChanged(OnHPChanged);
            }
        }

        // 몬스터의 체력 값이 변경되면 호출될 이벤트 리스너 메소드.
        private void OnHPChanged(float currentHP, float maxHP)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }
}