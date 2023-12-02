using Portfolio.Core;
using Portfolio.Core.Actors;
using Portfolio.Core.Components;
using Portfolio.Core.DependencyInjection;
using Portfolio.Core.Input;
using Portfolio.Core.Net;
using Portfolio.Core.UI;
using Portfolio.Unity.Actors;
using Portfolio.Unity.Input;
using Portfolio.Unity.Net;
using Portfolio.Unity.Systems;
using Portfolio.Unity.UI;
using UnityEngine;
using ILogger = Portfolio.Core.Logging.ILogger;
using Logger = Portfolio.Unity.Logging.Logger;

namespace Portfolio.Unity
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private Settings _settings = null!;
        [SerializeField] private CameraSystem _cameraSystem = null!;
        [SerializeField] private Canvas _canvas = null!;

        private Game _game = null!;

        private void Start()
        {
            var logger = new Logger();
            var client = new Client(_settings, logger);

            client.Connect();

            var container = new Container();
            container.Singleton<IContainer>(container);
            container.Singleton<IInput>(new UnityLegacyInput());
            container.Singleton<IViewFactory>(new ViewFactory(_canvas.transform));
            container.Singleton<IActorFactory>(new UnityActorFactory());
            container.Singleton<IClient>(client);
            container.Singleton<ILogger>(logger);

            _game = container.Instantiate<Game>();
            _game.ToMainMenuState();
        }

        private void OnPlayerSpawned(IActor actor)
        {
            var unityActor = (UnityActor) actor;
            _cameraSystem.StartFollow(unityActor.transform);
        }

        private void Update()
        {
            _game?.Tick(Time.deltaTime);
        }
    }
}
