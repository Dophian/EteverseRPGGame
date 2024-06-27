using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGGame
{
    public class Portal : MonoBehaviour
    {
        // 이동할 씬 이름.
        [SerializeField] private string sceneName;

        private void OnTriggerEnter(Collider other)
        {
            // 충돌한 물체가 플레이어라면 씬 이동.
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}