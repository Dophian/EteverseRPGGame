using System.IO;
using UnityEngine;

namespace RPGGame
{
    // 플레이어가 사용할 데이터 스크립트.
    // 파일로 생성해서 사용함.
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        // 플레이어 스탯(Stat).
        public float moveSpeed = 5f;
        public float rotateSpeed = 540f;
        public float maxHP = 100f;
        public float damage = 30f;
        public float attackRange = 1.5f;

        // 스킬 스탯 (스킬 시간/스킬 공격력/스킬 범위(반경)).
        public float skillCoolTime = 2f;
        public float skillAttackDamage = 60f;
        public float skillAttackRange = 7f;

        public void ToJson()
        {
            string jsonString = JsonUtility.ToJson(this);
            File.WriteAllText(
                "Assets/RPGGame/Resources/GameData/PlayerData.txt",
                jsonString
            );
        }
    }
}