using System;

namespace Portfolio.Core
{
    [Serializable]
    public class Settings
    {
        public string ServerAddress = null!;
        public int ServerPort;
        public string ServerSecret = null!;
    }
}
