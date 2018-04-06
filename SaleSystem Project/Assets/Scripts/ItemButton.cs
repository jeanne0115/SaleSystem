using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemButton : MonoBehaviour {

    public Entity_Sale saledate;

    public GameObject GameManager;
    public GameObject Textnum;

    int num;
    int Salenum;
    int tennum;

    Text text;

    bool State;
    public bool Pick;
    // Use this for initialization
    void Start () {
        num = 0;
        Pick = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void PushButtonItem()
    {
        tennum = GameManager.GetComponent<GameManager>().Max;
        State = GameManager.GetComponent<GameManager>().SaleState;
        if ((State == true) && (Pick == false)&&(tennum < 10))
        {
            for (int hoge = 1; hoge < 6; hoge++)
            {
                var sale = saledate.param.Where(date => date.id == hoge).FirstOrDefault();
                if (GetComponentInChildren<Text>().text == sale.name)
                {
                    GameManager.GetComponent<GameManager>().SalePriceAdd(sale.price);
                    Salenum = GameManager.GetComponent<GameManager>().SalenumAdd();
                    Textnum.GetComponent<Text>().text = Salenum.ToString();
                    Pick = true;
                }
            }
        }
        if((State == true)&&(Pick == true))
        {

        }
    }
    public void TextnumDelete()
    {
        Textnum.GetComponent<Text>().text = "";
        Pick = false;
    }
}
