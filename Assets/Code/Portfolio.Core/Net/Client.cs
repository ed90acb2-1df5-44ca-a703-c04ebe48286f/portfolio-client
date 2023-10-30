using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Google.Protobuf;
using LiteNetLib;
using LiteNetLib.Utils;
using Portfolio.Protocol;

namespace Portfolio.Core.Net
{
    public class Client : INetEventListener, IClient
    {
        private readonly Settings _settings;
        private readonly ILogger _logger;

        private readonly Dictionary<Opcode, Action<int, NetDataReader>> _handlers = new();
        private readonly BufferWriter _buffer = new();
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

        public void RegisterHandler<TMessage>(Action<TMessage> handler) where TMessage : class, IMessage, new()
        {
            var packet = new TMessage();

            _handlers[Opcodes.Get<TMessage>()] = (peer, reader) =>
            {
                _logger.Debug($"Processing Packet: {typeof(TMessage).Name}");

                packet.MergeFrom(reader.GetRemainingBytes());

                handler.Invoke(packet);
            };
        }

        public void Send<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            if (_peer == null)
            {
                _logger.Debug("Can't send message. Not connected!");
                return;
            }

            _buffer.Reset();
            _buffer.Write((uint) Opcodes.Get<TMessage>());

            message.WriteTo(_buffer);

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
            _logger.Debug("OnNetworkReceive");

            var opcode = (Opcode) reader.GetUInt();

            if (_handlers.TryGetValue(opcode, out var handler))
            {
                handler.Invoke(peer.Id, reader);
            }
            else
            {
                _logger.Debug($"OnNetworkReceive: no packet handler. Opcode: {opcode.ToString()}");
            }
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            _logger.Debug("OnNetworkReceiveUnconnected");
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            _logger.Debug("OnNetworkLatencyUpdate");
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            _logger.Debug("OnConnectionRequest");
        }
    }
}
