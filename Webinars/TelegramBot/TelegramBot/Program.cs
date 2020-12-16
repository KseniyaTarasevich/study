using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

//using Telegram.Bot.Types.InlineKeyboardButtons;

namespace TelegramBot
{
    class Program
    {
        static TelegramBotClient telegramBot;

        static void Main(string[] args)
        {
            telegramBot = new TelegramBotClient("1407721570:AAHQSA35rcWg68nxhpY7ON2IQ6_vSN-MsRA");

            telegramBot.OnMessage += BotOnMessageReceived;
            telegramBot.OnCallbackQuery += BotOnCallbackQueryReceived;

            var me = telegramBot.GetMeAsync().Result;


            Console.WriteLine(me.FirstName);

            telegramBot.StartReceiving();
            Console.ReadLine();
            telegramBot.StopReceiving();

        }

        private static async void BotOnCallbackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} pressed button {buttonText}");

            await telegramBot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"Вы нажали кнопку {buttonText}");
        }

        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != MessageType.Text) return;

            string name = $"{message.From.FirstName} {message.From.LastName}";

            Console.WriteLine($"{name} send message: '{message.Text}'");

            switch (message.Text)
            {
                case "/start":
                    string text =
@"Command list:
/start - запуск бота
/inline - вывод меню
/keyboard - вывод клавиатуры";
                    await telegramBot.SendTextMessageAsync(message.From.Id, text);
                    break;


                case "/inline":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                   {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("VK", "https://vk.com"),
                            InlineKeyboardButton.WithUrl("Telegram", "https://t.me")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Пункт 1"),
                            InlineKeyboardButton.WithCallbackData("Пункт 2"),

                        }
                    });
                    await telegramBot.SendTextMessageAsync(message.From.Id, "Выберите пункт меню", replyMarkup: inlineKeyboard);
                    break;
                case "/keyboard":
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                        new KeyboardButton("Hello"),
                        new KeyboardButton("How are you?")
                        },
                        new[]
                        {
                        new KeyboardButton("Contact") { RequestContact = true },
                        new KeyboardButton("Location") { RequestLocation = true }
                        }
                    });
                    await telegramBot.SendTextMessageAsync(message.Chat.Id, "Message", replyMarkup: replyKeyboard);
                    break;
                default:
                    break;
            }
        }
    }
}
