using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {

    public IRoom CurrentRoom { get; set; }
    public IPlayer CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      //Creating Rooms
      IRoom Room0 = new Room("start", "You are at the front of a cave.", false, false);
      IRoom Room1 = new Room("hallway 1", "hallway desc 1", false, false);
      IRoom Room2 = new Room("hallway 2", "hallway desc 2", false, false);
      IRoom Death = new Room("Death Room", "death desc 1", false, true);
      IRoom Room3 = new Room("hallway 3", "hallway desc 3", false, false);
      IRoom Trogdor = new Room("end room", "end desc 1", true, false);

      //Connecting Rooms
      Room0.AddExits("east", Room1);

      Room1.AddExits("west", Room0);
      Room1.AddExits("east", Room2);

      Room2.AddExits("east", Room3);
      Room2.AddExits("west", Room1);
      Room2.AddExits("south", Death);

      Room3.AddExits("west", Room2);
      Room3.AddExits("north", Trogdor);

      Trogdor.AddExits("south", Room3);

      //Add items
      Item Item1 = new Item("key", "an ordinary key");
      Item Item2 = new Item("sword", "a rusty sword");

      //Item locations
      Room0.Items.Add(Item1);
      Room3.Items.Add(Item2);

      CurrentRoom = Room0;
    }

    public Game()
    {
      CurrentPlayer = new Player();
      GetTemplate();
      Setup();
    }

    public void GetTemplate()
    {
      System.Console.WriteLine(@"Welcome");
    }
  }
}