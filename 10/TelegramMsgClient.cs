using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace _10
{
    class TelegramMsgClient
    {
        private MainWindow w;
        private static ITelegramBotClient bot;
        public static List<MessageLog> BotMessageLog;
        public TelegramMsgClient(MainWindow W, string tokenPath = "token.txt")
        {
            BotMessageLog = new List<MessageLog>();
            this.w = W;

            string token = System.IO.File.ReadAllText(tokenPath);
            bot = new TelegramBotClient(token);
            Console.WriteLine("запуск " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }
            };
            /*receive*/
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
                );
        }

        public static HashSet<string> diskFiles = new HashSet<string>();
        static Dictionary<string, string> translation = new Dictionary<string, string>()
        {
            {"а","a"},
            {"б","6"},
            {"в","B"},
            {"г","r"},
            {"д","d"},
            {"е","e"},
            {"ё","e"},
            {"ж","JIL"},
            {"з","3"},
            {"и","u"},
            {"й","u"},
            {"к","K"},
            {"л","JL"},
            {"м","M"},
            {"н","H"},
            {"о","o"},
            {"п","n"},
            {"р","p"},
            {"с","c"},
            {"т","T"},
            {"у","y"},
            {"ф","qp"},
            {"х","x"},
            {"ц","LL"},
            {"ч","4"},
            {"ш","LLI"},
            {"щ","LLL"},
            {"ъ","b"},
            {"ы","bl"},
            {"ь","b"},
            {"э","3"},
            {"ю","I0"},
            {"я","9"}
        };
        static string Translate(string input)
        {
            string output = string.Empty;
            foreach (var symbol in input)
            {
                if (translation.TryGetValue(symbol.ToString(), out string newSymbol))
                {
                    output += newSymbol;
                }
                else
                {
                    output += symbol;
                }
            }
            return output;
        }
        static async void DownLoad(string fileId, string path)
        {
            if (!System.IO.Directory.Exists("disk"))
            {
                System.IO.Directory.CreateDirectory("disk");
            }
            var file = await bot.GetFileAsync(fileId);
            FileStream fs = new FileStream("disk\\" + path, FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, fs);
            fs.Close();
            fs.Dispose();
        }
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            var message = update.Message;

            MessageLog messageLog = new MessageLog(DateTime.Now.ToString(), message.Chat.Id, message.Text, message.From.Username);
            BotMessageLog.Add(messageLog);

            #region формирование ответа
            if (message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "3dpaBcTByuTe)" +
                        "\n\nDJL9 uHuLLuaJlu3aLLuu ducka BBedute /disk" +
                        "\ni7pu 3arpy3ke HoBbLX qpauJLoB\no6HoBute duck c i7oMoLLLI0 /disk" +
                        "\n\nDJl9 cka4uBaHu9 c ducka BBedute uM9 qpauJla)" +
                        "\nHai7puMep: \"untitled.wav\"");
                    return;
                }
                else if (message.Text.ToLower() == "/disk")
                {
                    if (!System.IO.Directory.Exists("disk"))
                    {
                        System.IO.Directory.CreateDirectory("disk");
                    }
                    string output = "Ha ducke: ";
                    int counter = 1;
                    foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\disk"))
                    {
                        diskFiles.Add(file.Split('\\')[file.Split('\\').Length - 1]);
                        output += "\n" + counter + ". " + file.Split('\\')[file.Split('\\').Length - 1];
                        counter++;
                    }
                    await botClient.SendTextMessageAsync(message.Chat, output);
                }
                else if (diskFiles.Contains(message.Text))
                {
                    await botClient.SendTextMessageAsync(message.Chat, "BblcblJLaI0 qpauJL)");
                    using (var stream = System.IO.File.OpenRead("disk\\" + message.Text))
                    {
                        Telegram.Bot.Types.InputFiles.InputOnlineFile file = new Telegram.Bot.Types.InputFiles.InputOnlineFile(stream, message.Text);
                        await botClient.SendDocumentAsync(message.Chat, file);
                    }
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, Translate(message.Text.ToLower()));
                    return;
                }
            }
            else if (message.Type == Telegram.Bot.Types.Enums.MessageType.Document)
            {
                DownLoad(message.Document.FileId, message.Document.FileName);
                await botClient.SendTextMessageAsync(message.Chat, "doKyMeHt 3aKa4aH)");
                return;
            }
            else if (message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                DownLoad(message.Photo[message.Photo.Count() - 1].FileId,
                    message.Photo[message.Photo.Count() - 1].FileUniqueId + ".jpg");
                await botClient.SendTextMessageAsync(message.Chat, "qpoTka 3arpyJILeHa)");
                return;
            }
            else if (message.Type == Telegram.Bot.Types.Enums.MessageType.Audio)
            {
                DownLoad(message.Audio.FileId, message.Audio.FileName);
                await botClient.SendTextMessageAsync(message.Chat, "Ayduo donw`loded");
                return;
            }
            else if (message.Type == Telegram.Bot.Types.Enums.MessageType.Video)
            {
                DownLoad(message.Video.FileId, message.Video.FileName);
                await botClient.SendTextMessageAsync(message.Chat, "kJlaccHoe Budeo, cka4uBaHue...");
                return;
            }
            #endregion
        }
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }
        public void SendMessage(string Text, string Id)
        {
            long id = Convert.ToInt64(Id);
            bot.SendTextMessageAsync(id, Text);
        }
    }
}
