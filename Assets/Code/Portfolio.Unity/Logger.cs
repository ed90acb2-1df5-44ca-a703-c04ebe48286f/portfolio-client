using Portfolio.Core;

namespace Portfolio.Unity
{
    public class Logger : ILogger
    {
        public void Debug(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}
