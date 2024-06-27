using UnityEngine;

namespace RPGGame
{
    // 대미지 이벤트를 중간에서 전달하는 관리자 스크립트.
    internal class DamageManager : MonoBehaviour
    {
        // 메세지 (공개 메소드).

        // 몬스터가 플레이어에게 대미지를 전달할 때 사용하는 메소드.
        public static void SendDamageToPlayer(
            MonsterStateManager from,
            PlayerStateManager to,
            float damage)
        {
            // 예외처리.
            if (to == null)
            {
                return;
            }

            to.ReceiveDamage(damage);
        }

        // 플레이어가 몬스터에게 대미지를 전달할 때 사용하는 메소드.
        public static void SendDamageToMonster(
            PlayerStateManager from,
            MonsterStateManager to,
            float damage)
        {
            if (to == null)
            {
                return;
            }
        
        
            to.ReceiveDamage(damage);
        }

        // 광역 대미지 전달.
        // from: 대미지 전달을 요청한 객체의 트랜스폼.
        // damage : 전달할 대미지의 양.
        // range: 대미지 범위(반지름).
        // layermask: 충돌 확인에 사용할 레이어마스크.
        public static void SendDamageToRadial(
            Transform from,
            float damage,
            float range,
            int layermask)
        {
            // 범위를 사용해서 충돌한 물체 획득.
            var enemies = Physics.OverlapSphere(from.position, range, layermask);

            // 루프를 돌면서 대미지 전달.
            foreach (var enemy in enemies)
            {
                // 대미지 전달을 위해 몬스터 상태 관리자 얻어오기.
                var target = enemy.GetComponent<MonsterStateManager>();
                if (target != null)
                {
                    target.ReceiveDamage(damage);
                }
            }
        }
    }
}
