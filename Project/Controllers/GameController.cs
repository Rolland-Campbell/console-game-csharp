using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();

    public void Run()
    {
      while (true)
      {
        GetUserInput();
      }
    }

    public void GetUserInput()
    {
      Console.WriteLine(@"What would you like to do?
[Type l to look around, type h for Help, q to Quit]
      ");
      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();
      switch (command)
      {
        case "i":
          _gameService.Inventory();
          Console.Clear();
          Print();
          break;
        case "h":
          _gameService.Help();
          Console.Clear();
          Print();
          break;
        case "go":
          _gameService.Go(option);
          Console.Clear();
          Print();
          break;
        case "l":
          _gameService.Look();
          Console.Clear();
          Print();
          break;
        case "take":
          _gameService.TakeItem(option);
          Console.Clear();
          Print();
          break;
        case "q":
          Console.Clear();
          Environment.Exit(0);
          break;
        case "use":
          _gameService.UseItem(option);
          Console.Clear();
          Print();
          break;
        case "r":
          _gameService.Reset();
          Console.Clear();
          break;
        default:
          Console.Clear();
          System.Console.WriteLine(@"
That is not a command 
Commands:
    i: Show your current inventory.
    l: Look around for a description of the current room.
    take (item name): Picks up item with that name.
    go (direction [North, East, West, South]): Moves you to the next room.
    use (item name): Use the item from your inventory.
    q: Quit the game.
    r: Restarts the game.
    
    ");
          break;
      }
    }

    private void Print()
    {
      System.Console.WriteLine(@"                                                    
      Welcome
      ");
      foreach (string message in _gameService.Messages)
      {
        Console.WriteLine(message);
      }
      _gameService.Messages.Clear();
    }

  }
}