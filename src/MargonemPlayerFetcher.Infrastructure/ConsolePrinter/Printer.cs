using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Infrastructure.ConsolePrinter
{
    public class Printer : IPrinter
    {
        public void PrintServer(string server)
        {
            Console.WriteLine($"\nFetching now {server}");
        }

        public void PrintPlayerFetchStatus(int totalPlayers)
        {
            Console.WriteLine($"\tTotal: {totalPlayers}");
        }

        public void PrintException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Exception: {message}");
            Console.ResetColor();
        }

        public void PrintStart(string method)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Started: {method}");
            Console.ResetColor();
        }

        public void PrintNewItem(Item item)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\tNew Item: *{item.rarity}* {item.name}");
            Console.ResetColor();
        }

        public void PrintUpdatedItemFetchDate(Item item)
        {
            Console.WriteLine($"\tUpdated fetch date: {item.name}");
        }
        public void DrawProgress(int current, int total)
        {
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;

            Console.SetCursorPosition(Console.WindowWidth -11, 0);
            Console.Write($"{current}/{total}");

            Console.SetCursorPosition(originalLeft, originalTop);
        }

        public void PrintUnknownItemType(Item item)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\tUnknown Item type: *{item.rarity}* {item.name}");
            Console.ResetColor();
        }

        public void PrintDuplicatedItems(int duplicatedItemsCount)
        {
            Console.WriteLine($"Duplicated Items: {duplicatedItemsCount}");
        }

        public void PrintDuplicatedPlayers(int duplicatedPlayersCount)
        {
            Console.WriteLine($"Duplicated Players: {duplicatedPlayersCount}");
        }
    }
}
