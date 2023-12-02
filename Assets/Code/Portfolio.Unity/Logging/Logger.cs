using Portfolio.Core.Logging;

namespace Portfolio.Unity.Logging
{
    public class Logger : ILogger
    {
        public void Debug(string message)
        {
            UnityEngine.Debug.Log(message);
        }
    }
}
