namespace RPGGame
{
    public class PlayerMove : PlayerState
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            // 이동 마커 켜기.
            manager.SetMoveMarkerActive(true);
        }

        protected override void Update()
        {
            base.Update();

            // 회전.
            Utils.RotateToward(refTransform, manager.MovePosition, manager.Data.rotateSpeed);

            // 이동 및 도착 확인.
            if (Utils.MoveToward(
                refTransform,
                characterController,
                manager.MovePosition,
                manager.Data.moveSpeed) <= 0.5f)
            {
                manager.SetState(PlayerStateManager.State.PlayerIdle);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            // 이동 마커 끄기.
            manager.SetMoveMarkerActive(false);
        }
    }
}