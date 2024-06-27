using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGGame
{
    public class Portal : MonoBehaviour
    {
        // �̵��� �� �̸�.
        [SerializeField] private string sceneName;

        private void OnTriggerEnter(Collider other)
        {
            // �浹�� ��ü�� �÷��̾��� �� �̵�.
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}