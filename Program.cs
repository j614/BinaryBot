using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using TeleSharp.TL.Messages;
using TLSharp;
using TLSharp.Core;
using TeleSharp.TL;
using TeleSharp.TL.Updates;
using TeleSharp.TL.Channels;
using System.Linq;

namespace TestBinaryBot
{
    class Program
    {
        
        public static void WriteLog(string text)
        {
            DateTime timeNow = DateTime.Now; 
            try
            {
                using (StreamWriter sw = new StreamWriter("log.txt", true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("\nсделка +{0}\n", timeNow.ToString());
                    sw.WriteLine(text);
                }

                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async Task RequestOpenContract(BinaryWS bws)
        {
            string openContract = "{\"proposal_open_contract\": 1,\"contract_id\":126049182028,\"subscribe\": 1}";
            bws.SendRequest(openContract).Wait();
            string s = await bws.StartListen();
            Console.WriteLine(s);
            Root myClass = JsonSerializer.Deserialize<Root>(s);
            Console.WriteLine("\nДоход сделки:"+myClass.proposal_open_contract.profit.ToString());

        }
        public static async Task BuyContract(BinaryWS bws)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            Contract contr = new Contract();
            contr.Amount = 1;
            contr.Basis = "payout";
            contr.Contract_type = "CALL";
            contr.Currency = "USD";
            contr.Duration = 3;
            contr.Duration_unit = "m";
            contr.Symbol = "frxAUDUSD";

            Deal deal = new Deal();
            deal.Buy = 1;
            deal.Price = 5;
            deal.Parameters = contr;
            string jsonString = JsonSerializer.Serialize(deal, options);
            bws.SendRequest(jsonString).Wait();
            //string jsonAnswer = await bws.StartListen();
            //String substring = jsonAnswer.Substring(63, 12);
           // WriteLog(substring);
        }
        public static async Task Autharization(BinaryWS bws)
        {
            string auth = "{\"authorize\" : \"TiW6LJQp2qYWeHb\"}";
            bws.SendRequest(auth).Wait();
            string s = await bws.StartListen();
            Console.WriteLine(s);

        }

        public static async Task ListenTelega(TelegramClient telegram)
        {
            Console.WriteLine("Cлушаем...");
            while (true)
            {
                var dialogs = await telegram.GetUserDialogsAsync() as TLDialogs;
                foreach (TLDialog dialog in dialogs.Dialogs)
                {
                    int unreadMess = dialog.UnreadCount;
                    TLPeerUser u = (TLPeerUser)dialog.Peer;
                    var user = dialogs.Users
                      .Where(x => x.GetType() == typeof(TLUser))
                      .Cast<TLUser>()
                      .FirstOrDefault(x => x.Id == u.UserId);

                    Console.WriteLine(user.ToString());
                }
                await Task.Delay(500);
            }
        }

        public static async Task TelegramConnect()
        {
            int epoch = Convert.ToInt32((DateTime.Now.AddHours(-15) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
            TelegramClient client = new TelegramClient(6874445, "5a5c8e390774a1445e1defdf4a518b82");
            //TelegramClient client = new TelegramClient(6406376, "b1e52459f756128a16095799ea58745d");
            await client.ConnectAsync();
            var hash = await client.SendCodeRequestAsync("89632763905");
            Console.WriteLine("Код:");
            
            var code = Console.ReadLine();
            //var code = "15797";
            var user = await client.MakeAuthAsync("89632763905", hash, code);
            var dialogs = (TLDialogs)await client.GetUserDialogsAsync();
            ///var chat = dialogs.Chats.Where(c => c.GetType() == typeof(TLChannel)).Cast<TLChannel>().FirstOrDefault(c => c.Title == "Pocket Option Official Signal Bot");
            var chat = dialogs.Users.Where(c => c.GetType() == typeof(TLUser)).Cast<TLUser>().FirstOrDefault(c => c.Username == "PocketSignalBot");
            //var chat = dialogs.Users.GetList.OfType(TLUser).First(x => x.id == peer.UserId);
            var target = new TLInputPeerUser(){UserId = chat.Id, AccessHash = (long)chat.AccessHash};
            var hist = await client.GetHistoryAsync(target, 0, -1, 10);
            await ListenTelega(client);
            //TLInputPeerChannel inputPeer = new TLInputPeerChannel()
            // { ChannelId = chat.Id, AccessHash = (long)chat.AccessHash };

         /*   TLChannelMessages res = await client.SendRequestAsync<TLChannelMessages>(new TLRequestGetHistory()
            {

                    Peer = inputPeer,
                    Limit = 12,
                    AddOffset = 1,
                    OffsetId = 0
                });*/
           // var msgs = res.Messages;
           // var m = msgs.Last();
            Console.WriteLine();

        }


        public static Contract FormationNewContract(string message)
        {
            string[] subs = message.Split(' ');
            subs[0] = "frx" + subs[0];
            Contract contract = new Contract();
            contract.Amount = 1;
            contract.Basis = "payout";
            contract.Currency = "USD";
            contract.Duration = Convert.ToInt32(subs[1]);
            contract.Symbol = subs[0];
            contract.Duration_unit = "m";
            if (subs[3] == "ВНИЗ")
                contract.Contract_type = "PUT";
            else
                contract.Contract_type = "CALL";
            return contract;


        }
        static void Main(string[] args)
        {
            //string s = "EURAUD 15 минут ВВЕРХ";
            //FormationNewContract(s);


            TelegramConnect().Wait();
            //var bws = new BinaryWS();

            //bws.Connect().Wait();
            //Autharization(bws).Wait();
            //RequestOpenContract(bws).Wait();
            //Console.ReadLine();
        }
        
    }
}
