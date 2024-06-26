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