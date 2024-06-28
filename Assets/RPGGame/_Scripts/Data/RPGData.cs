using System.Collections.Generic;
using UnityEngine;

namespace RPGGame
{
    // RPG 샘플 데이터용 스크립트.
    [CreateAssetMenu]
    public class RPGData : ScriptableObject
    {
        // 데이터 저장용 클래스.
        [System.Serializable]
        public class Attribute
        {
            public string name;
            public float maxHP;
            public float damage;
            public float moveSpeed;
            public float rotateSpeed;
            public float attackRange;
        }

        // 리스트로 데이터 선언.
        public List<Attribute> data = new List<Attribute>();
    }
}