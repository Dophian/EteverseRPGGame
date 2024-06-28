using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGGame
{
    public class SceneLoader : MonoBehaviour
    {
        // 로드할 씬 이름.
        [SerializeField, Tooltip("로드할 씬의 이름")] private string sceneName;

        // Start 버튼 클릭 이벤트와 연결할 리스너 메소드.
        public void OnStartButtonClicked()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}