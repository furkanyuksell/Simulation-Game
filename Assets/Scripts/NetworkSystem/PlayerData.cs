using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
using Unity.Collections;

public struct PlayerData : IEquatable<PlayerData>, INetworkSerializable
{
    public ulong clientId;
    public FixedString64Bytes playerName;
    public FixedString64Bytes playerId;

    public bool Equals(PlayerData other)
    {
        return
            clientId == other.clientId &&
            playerName == other.playerName &&
            playerId == other.playerId;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref clientId);
        serializer.SerializeValue(ref playerName);
        serializer.SerializeValue(ref playerId);
    }
}