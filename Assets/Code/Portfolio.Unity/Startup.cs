using Portfolio.Core;
using Portfolio.Core.Net;
using UnityEngine;

namespace Portfolio.Unity
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Settings _settings = null!;

        private Game _game = null!;

        private void Start()
        {
            var logger = new Logger();
            _game = new Game(logger, new Client(_settings, logger));
        }

        private void Update()
        {
            _game.Tick(Time.deltaTime);
        }
    }
}
