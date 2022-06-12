namespace BlazorFlappyBird.Models
{
    public class Bird
    {
        public static int JumpStrength = 50;

        public static int WIDTH = 30;
        public static int HEIGHT = 45;  
        public int Id { get; set; }

        public int GroundDistance { get; set; } = 100;

        public void Fall(int gravity)
        {
            GroundDistance -= gravity;
        }

        public bool IsOnGround()
		{
            return GroundDistance <= 0;
		}

        public void Jump()
        {
            if(GroundDistance <= 530)
                GroundDistance += JumpStrength;
        }

    }
}
