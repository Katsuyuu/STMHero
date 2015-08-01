using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace STMHero.Model
{
   public static class FileManager
    {

       private static XmlSerializer serializer;
       #region Load
       public static SongManager LoadSong(string fileName)
        {

            try
            {
                using (var fileToLoad = new FileStream(fileName, FileMode.Open))
                {
                    serializer = new XmlSerializer(typeof(SongManager));
                    return (SongManager)serializer.Deserialize(fileToLoad);
                }
            }
            catch
            {
                return null;
            }
        }

       public static TotalRecords LoadResults(string fileName)
       {
           try
           {
               using (var fileToLoad = new FileStream(fileName, FileMode.Open))
               {
                   XmlSerializer serializer = new XmlSerializer(typeof(TotalRecords));
                   return (TotalRecords)serializer.Deserialize(fileToLoad);
               }
           }
           catch
           {
               return null;
           }
       }
       #endregion
       #region Save
       public static void SaveSong(string SongName, string SongDescription, List<Button> GameButtons)
        {
            Save(SongName + ".xml", new SongManager()
            {
                desription = SongDescription,
                name = SongName,
                songComposition = GameButtons
            });

        }
        private static void Save(string fileName, SongManager songManager)
        {
            using (var fileToSave = new FileStream(fileName, FileMode.Create))
            {

                XmlSerializer serializer = new XmlSerializer(typeof(SongManager));
                serializer.Serialize(fileToSave, songManager);
            }

        }
        public static void SaveResults(string fileName, TotalRecords totalRecords)
       {

           using (var fileToSave = new FileStream(fileName, FileMode.Create))
           {

               serializer = new XmlSerializer(typeof(TotalRecords));
               serializer.Serialize(fileToSave, totalRecords);
           }

       }
       #endregion
    }
}
