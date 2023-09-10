using Common;
using MargoFetcher.Application.Equipment;
using MargoFetcher.Application.Equipment.Commands;
using MargoFetcher.Application.Jobs;
using MargoFetcher.Application.Jobs.Commands;
using MargoFetcher.Domain.Entities;
using MargoFetcher.Domain.Enums;
using MargoFetcher.Domain.Interfaces;
using MargoFetcher.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MargoFetcher.Presentation
{
    public class Presentation
    {
        private readonly IDispatcherService _dispatcherService;

        public Presentation(
            IDispatcherService dispatcherService)
        {
            _dispatcherService = dispatcherService;
        }

        public async Task FetchPlayers()
        {
            var random = new Random();

            while (true)
            {
                await _dispatcherService.DispatchPlayerSync();

                int secondsToWait = random.Next(10, 51);

                Console.WriteLine($"Wait: {60+secondsToWait}sec");

                await Task.Delay(TimeSpan.FromSeconds(60 + secondsToWait));
            }
        }


        public async Task FetchEquipment()
        {
            await _dispatcherService.DispatchEquipmentSync();
        }
        public async Task Run()
        {
            var success = false;
            var parsedChoice = 0;
            do
            {
                Console.WriteLine("1 => Fetch Players");
                Console.WriteLine("2 => Fetch Equipment");

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
                default:
                    Console.WriteLine("Brak :(");
                    break;
            }
        }
    }
}
