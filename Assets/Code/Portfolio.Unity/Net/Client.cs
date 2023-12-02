using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Google.Protobuf;
using LiteNetLib;
using LiteNetLib.Utils;
using Portfolio.Common;
using Portfolio.Core;
using Portfolio.Core.Logging;
using Portfolio.Core.Net;

namespace Portfolio.Unity.Net
{
    public class Client : INetEventListener, IClient
    {
        private readonly Dictionary<ulong, Action<int, NetDataReader>> _handlers = new();
        private readonly Dictionary<Type, object> _recentMessages = new();
        private readonly BufferWriter _buffer = new();

        private readonly Settings _settings;
        private readonly ILogger _logger;
        private readonly NetManager _manager;

        private NetPeer? _peer;

        public Client(Settings settings, ILogger logger)
        {
            _settings = settings;
            _logger = logger;

            _manager = new NetManager(this);
            _manager.AutoRecycle = true;
        }

        public void Connect()
        {
            _manager.Start();
            _peer = _manager.Connect(_settings.ServerAddress, _settings.ServerPort, _settings.ServerSecret);
        }

        public void PollEvents()
        {
            _manager.PollEvents();
        }

        public void Disconnect()
        {
            _manager.Stop();
        }

        public async Task<TMessage> WaitFor<TMessage>() where TMessage : class
        {
            var messageType = typeof(TMessage);
            TMessage? message = null;

            await UniTask.WaitUntil(() =>
            {
                if (!_recentMessages.ContainsKey(messageType))
                {
                    return false;
                }

                message = (TMessage) _recentMessages[messageType];
                _recentMessages.Remove(messageType);
                return true;
            });

            return message!;
        }

        public void RegisterHandler<TMessage>(Action<TMessage> handler) where TMessage : class, new()
        {
            var message = new TMessage();

            _handlers[TypeHash.Hash<TMessage>()] = (peer, reader) =>
            {
                //_logger.Debug($"Processing Packet: {typeof(TMessage).Name}");

                ((IMessage) message).MergeFrom(reader.GetRemainingBytes());

                handler.Invoke(message);

                _recentMessages[typeof(TMessage)] = message;
            };
        }

        public void Command<TCommand>(TCommand command) where TCommand : class
        {
            if (_peer == null)
            {
                _logger.Debug("Can't send command. Not connected!");
                return;
            }

            _buffer.Reset();
            _buffer.Write(TypeHash.Hash<TCommand>());

            ((IMessage) command).WriteTo(_buffer);

            _peer.Send(_buffer.AsSpan(), DeliveryMethod.ReliableOrdered);
        }

        public void OnPeerConnected(NetPeer peer)
        {
            _logger.Debug("OnPeerConnected");
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            _logger.Debug("OnPeerDisconnected");

            _peer = null;
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            _logger.Debug("OnNetworkError");
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
        {
            var opcode = reader.GetULong();

            if (_handlers.TryGetValue(opcode, out var handler))
            {
                handler.Invoke(peer.Id, reader);
            }
            else
            {
                _logger.Debug($"OnNetworkReceive: no packet handler. {TypeHash.Type(opcode)?.Name}");
            }
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            _logger.Debug("OnNetworkReceiveUnconnected");
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            //_logger.Debug("OnNetworkLatencyUpdate");
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            _logger.Debug("OnConnectionRequest");
        }
    }
}
