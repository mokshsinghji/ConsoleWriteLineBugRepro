using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using TypingTest.Utilities;
using System.Timers;

namespace TypingTest.Pages
{
    public partial class Index
    {
        public bool IsStarted { get; set; } = false;

        public JSConsole JSConsole { get; set; }

        [Inject]
        public IJSRuntime JsRuntime { get; set; }

        public int Cpm { get; set; }

        public int Wpm
        {
            get
            {
                return (int)(Cpm / 4.7);
            }
        }

        private System.Timers.Timer Timer { get; set; }

        public string Seconds { get; set; } = "00";
        public string Minutes { get; set; } = "00";

        public event EventHandler<string> TextChanged;

        private string _textAreaContent = "";

        public string TextAreaContent
        {
            get
            {
                return _textAreaContent;
            }
            set
            {
                _textAreaContent = value;
                TextChanged.Invoke(this, value);
            }
        }

        private Time TimePassed { get; set; } = new Time()
        {
            Minutes = 0,
            Seconds = 30,
        };

        public Index()
        {
            TextChanged += OnTextChange;
        }

        public void OnTextChange(object sender, string text)
        {
            if (!IsStarted)
            {
                IsStarted = true;

                Timer = new()
                {
                    Interval = 1000
                };

                Timer.Elapsed += AddTime;

                Timer.Start();

            }

            Cpm = text.ToString().Length / (TimePassed.GetTimeInSeconds() == 0 ? 1 : TimePassed.Seconds) * 60;

            JSConsole.Log("TextChanged");
        }

        private void AddTime(object sender, ElapsedEventArgs e)
        {
            if (TimePassed.Seconds >= 59)
            {
                TimePassed.Minutes += 1;
                TimePassed.Seconds = 0;
                return;
            }
            TimePassed.Seconds += 1;
        }

        protected override void OnInitialized()
        {
            JSConsole = new JSConsole(JsRuntime);
            TimePassed.TimeChanged += OnTimeChanged;
        }

        private async void OnTimeChanged(object sender, string time)
        {
            await InvokeAsync(() =>
            {
                JSConsole.Log("OnTimeChanged");
                Seconds = TimePassed.Seconds.ToString().PadLeft(2, '0');
                Minutes = TimePassed.Minutes.ToString().PadLeft(2, '0');
                Cpm = TextAreaContent.Length / ((TimePassed.GetTimeInSeconds() == 0 ? 1 : TimePassed.GetTimeInSeconds()) * 60);

                StateHasChanged();

                
            });
    }
}
}
