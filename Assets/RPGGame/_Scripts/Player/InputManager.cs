using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace RPGGame
{
    // 새로운 입력 시스템(New Input System)을 통해 입력 이벤트를 발행하는 스크립트.
    // 기존 입력 시스템(Legacy Input System).
    public class InputManager : MonoBehaviour
    {
        // 마우스가 클릭되면 마우스 커서의 위치를 전달하면서 이벤트를 발행함.
        [SerializeField] private UnityEvent<Vector2> OnMouseClicked;

        // 이벤트에 등록하는 공개 메소드(메시지).
        public void SubscribeOnMouseClicked(UnityAction<Vector2> action)
        {
            OnMouseClicked.AddListener(action);
        }

        private void OnFire()
        {
            //Debug.Log("클릭됨");

            OnMouseClicked?.Invoke(Mouse.current.position.ReadValue()
            );
        }
    }
}