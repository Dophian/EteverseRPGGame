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

        }

        // 플레이어가 몬스터에게 대미지를 전달할 때 사용하는 메소드.
        public static void SendDamageToMonster(
            PlayerStateManager from,
            MonsterStateManager to,
            float damage)
        {
            to.ReceiveDamage(damage);
        }
    }
}
