using UnityEngine;

namespace RPGGame
{
    // ���� �������� ���� ������Ʈ ��ũ��Ʈ.
    // ����1: �浹(Ŭ��)�� �ȵǵ��� �ݶ��̴� ����.
    // ����2: ���� �ð� �ڿ� ���� ������Ʈ ����.
    public class MonsterDead : MonsterState
    {
        // ���� ������Ʈ ���� ������ ����ϴ� �ð� ��(����: ��).
        [SerializeField] private float deadWaitTime = 3f;

        // ���� �� ��Ȱ��ȭ ��ų �ݶ��̴�.
        [SerializeField] private Collider refCollider;

        protected override void OnEnable()
        {
            base.OnEnable();

            // ����1 ó��.
            if (refCollider != null)
            {
                refCollider.enabled = false;
            }

            // ����2 ó��.
            Destroy(gameObject, deadWaitTime);
        }
    }
}