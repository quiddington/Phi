﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PhiClient.TransactionSystem
{
    /// <summary>
    /// Received by the client
    /// </summary>
    [Serializable]
    public class ConfirmTransactionPacket : Packet
    {
        [NonSerialized]
        public Transaction transaction;
        private int transactionId;
        public TransactionResponse response;

        // This is there so that this packet also works when sender == receiver
        // Otherwise, the user would receive the same packet and execute OnEndSender();
        // twice
        public bool toSender = false;

        public override void Apply(User user, RealmData realmData)
        {
            transaction.state = response;
            if (user == transaction.sender && toSender)
            {
                transaction.OnEndSender(realmData);
            }
            else if (user == transaction.receiver && !toSender)
            {
                transaction.OnEndReceiver(realmData);
            }
        }

        [OnSerializing]
        internal void OnSerializingCallback(StreamingContext c)
        {
            transactionId = transaction.id;
        }

        [OnDeserialized]
        internal void OnDeserializedCallback(StreamingContext c)
        {
            RealmData realmData = c.Context as RealmData;

            transaction = ID.Find(realmData.transactions, transactionId);
        }
    }
}
