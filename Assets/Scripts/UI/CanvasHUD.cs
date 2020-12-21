using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject toolTip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowToolTip(Vector3 position, string name, string cost, string description) 
    {

        toolTip.SetActive(true);
        toolTip.transform.position = position;
        toolTip.GetComponentInChildren<Text>().text = name+"\n"+cost+"\n"+description;


    }

    public void HideToolTip()
    {
        toolTip.SetActive(false);
    }
}
