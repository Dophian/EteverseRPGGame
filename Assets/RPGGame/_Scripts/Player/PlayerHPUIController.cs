using UnityEngine;
using UnityEngine.UI;

namespace RPGGame
{
    // 플레이어 캐릭터의 HPBar 게이지 관리를 담당하는 스크립트
    public class PlayerHPUIController : MonoBehaviour
    {
        // 필드.
        [SerializeField] private Image hpBar;

        private void Awake()
        {
            // 이미지 컴포넌트 초기화.
            hpBar = transform.parent.GetComponentInChildren<Image>();

            // 이벤트 구독.
            var damageController
                = transform.parent.GetComponentInChildren<PlayerDamageController>();
            if (damageController != null)
            {
                damageController.SubscribeOnPlayerHPChanged(OnPlayerHPChanged);
            }
        }

        // 플레이어 HP가 변경될 때 발생되는 이벤트 리스너 메소드.
        private void OnPlayerHPChanged(float currentHP, float maxHP)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }
}