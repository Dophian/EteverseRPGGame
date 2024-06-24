using UnityEngine;

namespace Test
{
    // �׽�Ʈ ����: �þ߸� ������ �� �� ������ �ʿ���.
    // �׽�Ʈ1: �� ��ü ������ ������ Ȯ��.
    // �׽�Ʈ2: �� ��ü ������ �Ÿ��� Ȯ��.
    public class AngleCheckTester : MonoBehaviour
    {
        // ���Ϸ��� ���.
        [SerializeField] private Transform target;

        // ������: ���� ��.
        [SerializeField] private float angle;

        // ������: �Ÿ� ��.
        [SerializeField] private float distance;

        private Transform refTransform;

        private void Awake()
        {
            refTransform = transform;
        }

        private void Update()
        {
            if (refTransform && target)
            {
                // ���� ���: ���� Ȱ��.
                // �� ���Ͱ� �ʿ���
                // ����1: ��ü�� �չ��� ����(forward).
                // ����2: ��ü�� ��ġ���� ��� ��ü�� ���ϴ� ����(direction).
                Vector3 direction = target.position - refTransform.position;

                // ������ ũ�⸦ 1�� �����.
                direction.Normalize();

                // ���� ������ ���� ���. (Vector3.Dot �Լ��� �� ������ ������ ���).
                angle = Mathf.Acos(Vector3.Dot(direction, refTransform.forward));

                // ������ ������ ��ȯ.
                angle = angle * Mathf.Rad2Deg;

                // �Ÿ� ���.
                distance = (target.position - refTransform.position).magnitude;
            }
        }

        // ������: ����� �׸���.
        // ���ǻ���: Update �Լ��� �����ϰ� �������� �����ϴµ�,
        // ������ ��忡���� ������.
        private void OnDrawGizmos()
        {
            // ���� ó��.
            if (refTransform == null)
            {
                return;
            }

            // ����� ���� ����.
            Gizmos.color = Color.blue;

            // ���� ������Ʈ�� �չ����� ������ �׸���.
            Gizmos.DrawLine(
                refTransform.position,
                refTransform.position + refTransform.forward * 3f
                );

            // Ÿ�� ������Ʈ�� ���ؼ� �� �׸���.
            Gizmos.color = Color.red;
            Gizmos.DrawLine(refTransform.position, target.position);
        }
    }
}
