namespace TypingTest.Utilities
{
    public class Time
    {
        public int Minutes { get; set; }
        private int _seconds;
        public int Seconds {
            get
            {
                return _seconds;
            } set
            {
                _seconds = value;
                TimeChanged?.Invoke(this, $"{Seconds,2}:{Minutes,2}");
            }
        }

        public int GetTimeInSeconds()
        {
            var inSeconds = (Minutes * 60) + Seconds;
            return inSeconds;
        }

        public event EventHandler<string> TimeChanged;
    }
}