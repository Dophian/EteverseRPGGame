using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPGGame
{
    public class SceneLoader : MonoBehaviour
    {
        // �ε��� �� �̸�.
        [SerializeField, Tooltip("�ε��� ���� �̸�")] private string sceneName;

        // Start ��ư Ŭ�� �̺�Ʈ�� ������ ������ �޼ҵ�.
        public void OnStartButtonClicked()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}