using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Diagnostics;

namespace _10
{
    class MessageLog
    {
        public string Time { get; set; }

        public long Id { get; set; }

        public string Msg { get; set; }

        public string Username { get; set; }

        public MessageLog(string time, long id, string msg, string username)
        {
            Time = time;
            Id = id;
            Msg = msg;
            Username = username;
        }

        public static void SerializeMessageLog(HashSet<MessageLog> logs)
        {
            string fileName = "MessageLogs.json";
            string jsonString = JsonSerializer.Serialize(logs);
            File.WriteAllText(fileName, jsonString);
        }
        public static HashSet<MessageLog> DeserializeMessageLogs()
        {
            string fileName = "MessageLogs.json";
            HashSet<MessageLog> tempMessageLogs = new HashSet<MessageLog>();
            if (!File.Exists(fileName))
            {
                File.Create(fileName).Close();
                File.WriteAllText(fileName, "[]");
            }
            string jsonString = File.ReadAllText(fileName);
            tempMessageLogs = JsonSerializer.Deserialize<HashSet<MessageLog>>(jsonString);
            return tempMessageLogs;
        }

        public static void SerializeAllLogs(List<MessageLog> logs)
        {
            HashSet<MessageLog> oldLogs = DeserializeMessageLogs();
            //Debug.WriteLine("oldlogs" + oldLogs.Count);
            oldLogs.UnionWith(logs.ToHashSet<MessageLog>());
            //Debug.WriteLine("+union" + oldLogs.Count);
            SerializeMessageLog(oldLogs);
            TelegramMsgClient.BotMessageLog.Clear();
        }
    }
}
