using Microsoft.JSInterop;

namespace TypingTest.Utilities
{

    public class JSConsole
    {

        public IJSRuntime JsRuntime { get; set; }

        public JSConsole(IJSRuntime jsRuntime)
        {
            JsRuntime = jsRuntime;
        }

        public void Log(object message)
        {
            JsRuntime.InvokeVoidAsync("console.log", message);
        }
    }
}
