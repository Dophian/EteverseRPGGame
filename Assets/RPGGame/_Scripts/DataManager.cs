using UnityEngine;

namespace RPGGame
{
    // 플레이어/몬스터의 데이터를 관리하는 스크립트.
    public class DataManager : MonoBehaviour
    {
        // 전역에서 접근이 가능하도록 하기 위해 인스턴스 선언.
        private static DataManager instance = null;

        // 데이터 필드.
        [SerializeField] private PlayerData playerData;

        [SerializeField] private MonsterData slimeData;
        [SerializeField] private MonsterData wildPigData;

        private void Awake()
        {
            // 싱글톤 객체 설정.
            // 초기화가 안되어 있으면 현재 객체로 초기화.
            if (instance == null)
            {
                instance = this;
            }

            // 이미 다른 객체가 있다면, 이 객체는 제거.
            else
            {
                Destroy(gameObject);
            }
        }

        // 플레이어 데이터를 반환하는 메소드.
        public static PlayerData GetPlayerData()
        {
            // 예외 처리.
            // instace가 null이면 씬에서 검색해서 설정.
            if (instance == null)
            {
                instance = FindFirstObjectByType<DataManager>();
            }

            // PlayerData가 null이면 Resources 폴더에서 검색해서 로드.
            if (instance.playerData == null)
            {
                instance.playerData
                    = Resources.Load("GameData/Player Data") as PlayerData;
            }

            return instance.playerData;
        }
        // 몬스터 데이터를 반환하는 메소드.
        public static MonsterData GetMonsterData(string monsterName)
        {
            // 예외 처리
            if (instance == null)
            {
                instance = FindFirstObjectByType<DataManager>();
            }

            // 데이터 로드 및 반환.
            if (monsterName.Equals("Slime"))
            {
                if (instance.slimeData == null)
                {
                    instance.slimeData = Resources.Load("GameData/Monster Slime Data") as MonsterData;
                }

                return instance.slimeData;
            }
            else if (monsterName.Equals("WildPig"))
            {
                // 데이터 로드.
                if (instance.wildPigData == null)
                {
                    instance.wildPigData = Resources.Load("GameData/Monster WildPig Data") as MonsterData;
                }

                return instance.wildPigData;
            }
            else if (monsterName.Equals("Goblin"))
            {

            }
            return null;
        }
    }
}