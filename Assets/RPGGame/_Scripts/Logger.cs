using UnityEngine;

namespace RPGGame
{
    // Debug.Log를 대체하기 위한 스크립트.
    public class Logger
    {
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }

        public static void LogRed(object message)
        {
#if UNITY_EDITOR
            Debug.Log($"<color=red>{message}</color>");
#endif
        }
    }
}
