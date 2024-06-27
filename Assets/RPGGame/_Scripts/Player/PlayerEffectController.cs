using System.Collections;
using UnityEngine;

namespace RPGGame
{
    // �÷��̾ ����� ���� ������Ʈ ��� ����Ʈ�� �����ϴ� ��ũ��Ʈ.
    public class PlayerEffectController : MonoBehaviour
    {
        // ����Ʈ.
        // - ���� ������Ʈ (�Ѹ� ����ǰ�, ���� ��� ����).
        // - ��� �ð� (����: ��).
        #region ����Ʈ Ŭ����
        [System.Serializable]
        public class Effect
        {
            // ����Ʈ ���� ������Ʈ (���� �̸� ��ġ�صΰ� ������ ���).
            [SerializeField] private GameObject effect;

            // ����Ʈ ��� �ð�.
            [SerializeField] private float playTime;
            public float PlayTime
            {
                get
                {
                    return playTime;
                }
            }

            // �޽���.
            public void Play()
            {
                // ���� ó��.
                if (effect == null)
                {
                    return;
                }

                // ���.
                effect.SetActive(false);
                effect.SetActive(true);
            }

            public void Stop()
            {
                // ���� ó��.
                if (effect == null)
                {
                    return;
                }

                effect.SetActive(false);
            }
        }
        #endregion
        // ���� ��ǿ� ����� ����Ʈ.
        [SerializeField] private Effect attackEffect;

        // ��ų ���� ��ǿ� ����� ����Ʈ.
        [SerializeField] private Effect skillAttackEffect;

        // �̺�Ʈ ������ �޼ҵ�.
        private void StartAttack()
        {
            StartCoroutine(PlayEffect(attackEffect));
        }

        // ��ų ����Ʈ.
        public void StartSkillAttack()
        {
            StartCoroutine(PlayEffect(skillAttackEffect));
        }

        private IEnumerator PlayEffect(Effect effect)
        {
            // ����Ʈ ��� (���� ������Ʈ Ȱ��ȭ).
            effect.Play();

            // ��� �ð���ŭ ���.
            yield return new WaitForSeconds(effect.PlayTime);

            // ����Ʈ ���� (���� ������Ʈ ��Ȱ��ȭ).
            effect.Stop();
        }
        //private IEnumerator PlayAttackEffect()
        //{
        //    // ����Ʈ ��� (���� ������Ʈ Ȱ��ȭ).
        //    attackEffect.Play();

        //    // ��� �ð���ŭ ���.
        //    yield return new WaitForSeconds(attackEffect.PlayTime);

        //    // ����Ʈ ���� (���� ������Ʈ ��Ȱ��ȭ).
        //    attackEffect.Stop();
        //}
    }
}