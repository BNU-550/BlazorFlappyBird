using System.ComponentModel;

namespace BlazorFlappyBird.Models
{
    public class GameContoller : INotifyPropertyChanged
    {
        public static int SCREEN_WIDTH = 500;
        public static int SCREEN_HEIGHT = 500;

        public static int GROUND_HEIGHT = 150;

        public Bird Bird { get; private set; }

        public List<Pipe> Pipes { get; private set; }

        public bool IsRunning { get; private set; } = false;

        private readonly int gravity = 2;

        public event PropertyChangedEventHandler? PropertyChanged;
        
        public event EventHandler OnGameUpdate;

        public GameContoller()
        {
            StartGame();
        }

        public async void Update()
        {
            IsRunning = true;

            while(IsRunning)
            {
                ManagePipes();
                MoveObjects();
                DetectCollisions();

                OnGameUpdate?.Invoke(this, EventArgs.Empty);
                await Task.Delay(20);
            }
        }

        public void MoveObjects()
		{
            Bird.Fall(gravity);
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bird)));

            foreach (Pipe pipe in Pipes)
            {
                pipe.Move();
            }
        }

        /// <summary>
        /// Check for a pipe in the middle, and if so
        /// check for collision with top & bottom pipe
        /// </summary>
        public void DetectCollisions()
		{
            if (Bird.IsOnGround())
                EndGame();

            var centeredPipe = Pipes.FirstOrDefault(p => p.IsHalfWay());
            
            if(centeredPipe != null)
            {
                bool hasCollided1 = Bird.GroundDistance < 
                    centeredPipe.GapBottom - GROUND_HEIGHT;
                
                bool hasCollided2 = Bird.GroundDistance + Bird.HEIGHT > 
                    centeredPipe.GapBottom + centeredPipe.Gap - GROUND_HEIGHT;
                
                if(hasCollided1 || hasCollided2)EndGame();
            }
        }

        /// <summary>
        /// If the last pipe is more than half the screen width
        /// then add a new Pipe.  If the first pipe is not on
        /// the screen remove it from the list
        /// </summary>
        public void ManagePipes()
		{
            if(!Pipes.Any() || Pipes.Last().IsHalfWay())
                Pipes.Add(new Pipe());

            if(Pipes.First().IsOffScreen())
                Pipes.Remove(Pipes.First());
		}
        public void StartGame()
        {
            Pipes = new List<Pipe>();
            if(!IsRunning)
            {
                Bird = new Bird();
                Update();
            }
        }

        public void EndGame()
        {
            IsRunning = false;
        }

        public void Jump()
		{
            if(IsRunning) Bird.Jump();
		}
    }
}
