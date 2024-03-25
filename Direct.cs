using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class Direct : MonoBehaviour
{
    public string directoryName = "MyData";
    public string fileName = "data.xml";

    private void Start()
    {
        // Debug log directory path
        Debug.Log("Directory path: " + Path.Combine(Application.persistentDataPath, directoryName));

        // Create directory if it doesn't exist
        string directoryPath = Path.Combine(Application.persistentDataPath, directoryName);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Debug.Log("Directory created: " + directoryPath);
        }
        else
        {
            Debug.Log("Directory already exists: " + directoryPath);
        }

        // Data to write in XML
        Person person = new Person
        {
            Name = "Tristan",
            BirthDate = "07/09/1903",
            FavoriteColor = "Red"
        };

        // Write data to XML file
        string filePath = Path.Combine(directoryPath, fileName);
        SerializeObjectToXml(person, filePath);
        Debug.Log("Data written to XML file: " + filePath);
    }

    public static void SerializeObjectToXml<T>(T data, string filePath)
    {
        using (var stream = File.OpenWrite(filePath))
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, data);
        }
    }

    public static T DeserializeObjectFromXml<T>(string filePath)
    {
        using (var stream = File.OpenRead(filePath))
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
    }

    [System.Serializable] // Mark Person class as serializable
    public class Person
    {
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string FavoriteColor { get; set; }
    }
}
