using UnityEngine;

namespace RPGGame
{
    // 재사용이 가능한 기능을 제공하는 유틸리티 스크립트.
    public class Utils
    {
        // 이동.
        public static float MoveToward(
            Transform selfTransform,
            CharacterController characterController,
            Vector3 destination,
            float moveSpeed)
        {
            // 프레임 마다 이동을 처리한 뒤.
            // 이동 방향.
            Vector3 direction = destination - selfTransform.position;

            // 이동하려는 목적지를 향해서 꾸준히 이동.
            // 다음에 이동할 양(Amount) 구하기 - 방향이 고려되어야 함.
            Vector3 moveAmout = direction.normalized * moveSpeed * Time.deltaTime;

            //refTransform.position += moveAmout;
            characterController.Move(moveAmout);

            // 목적지까지 남은 거리를 전달.
            return Vector3.Distance(destination, selfTransform.position);
        }

        // 회전.
        public static void RotateToward(
            Transform selfTransform,
            Vector3 destination,
            float rotateSpeed
            )
        {
            // 이동 방향.
            Vector3 direction = destination - selfTransform.position;

            // 이동하는 방향에 맞게 부드럽게 회전.
            Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);
            if (directionXZ != Vector3.zero)
            {
                selfTransform.rotation = Quaternion.RotateTowards(
                    selfTransform.rotation,                  // 현재 회전 값.
                    Quaternion.LookRotation(directionXZ),   // 목표 방향(회전) 값.
                    rotateSpeed * Time.deltaTime            // 한 프레임에 회전할 수 있는 각도.
                );
            }
        }

        // 시야 판정 메소드.
        public static bool IsInSight(
            Transform selfTransform,
            Transform targetTransform,
            float sightAngle,
            float sightRange)
        {
            // 각도 안에 있는지?
            float angle = Vector3.Angle(
                selfTransform.forward, 
                targetTransform.position - selfTransform.position
            );
            
            if (angle <= sightAngle) 
            {
                // 거리 안에 있는지?
                float distance = Vector3.Distance(
                    selfTransform.position, targetTransform.position 
                );

                if (distance <= sightRange)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
