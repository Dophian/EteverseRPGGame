using UnityEngine;
using UnityEngine.UI;

namespace RPGGame
{
    // ������ HP Bar�� �����ϴ� ��ũ��Ʈ.
    public class MonsterHPUIController : MonoBehaviour
    {
        // HPBar �̹��� ������Ʈ.
        [SerializeField] private Image hpBar;

        private void Awake()
        {
            if (hpBar == null)
            {
                hpBar = transform.parent.GetComponentInChildren<Image>();
            }
        
            // �̺�Ʈ ����.
            var damageController =
                transform.parent.GetComponentInChildren<MonsterDamageController>();
            if (damageController != null )
            {
                damageController.SubscribeOnMonsterHPChanged(OnHPChanged);
            }
        }

        // ������ ü�� ���� ����Ǹ� ȣ��� �̺�Ʈ ������ �޼ҵ�.
        private void OnHPChanged(float currentHP, float maxHP)
        {
            hpBar.fillAmount = currentHP / maxHP;
        }
    }
}