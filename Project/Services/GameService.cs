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
        Messages.Add($"You have moved to {_game.CurrentRoom.Name} \n");
        Messages.Add(_game.CurrentRoom.Description);

        var exits = _game.CurrentRoom.Exits;
        string template = "";
        foreach (var e in exits)
        {
          template += $"{e.Key} \n";
        }
        Messages.Add("From here you can go: \n" + template);
      }
      else
      {
        Messages.Add("You cannot go that way");
        var exits = _game.CurrentRoom.Exits;
        string template = "From here you can go: \n";
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
    s: search the current room.
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
      string template = "From here you can go: \n";
      foreach (var e in exits)
      {
        template += $"{e.Key} \n";
      }
      Messages.Add(template);

      if (_game.CurrentRoom.Items.Count == 0)
      {
        Messages.Add("You search around but find nothing. \n");

        return;
      }
      var items = _game.CurrentRoom.Items;
      string templateItem = "You search around and find the following items: \n";
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
      _game.CurrentRoom.Items.Clear();
    }
    public void Quit()
    {
    }

    public static void Restart();
    // public void Reset()
    // {
    //   Environment.Restarts();
    //   _game.CurrentPlayer.Inventory.Clear();
    //   _game.CurrentRoom.Items.Clear();
    //   _game.Setup();
    // }

    public void Win()
    {
      Messages.Add($"\nYou have defeated the giant, and claimed the treasures of the keep! \n\nHuzzah!!!");
      Messages.Add($"\nPress r to restart, or q to quit \n");
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

      else if (_game.CurrentRoom.Trapped == true && itemName == "key")
      {
        Messages.Add($"You use the { i.Name}.\n");
        Messages.Add("You hear a series of click and the sound of tumblers rolling. \nA hidden door slides open to the south! \n");
        var exits = _game.CurrentRoom.Exits;
        // var locked = _game.CurrentRoom.Locked;
        string template = "From here you can go: \n";
        foreach (var e in exits)
        {
          template += $"{e.Key} \n";
        }
        Messages.Add(template);
      }

      else if (_game.CurrentRoom.Dead == true && itemName == "sword")
      {
        Messages.Add($"You swing your { i.Name } at the giant! \nThe blade strikes him with an explosion of light! \nAs your eyes adjust from the blinding light, you see that your sword has broken!  \nThe giant laughs at you, and says 'I see you have found the cursed blade!, \nI spent my life looking for it. \nIt's power unlocks immortality to the victim of it's bite! \nFool, you have given me the ability to make this realm my own! \nand now you shall die!' \n");
        _game.CurrentRoom.Items.Clear();
        Dead();
      }

      else if (_game.CurrentRoom.Dead == true && itemName == "shield")
      {
        Messages.Add($"The tarnished shield begins glowing with a red light! \nThe light builds with intensity, then fires a ray of fire into the undead giant! \nThe giant screams 'NOOOOOOO!!!! and is vapoized!");
        Win();
      }


      else Messages.Add($"You use your {i.Name}! It has no effect and falls to the floor... \n");
      _game.CurrentPlayer.Inventory.Remove(i);
      _game.CurrentRoom.Items.Add(i);
    }
  }
}