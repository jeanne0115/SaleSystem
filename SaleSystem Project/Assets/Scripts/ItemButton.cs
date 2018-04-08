using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemButton : MonoBehaviour {

    public Entity_Sale saledate;

    public GameObject GameManager;
    public GameObject Textnum;
    
    int salenum;
    int tennum;

    Text text;

    bool state;
    public bool pick;
    // Use this for initialization
    void Start () {
        pick = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //アイテム欄のButtonを押したときの処理
    public void PushButtonItem()
    {
        tennum = GameManager.GetComponent<GameManager>().max;
        state = GameManager.GetComponent<GameManager>().saleState;

        //売却状態かつ選択されていないかつ選択されているのは10個未満
        if ((state == true) && (pick == false)&&(tennum < 10))
        {
            for (int hoge = 1; hoge < 6; hoge++)
            {
                var sale = saledate.param.Where(date => date.id == hoge).FirstOrDefault();

                //Buttonの子についているTextとScripttableObject内のアイテム名が一致したときの処理
                if (GetComponentInChildren<Text>().text == sale.name)
                {
                    GameManager.GetComponent<GameManager>().Pick(Textnum);
                    GameManager.GetComponent<GameManager>().SalePriceAdd(sale.price);
                    salenum = GameManager.GetComponent<GameManager>().SalenumAdd();
                    Textnum.GetComponent<Text>().text = salenum.ToString();
                    pick = true;
                }
            }
        }

        //売却状態かつ選択されている
        else if((state == true)&&(pick == true))
        {
            GameManager.GetComponent<GameManager>().CansellPick(Textnum);
            for (int hoge = 1; hoge < 6; hoge++)
            {
                var sale = saledate.param.Where(date => date.id == hoge).FirstOrDefault();
                if (GetComponentInChildren<Text>().text == sale.name)
                {
                    GameManager.GetComponent<GameManager>().SalePriceTake(sale.price);
                }
            }
            pick = false;
        }
    }

    //Itemに表示されている数字をnullにする
    public void TextnumDelete()
    {
        Textnum.GetComponent<Text>().text = null;
        pick = false;
    }
}
