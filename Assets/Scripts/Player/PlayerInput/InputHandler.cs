namespace PlayerInput
{
    public abstract class InputHandler
    {
        protected Player HandlingPlayer;

        public void SetPlayer(Player player)
        {
            HandlingPlayer = player;
        }
    
        public abstract void Handle();
    }
}