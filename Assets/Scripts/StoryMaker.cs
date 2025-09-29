using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using File = System.IO.File;

public class StoryMaker : MonoBehaviour
{
    
    
    //New story function will ask the user to fill in a story and then will serialize it in a json.
    //TODO User interaction
    public static string NewStory()
    {
        //For now ill just make a whole new story and see if it works, later I want to be able to have a story maker in the game.
        //First we make a Choices obj.
        //We need to be able to put in as many choices as we wish
        string DescriptionChoice;
        // The id of the thumbnail this choice send
        string ThumbnailLinkId;
        // The list of item need to be able to select this choice
        string[] NeededItemsId;
        // The list of item given when selecting this choice
        string[] GivenItemsId;
        // The list of item remove form the inventory when selecting this choice
        string[] TakenItemsId;
        Choice choice = new Choice(
            "Description of a choice", 
            "001", 
            new List<string>{"item1", "item2"}, 
            new List<string> { "itemA", "itemB" },
            new List<string> { "item1", "item2" });
        
        //Seccond a Thumbnail USING that choice made
        //Each choice corresponds to a thumbnail
        string ThumbainId;
        // The name of the image of the thumbnail inside the folder with the extension
        // For example : "toto.png"
        string ImageNameThumbnail;
        // The name of the sfx of the thumbnail inside the folder with the extension
        // For example : "toto.flac"
        string SfxName;
        // The name of the music of the thumbnail inside the folder with the extension
        // For example : "toto.ogg"
        string MusicName;
        // The description of the thumbnail, scenery description
        string DescriptionThumbnail;
        // The list of the choices for this thumbnail
        List<Choice> Choices;

        Thumbnail thumbnail = new Thumbnail(
            "001",
            "Image name thumbnail",
            "SFX name thumbnail",
            "Music name thumbnail",
            "This is a thumbnail description",
            new List<Choice> { choice }
        );
        
        //There are also Items that the play can use and find
        // The Id of the item for reference
        string ItemId;
        // The name of the item (for label)
        string ItemName;
        // The name of the icon representative of the item in the story folder (example : sword.png)
        string IconName;
        // The description of the item (for label)
        string Description;
        Item item = new Item(
            "001",
            "Item Name",
            "Icon 1",
            "This is a item description"
            );
        

        //Third the Story is made with BOTH the previous thumbnail and Choice
        string StoryName;
        // The name of the image representative of the story (banner-like)
        string ImageNameStory;
        // The starting id of the first thumbnail
        string StartingThumbnailId;
        // The list containing all thumbnail of the story
        List<Thumbnail> Thumbnails;
        // The list containing all items of the story
        List<Progress.Item> Items;

        Story story = new Story(
            "First story",
            "Story image",
            "001",
            new List<Thumbnail> { thumbnail },
            new List<Item> {item}
            );

        var SerializedStory = JsonUtility.ToJson(story);
        SaveStory(SerializedStory, "First story");
        return SerializedStory;
    }
    
    //DONE saves to a json file 
    public static void SaveStory(string story, string title)
    {
        //In comes the json string
        //and out should go a json file to Application.persistentDataPath.
        //The file should be named after its title
        //TODO maybe sanitize and  limit titles for application names?

        try
        {
            Debug.Log(Application.persistentDataPath + "/" + title + ".json");
            //System.IO.File.WriteAllText(Application.persistentDataPath + "/" + title + ".json", story);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    void Awake()
    {
        //NewStory();
        Story story = Deserialize.ReadStory("First story");
        //SetupStory(story);
    }
}

/*
 *
Barème de notation du TP – "L’histoire dont vous êtes le héros"

Projet Unity mobile : lecture interactive avec gestion d’inventaire et sérialisation des données.
Structure de données imposée (Story, Thumbnail, Choice, Item).

Total : 20 points

1. Chargement et sélection des histoires (5 pts)
- Affichage d’une liste d’histoires (titre + image si possible)
- Possibilité de sélectionner une histoire et la lancer
DONE Chargement des données depuis un fichier local
- Bonus si l’histoire est récupérée dynamiquement (Internet, dépôt distant…)
- Bonne gestion des erreurs : fichier absent, image manquante, JSON mal formé, etc.

2. Intégration de la structure fournie (4 pts)
DONE Lecture correcte du JSON (Story avec ses Thumbnails, Choices, etc.)
- Navigation dynamique entre les scènes de l’histoire (Thumbnail.Id et Choice.ThumbnailLinkId)
- Utilisation correcte des champs dans Choice (naviguer vers une autre vignette, utiliser ou donner des items)
- Utilisation des champs médias : images, musiques, sons (si présents)

3. Lecture interactive et logique de jeu (5 pts)
- Texte de la scène bien affiché (Thumbnail.Description)
- Affichage des choix disponibles, navigation fluide entre les scènes
- Gestion complète de l’inventaire :
   - Les choix peuvent être désactivés ou cachés si le joueur ne possède pas les objets requis (NeededItemsId)
   - Les objets sont bien donnés (GivenItemsId) ou retirés (TakenItemsId) en fonction des choix
- L’histoire se termine clairement quand il n’y a plus de choix

4. Sauvegarde et reprise (4 pts)
- Possibilité de sauvegarder l’état du jeu : vignette actuelle + inventaire
- Reprise correcte à partir d’une sauvegarde
- Format propre (JSON lisible par exemple)
- Bouton ou système de reprise automatique proposé à l’utilisateur

5. Interface mobile et input (2 pts)
- Utilisation du nouvel Input System Unity
- UI adaptée au mobile : responsive, lisible, claire, utilisable au tactile

6. Qualité du code et présentation (1 pt)
- Code organisé, lisible, avec des fichiers séparés pour les rôles importants (chargement, logique, UI…)
- Expérience utilisateur soignée : transitions, effets visuels, sons, feedback sur les actions

Bonus (jusqu’à +2 pts)
- Sauvegardes multiples, filtres sur les histoires, animations, effets audio soignés, système de téléchargement en ligne, etc.
 * 
 */