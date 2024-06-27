using System.Collections;
using UnityEngine;

namespace RPGGame
{
    // 플레이어가 재생할 게임 오브젝트 기반 이펙트를 관리하는 스크립트.
    public class PlayerEffectController : MonoBehaviour
    {
        // 이펙트.
        // - 게임 오브젝트 (켜면 재생되고, 끄면 재생 정지).
        // - 재생 시간 (단위: 초).
        #region 이펙트 클래스
        [System.Serializable]
        public class Effect
        {
            // 이펙트 게임 오브젝트 (씬에 미리 배치해두고 참조를 사용).
            [SerializeField] private GameObject effect;

            // 이펙트 재생 시간.
            [SerializeField] private float playTime;
            public float PlayTime
            {
                get
                {
                    return playTime;
                }
            }

            // 메시지.
            public void Play()
            {
                // 예외 처리.
                if (effect == null)
                {
                    return;
                }

                // 재생.
                effect.SetActive(false);
                effect.SetActive(true);
            }

            public void Stop()
            {
                // 예외 처리.
                if (effect == null)
                {
                    return;
                }

                effect.SetActive(false);
            }
        }
        #endregion
        // 공격 모션에 사용할 이펙트.
        [SerializeField] private Effect attackEffect;

        // 스킬 공격 모션에 사용할 이펙트.
        [SerializeField] private Effect skillAttackEffect;

        // 이벤트 리스너 메소드.
        private void StartAttack()
        {
            StartCoroutine(PlayEffect(attackEffect));
        }

        // 스킬 이펙트.
        public void StartSkillAttack()
        {
            StartCoroutine(PlayEffect(skillAttackEffect));
        }

        private IEnumerator PlayEffect(Effect effect)
        {
            // 이펙트 재생 (게임 오브젝트 활성화).
            effect.Play();

            // 재생 시간만큼 대기.
            yield return new WaitForSeconds(effect.PlayTime);

            // 이펙트 정지 (게임 오브젝트 비활성화).
            effect.Stop();
        }
        //private IEnumerator PlayAttackEffect()
        //{
        //    // 이펙트 재생 (게임 오브젝트 활성화).
        //    attackEffect.Play();

        //    // 재생 시간만큼 대기.
        //    yield return new WaitForSeconds(attackEffect.PlayTime);

        //    // 이펙트 정지 (게임 오브젝트 비활성화).
        //    attackEffect.Stop();
        //}
    }
}