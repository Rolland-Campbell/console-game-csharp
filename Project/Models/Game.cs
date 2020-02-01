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
      IRoom Room0 = new Room("Outside", "You are standing outside of a long abandoned castle. \nThe once proud gates are rusted. One door lays on the ground. \nIt appears to have been riped from it's hinges. \n", false, false);
      IRoom Room1 = new Room("the Gatehouse", "The gate house is dark. Not much remains in this room. \nMost of the items have been scavenged or destroyed by time.\n", false, false);
      IRoom Room2 = new Room("the Courtyard", "The courtyard is overgrown with weeds and roots. \nSkeletal remains of what appear to have been livestock clutter the ground. \n", false, false);
      IRoom Room3 = new Room("the Main Keep Entry", "The main keep entry is full of broken furniture. \nIt appears that someone had tried to construct a barricade against something \n... big \n", false, false);
      IRoom Room4 = new Room("the Armory", "This room is also in a state of disrepair. \nBroken items litter the floor. \n", false, false);
      IRoom Death = new Room("Death Room", "death desc 1", false, true);
      IRoom Win = new Room("end room", "end desc 1", true, false);

      //Connecting Rooms
      Room0.AddExits("east", Room1);

      Room1.AddExits("west", Room0);
      Room1.AddExits("east", Room2);

      Room2.AddExits("east", Room3);
      Room2.AddExits("west", Room1);
      Room2.AddExits("south", Room4);

      Room3.AddExits("west", Room2);
      Room3.AddExits("north", Win);

      Room4.AddExits("north", Room2);

      Win.AddExits("south", Room3);

      //Add items
      Item Item1 = new Item("key", "an old rusty key");
      Item Item2 = new Item("sword", "a rusty sword");
      Item Item3 = new Item("shield", "a tarnished metal shield");

      //Item locations
      Room0.Items.Add(Item1);
      Room3.Items.Add(Item2);
      Room4.Items.Add(Item3);

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
      System.Console.WriteLine(@"" + "\n" + "After being thrown from your horse, you trudge through the swamp." + "\n" + "Eventually you come to an abandoned castle." + '\n' + "The once proud gates are rusted. One door lays on the ground." + "\n" + "It appears to have been riped from it's hinges." + "\n");
    }
  }
}