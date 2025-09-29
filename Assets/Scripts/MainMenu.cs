using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //These should all become their own classes I fear

    //This class will let users chose from any stories they have saved (Continue from last save), have the json file for and hopefully pick from a database
    //TODO Start a story
    //TODO save story progress
    //TODO convert json to story
    //TODO TODO QR codes, online databases, other advanced stuff.
    public class StartStory
    {
        //Player will pick a story
        private static string _story;
        
        //Buttons should have an emitter of some sort right?

    }

    //This is an option for the user to start making their own adventure, it should have an interface to write an entire story then save it locally and in the future, online
    //TODO Build your own story
    //TODO TODO Save to database
    public class BuildStory
    {
        
    }

    //This class is where the user can view all their own built stories and modify them.
    //Maybe useless and might jsut make it a feature of start and adventure
    //TODO list all local stories
    //TODO allow the user to modify them
    public class MyStories
    {
        
    }
    
}
