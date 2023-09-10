using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MargoFetcher.Domain.Entities;

namespace MargoFetcher.Domain.Interfaces
{
    public interface IPrinter
    {
        void PrintServer(string server);
        void PrintPlayerFetchStatus(int totalPlayers);
        void PrintException(string message);
        void PrintStart(string method);
        void PrintNewItem(int lp, Item item);
        void PrintUpdatedItemFetchDate(int lp, Item item);
        void DrawProgress(int current, int total);
        void PrintUnknownItemType(Item item);
        void PrintDuplicatedItems(int duplicatedItemsCount);
        void PrintDuplicatedPlayers(int duplicatedPlayersCount);
    }
}
