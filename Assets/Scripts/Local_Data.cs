using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Local_Data : MonoBehaviour
{

    #region Variables_Methods
    
    [Header("Personalization")]
    [Space(5)]
    
    [SerializeField] string FolderName;
    [SerializeField] string FileName;
    [SerializeField] string ExtensionName;
    [SerializeField] string FinalFileName;
    [SerializeField] bool PersistentDataPath;
    [SerializeField] bool Personalization;
    [HideInInspector] string Json;
    [HideInInspector] string FolderPath;
    [HideInInspector] string FilePath;
    [HideInInspector]string FileLoaded;
    [HideInInspector] Data FileDataBase;

    #endregion



    #region Variables_DB
    
    [Header("DataBase")]
    [Space(5)]

    public List<string> Title;
    public List<string> Content;
    public List<string> Date;

    #endregion



    #region UnityMethods


    void Awake()
    {
        Personalize_BDP();
        FileDataBase = new Data();
    }


    #endregion



    //Manage DataBase Methods
    #region MDB_Methods


    public void CreateDirectory()
    {

        if (File.Exists(FolderPath) && File.Exists(FilePath))
        {
            LoadData();
            
            Debug.Log("Json was been Load");
        }

        else if (File.Exists(FolderPath) && !File.Exists(FilePath))
        {
            Json = JsonUtility.ToJson(FileDataBase);
            File.WriteAllText(FilePath, Json);
            
            Debug.Log("Note created");
        }

        else if (!File.Exists(FolderPath) && !File.Exists(FilePath))
        {
            Directory.CreateDirectory(FolderPath);
            Json = JsonUtility.ToJson(FileDataBase);
            File.WriteAllText(FilePath, Json);

            Json = JsonUtility.ToJson(FileDataBase);
            File.WriteAllText(FilePath, Json);
            
            Debug.Log("Note & Folder created");
        }


    }


    public void LoadData()
    {
        //busca el archivo en la ruta que le pasamos
        FileLoaded = File.ReadAllText(FilePath);

        //y aqui le pasa los valores a la clase publica 
        FileDataBase = JsonUtility.FromJson<Data>(FileLoaded);

        //se llena la DataBase con la info del json para un manejo
        //en tiempo real de la informacion
        Fill_DB();
    }


    public void SaveData()
    {
        //realizo un vaciado de la DataBase a la clase
        //FileDataBase para poder hacer un guardado correcto
        Fill_FDB();

        //le pasamos los datos de la clase al Json
        Json = JsonUtility.ToJson(FileDataBase);

        //guarda los datos de la clase publica en el Archivo "Json" creado
        File.WriteAllText(FilePath, Json);
    }


    #region Fill_Methods
        
        //estos metodos estan orientados a el uso de listas pero
        //bien pueden ser orientados al uso de arrays, aunque por
        //uso mas practico recomiendo el uso de listas
        

        //Fill DataBase
        void Fill_DB()
        {
            //llena la clase que se llama Database que es basicamente
            //este script y se hace para poder hacer una carga de la informacion
            //para poderla modificar o leerla posteriormente
            if (FileDataBase.Title.Count >= 1)
            {
                Title.Clear();
                for (int i = 0; i < FileDataBase.Title.Count; i++)
                {
                    Title.Add(FileDataBase.Title[i]);
                }
                Debug.Log("Database Loaded");
            }
            else
            {
                Debug.LogWarning("The NoteFile is empty");
            }
        }


        //Fill File DataBase
        void Fill_FDB()
        {
            //llena la clase que se llama FileDataBase para poder
            //hacer un guardado posteriormente

            if (Title.Count >= 1)
            {
                FileDataBase.Title.Clear();
                for (int i = 0; i < Title.Count; i++)
                {
                    FileDataBase.Title.Add(Title[i]);
                }
                //FileDataBase.Title.Remove("No");
                Debug.Log("Database Loaded in the NoteFile");
            }
            else
            {
                Debug.LogWarning("The Database is empty");
            }
        }


    #endregion


    //Personalize DataBase Parameters
    void Personalize_BDP()
    {
        if (Personalization)
        {
            FolderName = $"/{FolderName}/";

            FinalFileName = $"{FileName}.{ExtensionName}";
            if (PersistentDataPath)
            {
                FolderPath = Application.persistentDataPath + FolderName;
                FilePath = FolderPath + FinalFileName;
            }
            else
            {
                FolderPath = Application.dataPath + FolderName;
                FilePath = FolderPath + FinalFileName;
                goto Exit;
            }
        }

        else
        {
            FolderName = "/Jsons/";
            ExtensionName= "json";
            FileName = "DataBase";
            FinalFileName = $"{FileName}.{ExtensionName}";
            FolderPath = Application.dataPath + FolderName;
            FilePath = FolderPath + FinalFileName;
            goto Exit;
        }
        Exit:;
    }
    

    #endregion

}

public class Data
{
    public List<string> Title;
    public List<string> Content;
    public List<string> Date;
}