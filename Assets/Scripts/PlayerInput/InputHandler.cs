namespace PlayerInput
{
    public abstract class InputHandler
    {
        protected PlayerMovement Player;

        public void SetPlayer(PlayerMovement player)
        {
            Player = player;
        }
    
        public abstract void Handle();
    }
}