using System.IO;
using UnityEngine;

namespace RPGGame
{
    // [CreateAssetMenu]를 추가하면 생성 메뉴를 자동으로 만들어줌.
    [CreateAssetMenu]
    public class MonsterData : ScriptableObject
    {
        // 몬스터 스탯(Stat).

        // 정찰할 때 대기할 시간.
        public float patrolWaitTime = 3f;

        // 정찰할 때 이동 속도.
        public float patrolSpeed = 3f;

        // 쫒아갈 때 이동 속도.
        public float chaseSpeed = 4f;

        // 회전 속도.
        public float rotateSpeed = 360f;

        // 공격 가능 범위
        public float attackRange = 1.5f;

        // 시야 범위.
        public float sightRange = 5f;

        // 시야 각도 (전체 시야의 1/2 각도 값).
        public float sightAngle = 60f;

        // 체력.
        public float maxHP = 50f;

        // 공격력.
        public float damage = 10f;

        public void ToJson()
        {
            string jsonString = JsonUtility.ToJson(this);
            File.WriteAllText(
                "Assets/RPGGame/Resources/GameData/MonsterData.txt",
                jsonString
            );
        }
    }
}