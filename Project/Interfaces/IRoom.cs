using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
    Dictionary<string, IRoom> Exits { get; set; }
    Dictionary<string, IRoom> Locked { get; set; }

    bool Trapped { get; set; }
    bool Dead { get; set; }

    void AddExits(string direction, IRoom room);

    void AddLocked(string direction, IRoom room);
  }
}