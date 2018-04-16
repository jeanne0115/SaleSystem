using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    public GameObject Pages; 
    public GameObject Pagenum;
    public GameObject GameText;
    public GameObject AllMoneyText;
    public GameObject How_to_useText;
    public GameObject PriceText;
    public GameObject BuyButton;
    public GameObject SaleButton;
    public GameObject YesButton;
    public GameObject NoButton;

    public GameObject[] Page = new GameObject[36];      //アイテム欄の配列
    public GameObject[] PickItem = new GameObject[10];  //選択されているアイテムを入れる配列

    int pagei = 0;
    int spriteflg;
    int num;
    int pickItemnum = 0;
    int allMoney = 0;
    public int max = 0;
    public int price = 0;
    int salenum = 0;

    public Entity_Sale saledate;

    public Sprite bone;
    public Sprite ant;
    public Sprite cat;
    public Sprite lion;
    public Sprite bean;

    string pagenumtext;
    bool saleEnd;
    bool pick;
    public bool saleState;

    // Use this for initialization
    void Start () {
        num = 1;

        saleState = false;

        saleEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
		//Textが表示されているときクリックで消す
        if((Input.GetMouseButton(0)) && (GameText == true))
        {
            GameText.SetActive(false);
        }
	}
    //TitleSceneからのシーン遷移
    public void PushButtonStart()
    {
        SceneManager.LoadScene(1);
    }

    //ButtonLeftを押したときの処理
    public void PushButtonLeft()
    {
        pagenumtext = Pagenum.GetComponent<Text>().text;        //ページの番号をpagenumtextに
        //1ページ目以外の時は左にページ変更
        if (pagenumtext != "1")
        {
            Pages.transform.Translate(360, 0, 0);
            num = num - 1;
            Pagenum.GetComponent<Text>().text = num.ToString();
        }
        
    }
    //ButtonRightを押したときの処理
    public void PushButtonRight()
    {
        pagenumtext = Pagenum.GetComponent<Text>().text;
        //3ページ目以外の時は右にページ変更
        if (pagenumtext != "3")
        {
            Pages.transform.Translate(-360, 0, 0);
            num = num + 1;
            Pagenum.GetComponent<Text>().text = num.ToString();
        }
    }
    //アイテムを手に入れる処理（）
    public void PushButtonBuy()
    {
        //アイテム欄の最後が空いてる場合
        if(Page[35].GetComponent<Image>().sprite == null)
        {
            //ランダムでButtonにアイテムの画像を入れる
            //子の非表示のTextにアイテム名を入れる
            spriteflg = Random.Range(1, 6);
            if(spriteflg == 1)
            {
                Page[pagei].GetComponent<Image>().sprite = bone;
                Page[pagei].GetComponentInChildren<Text>().text = "bone";
            }
            else if (spriteflg == 2)
            {
                Page[pagei].GetComponent<Image>().sprite = ant;
                Page[pagei].GetComponentInChildren<Text>().text = "ant";
            }
            else if (spriteflg == 3)
            {
                Page[pagei].GetComponent<Image>().sprite = cat;
                Page[pagei].GetComponentInChildren<Text>().text = "cat";
            }
            else if (spriteflg == 4)
            {
                Page[pagei].GetComponent<Image>().sprite = lion;
                Page[pagei].GetComponentInChildren<Text>().text = "lion";
            }
            else if (spriteflg == 5)
            {
                Page[pagei].GetComponent<Image>().sprite = bean;
                Page[pagei].GetComponentInChildren<Text>().text = "bean";
            }
            pagei = pagei + 1;
        }
    }
    //ButtonSaleを押したときの処理
    public void PushButtonSale()
    {
        GameText.SetActive(true);
        GameText.GetComponent<Text>().text = "売却するものを選択してください";
        BuyButton.SetActive(false);
        SaleButton.SetActive(false);
        YesButton.SetActive(true);
        NoButton.SetActive(true);
        How_to_useText.SetActive(false);
        PriceText.SetActive(true);
        
        saleState = true;
    }
    //ButtonNoを押したときの処理
    public void PushButtonCansell()
    {
        BuyButton.SetActive(true);
        SaleButton.SetActive(true);
        YesButton.SetActive(false);
        NoButton.SetActive(false);
        How_to_useText.SetActive(true);
        PriceText.SetActive(false);

        saleState = false;
        saleEnd   = false;
        price = 0;
        max = 0;
        salenum = 0;
        pickItemnum = 0;

        //選択されていたアイテムを選択解除する
        for(int u = 0; u < 10; u++)
        {
            if(PickItem[u] != null)
            {
                PickItem[u].GetComponent<Text>().text = null;
                PickItem[u] = null;
                
            } 
        }
        //すべてのButtonのPickをfalseにする
        for(int f = 0; f < 36; f++)
        {
            Page[f].GetComponent<ItemButton>().pick = false;
            Page[f].GetComponent<Image>().color = Color.white;
        }
    }

    //ButtonYesが押されたときの処理
    public void PushButtonSaleEnter()
    {
        if(saleEnd == true)
        {
            allMoney = allMoney + price;    //すべての売却値
            GameText.SetActive(true);
            GameText.GetComponent<Text>().text = "売却値は" + price.ToString() + "です。";
            AllMoneyText.GetComponent<Text>().text = allMoney.ToString() + "G";
            price = 0;
            for(int z = 0; z < 36; z++)
            {
                pick = Page[z].GetComponent<ItemButton>().pick;
                if(pick == true)
                {
                    //選択されているアイテムを消して空白にする
                    Page[z].GetComponent<Image>().sprite = null;
                    Page[z].GetComponentInChildren<Text>().text = null;
                    Page[z].GetComponent<ItemButton>().TextnumDelete();
                    Page[z].GetComponent<Image>().color = Color.white;
                }

             salenum = 0;
            }

            pickItemnum = 0;
            max = 0;
            Form_a_line();
            //選択されているPickItemの中身をnullにする
            for(int r = 0; r < 10; r++)
            {
                PickItem[r] = null;
            }
        }

    }

    //与えられた引数をpriceに足す
    public void SalePriceAdd(int AddPrice)
    {
        price = price + AddPrice;
        saleEnd = true;

        max = max + 1;
    }

    //与えられた引数をpriceから引く
    public void SalePriceTake(int TakePrice)
    {
        price = price - TakePrice;
        max = max - 1;
    }

    //選択されたアイテムに入れる番号を返す
    public int SalenumAdd()
    {
        salenum = salenum + 1;
        return salenum;
    }

    //アイテム欄の空白を詰める処理
    void Form_a_line()
    {
        for(int k = 0; k < 36; k++)
        {
            if(Page[k].GetComponent<Image>().sprite == null)
            {
                for (int v = k + 1; v < 36; v++)
                {
                    if(Page[v].GetComponent<Image>().sprite != null)
                    {
                        Page[k].GetComponent<Image>().sprite = Page[v].GetComponent<Image>().sprite;
                        if(Page[k].GetComponent<Image>().sprite == bone)
                        {
                            Page[k].GetComponentInChildren<Text>().text = "bone";
                        }
                        else if (Page[k].GetComponent<Image>().sprite == ant)
                        {
                            Page[k].GetComponentInChildren<Text>().text = "ant";
                        }
                        else if (Page[k].GetComponent<Image>().sprite == cat)
                        {
                            Page[k].GetComponentInChildren<Text>().text = "cat";
                        }
                        else if (Page[k].GetComponent<Image>().sprite == lion)
                        {
                            Page[k].GetComponentInChildren<Text>().text = "lion";
                        }
                        else if (Page[k].GetComponent<Image>().sprite == bean)
                        {
                            Page[k].GetComponentInChildren<Text>().text = "bean";
                        }
                        Page[v].GetComponent<Image>().sprite = null;
                        Page[v].GetComponentInChildren<Text>().text = "";
                        k = k + 1;
                    }
                }
                    pagei = k;
                    break;
            }
        }
    }

    //選択されているアイテムを配列に入れる処理
    public void Pick(GameObject TenItem)
    {
        PickItem[pickItemnum] = TenItem;
        pickItemnum = pickItemnum + 1;
    }

    //選択されているアイテムを非選択状態に戻す処理
    public void CansellPick(GameObject ReduceItem)
    {
        salenum = salenum - 1;
        pickItemnum = pickItemnum - 1;
        for(int e = 0; e < 10; e++)
        {
            //PickItemの中身と引数のobjectが一致したとき
            if(PickItem[e] == ReduceItem)
            {
                int l = e + 1;
                PickItem[e].GetComponent<Text>().text = null;   //中身をnullにする
                PickItem[e] = null;
                for (int q = e; q < 9; q++)     //配列の空白を詰め表示されている数字を変える
                {
                    if (PickItem[q + 1] != null)
                    {
                        PickItem[q] = PickItem[q + 1];
                        PickItem[q + 1] = null;
                        PickItem[q].GetComponent<Text>().text = l.ToString();
                        l = l + 1;
                        if (e == 0)
                        {
                            PickItem[e].GetComponent<Text>().text = "1";
                        }
                    }
                }
            }
        }
    }
}
