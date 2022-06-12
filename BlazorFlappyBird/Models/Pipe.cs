namespace BlazorFlappyBird.Models
{
	public class Pipe
	{
		public static int LEFT_START = 500;

		public static int MAX_BOTTOM_MARGIN = 60;

		public static int OFF_SCREEN = -60;

		public static int WIDTH = 60;

		public static int HEIGHT = 300;

		public int LeftDistance { get; private set; } = LEFT_START;

		public int BottomDistance { get; private set; } = new Random().Next(0, MAX_BOTTOM_MARGIN);

		public int Speed { get; private set; } = 2;

		public int Gap { get; private set; } = 130;

		public int GapBottom => BottomDistance + HEIGHT;
		
		public bool IsOffScreen()
		{
			return LeftDistance < OFF_SCREEN;
		}

		public bool IsHalfWay()
		{
			int x1 = (LEFT_START / 2) + (Bird.WIDTH / 2);
			int x2 = (LEFT_START / 2) - (Bird.WIDTH / 2) - (WIDTH/2);

			return (LeftDistance <= x1) && (LeftDistance > x2);
		}

		public void Move()
		{
			LeftDistance -= Speed;
		}
	}
}
