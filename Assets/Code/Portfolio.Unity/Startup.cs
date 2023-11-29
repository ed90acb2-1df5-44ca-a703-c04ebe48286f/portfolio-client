using Portfolio.Core;
using Portfolio.Core.Components;
using Portfolio.Core.Net;
using Portfolio.Protocol.Authentication;
using Portfolio.Protocol.Messages;
using Portfolio.Unity.Actors;
using Portfolio.Unity.UI;
using UnityEngine;

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

            client.RegisterHandler<LoginResponse>(message =>
            {
                Debug.Log(message.GetType());
            });

            client.RegisterHandler<RegistrationResponse>(message =>
            {
                Debug.Log(message.GetType());
            });

            client.RegisterHandler<PlayerSpawnedMessage>(message =>
            {
                Debug.Log(message.GetType());
            });

            client.RegisterHandler<WorldStateMessage>(message =>
            {
            });

            client.Connect();

            _game = new Game(logger, client, new UnityLegacyInput(), new UnityActorFactory(), new ViewFactory(_canvas.transform));
            _game.ToMainMenuState();
        }

        private void OnPlayerSpawned(IActor actor)
        {
            var unityActor = (UnityActor) actor;
            _cameraSystem.StartFollow(unityActor.transform);
        }

        private void Update()
        {
            _game.Tick(Time.deltaTime);
        }
    }
}
