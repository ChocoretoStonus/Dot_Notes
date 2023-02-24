using System.Collections;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class Add_Notes : MonoBehaviour
{


    #region Variables

    [SerializeField] GameObject[] Notes;
    [SerializeField] GameObject Error_Notify;
    [SerializeField] Transform Parent;
    [SerializeField] bool IsPress, ErrorIsActive;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip[] Sounds;
    [SerializeField] int ActiveCounter;
    [SerializeField] int InactiveCounter;


    #endregion



    #region Unity_Methods


    private void Awake()
    {
        Error_Notify.SetActive(false);
    }


    private void Update()
    {
        if (IsPress && ErrorIsActive)
        {
            StopAllCoroutines();
            StartCoroutine("ActiveError");
            IsPress= false;
        }
    }


    #endregion



    #region My_Methods


    public void Create_Note()
    {
        ActiveCounter = default;
        InactiveCounter = default;
        StartCoroutine("ActivePress");
        IsPress= true;
        ListCheck(Parent);
    }


    void ListCheck(Transform Father)
    {
        for (int i = 0; i < Notes.Length; i++)
        {
            if (Notes[i].active == true)
            {
                ActiveCounter++;
            }
            else
            {
                InactiveCounter++;
            }
        }

        if (ActiveCounter < 12)
        {
            AddToList();
            ErrorIsActive = false;
        }
        else
        {
            Error_List();
            ErrorIsActive= true;
            Debug.Log("Ya no caben mas we");
        }

    }


    void AddToList()
    {
        for (int i = 0; i < Notes.Length; i++)
        {
            if (Notes[i].active == false)
            {
                Notes[i].SetActive(true);
                i = Notes.Length - 1;
                break;
            }
        }
        
        
        AS.clip = Sounds[0];
        AS.Play();
    }


    void Error_List()
    {
        AS.clip = Sounds[1];
        AS.Play();
        StartCoroutine("ActiveError");
    }


    #endregion



    #region Corrutines


    IEnumerator ActiveError()
    {
        Error_Notify.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        Error_Notify.SetActive(false);
        ErrorIsActive = false;
    }


    IEnumerator ActivePress()
    {
        IsPress = true;
        yield return new WaitForSeconds(0.5f);
        IsPress = false;
    }

    #endregion


}
