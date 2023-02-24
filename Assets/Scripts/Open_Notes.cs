using UnityEngine;
using TMPro;

public class Open_Notes : MonoBehaviour
{

    #region Variables
    
    [SerializeField] public Local_Data DataBase;
    [SerializeField] TextMeshProUGUI Title_Text;
    string Title;

    #endregion



    #region Unity_Methods


    void Start()
    {
        DataBase = FindObjectOfType<Local_Data>();
    }


    void Update()
    {
        
    }
    

    void Awake()
    {
        
    }


    #endregion



    #region My_Methods


    public void Load_Note()
    {
        Title = Title_Text.text;
        DataBase.CreateDirectory();
        DataBase.LoadData();
    }


    [ContextMenu("Save")]
    void Load_Class()
    {
        DataBase.SaveData();
    }

    [ContextMenu("index")]
    void Note_Search()
    {
        //Busqueda del index de la nota
        DataBase.Title.IndexOf(Title);

        //extraccion de la fecha del momento
        //Date.Add(DateTime.Now.ToString());
    }



    #endregion

}
