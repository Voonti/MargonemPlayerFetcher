using Common;
using MargoFetcher.Application.Equipment;
using MargoFetcher.Application.Equipment.Commands;
using MargoFetcher.Application.Jobs;
using MargoFetcher.Application.Jobs.Commands;
using MargoFetcher.Application.Players.Commands;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Enums;
using MargoFetcher.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MargoFetcher.Presentation
{
    public class Presentation
    {
        private readonly IMediator _mediator;

        public Presentation(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task FetchPlayers()
        {
            await _mediator.Send(new SyncPlayerCommand());
        }


        public async Task FetchEquipment()
        {
            await _mediator.Send(new SyncEquipmentCommand());
        }

        public async Task FetchCorruptedItems()
        {
            await _mediator.Send(new FetchCorruptedItemsCommand());
        }

        public async Task FetchCorruptedPlayers()
        {
            await _mediator.Send(new FetchCorruptedPlayersCommand());
        }

        public async Task Run()
        {
            var success = false;
            var parsedChoice = 0;
            do
            {
                Console.WriteLine("1 => Fetch Players");
                Console.WriteLine("2 => Fetch Equipment");
                Console.WriteLine("3 => Fetch Corrupted Items");
                Console.WriteLine("4 => Fetch Corrupted Players");

                var choice = Console.ReadLine();

                success = int.TryParse(choice, out parsedChoice);
            } while (!success);

            switch (parsedChoice) {
                case 1:
                    await FetchPlayers();
                    break;
                case 2:
                    await FetchEquipment();
                    break;
                case 3:
                    await FetchCorruptedItems();
                    break;
                case 4:
                    await FetchCorruptedPlayers();
                    break;
                default:
                    Console.WriteLine("Brak :(");
                    break;
            }
        }



    }
}
