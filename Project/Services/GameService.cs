using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    private Game _game { get; set; }
    public List<string> Messages { get; set; }

    public GameService()
    {
      _game = new Game();
      Messages = new List<string>();
    }

    public void Go(string direction)
    {
      if (_game.CurrentRoom.Exits.ContainsKey(direction))
      {
        _game.CurrentRoom = _game.CurrentRoom.Exits[direction];
        Messages.Add($"You have moved to {_game.CurrentRoom.Name}");
        Messages.Add(_game.CurrentRoom.Description);

        if (_game.CurrentRoom.Dead == true)
        {
          Dead();
          return;
        }

        var exits = _game.CurrentRoom.Exits;
        string template = "";
        foreach (var e in exits)
        {
          template += $"{e.Key} \n";
        }
        Messages.Add("This room has the following exits: \n" + template);


        if (_game.CurrentRoom.Items.Count == 0)
        {
          Messages.Add("There are no items in the room \n");
          return;
        }
        var items = _game.CurrentRoom.Items;
        string templateItem = "The following items are in the room: \n";
        foreach (var i in items)
        {
          templateItem += $"{i.Name}: {i.Description} \n";
        }
        Messages.Add(templateItem);

      }
      else
      {
        Messages.Add("You cannot go that way");
        var exits = _game.CurrentRoom.Exits;
        string template = "This room has the following exits: \n";
        foreach (var e in exits)
        {
          template += $"{e.Key} \n";
        }
        Messages.Add(template);
      }
    }
    public void Help()
    {
      string help = @"
Commands:
    i: Show your current inventory.
    l: Look around for a description of the current room.
    take (item name): Picks up item with that name.
    go (direction [North, East, West, South]): Moves you to the next room.
    use (item name): Use the item from your inventory.
    q: Quit the game.
    r: Restarts the game.
      ";
      Messages.Add(help);
    }

    public void Inventory()
    {
      var items = _game.CurrentPlayer.Inventory;
      string template = "Your current inventory: \n";
      if (items.Count > 0)
      {
        foreach (var i in items)
        {
          template += $"{i.Name}: {i.Description} \n";
        }
      }
      else
      {
        Messages.Add("You have nothing in your inventory. \n");
      }
      Messages.Add(template);
    }

    public void Look()
    {
      Messages.Add($"{_game.CurrentRoom.Name} \n");
      Messages.Add($"{_game.CurrentRoom.Description} \n");
      var exits = _game.CurrentRoom.Exits;
      string template = "This room has the following exits: \n";
      foreach (var e in exits)
      {
        template += $"{e.Key} \n";
      }
      Messages.Add(template);

      if (_game.CurrentRoom.Items.Count == 0)
      {
        Messages.Add("There are no items in the room \n");
        return;
      }
      var items = _game.CurrentRoom.Items;
      string templateItem = "The following items are in the room: \n";
      foreach (var i in items)
      {
        templateItem += $"{i.Name}: {i.Description} \n";
      }
      Messages.Add(templateItem);
    }

    public void Dead()
    {
      Messages.Add(@"You have died, Press r to restart or q to quit
      ");
      _game.CurrentRoom.Exits.Clear();
      _game.CurrentPlayer.Inventory.Clear();
    }
    public void Quit()
    {
    }

    public void Reset()
    {
      _game.CurrentPlayer.Inventory.Clear();
      _game.CurrentRoom.Items.Clear();
      _game.Setup();
    }

    public void Win()
    {
    }
    public void Setup(string playerName)
    {
    }

    public void TakeItem(string itemName)
    {
      Item i = _game.CurrentRoom.Items.Find(item => item.Name == itemName); //searches room items for typed item. Compare typed name, with item names in room list.
      if (i == null)
      {
        Messages.Add("That item is not in this room \n"); //if typed name is incorrect
        return;
      }
      _game.CurrentPlayer.Inventory.Add(i); //add item to inventory
      Messages.Add($"You picked up a {i.Name} \n");
      _game.CurrentRoom.Items.Remove(i); //Remove from room list
    }

    public void UseItem(string itemName)
    {
      Item i = _game.CurrentPlayer.Inventory.Find(item => item.Name == itemName);
      if (i == null)
      {
        Messages.Add($"You do not seem to have a {itemName}. \n");
      }

      else if (_game.CurrentRoom.Trapped == true && itemName == "grumblecakes")
      {
        Messages.Add($"He is hit with { i.Description}.\n");
        Messages.Add("Not very original or great, or fun in any way...Oh well.. \n");
        _game.CurrentRoom.Description = "This room is empty, but also full of you winny-ness and glory and such...\n";
        Messages.Add("Press r to restart the game, or q to quit. \n");

      }
      else Messages.Add($"You use your {i}! It has no effect and falls to the floor... \n");
      _game.CurrentPlayer.Inventory.Remove(i);
      _game.CurrentRoom.Items.Add(i);
    }
  }
}