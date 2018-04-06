using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour {

    public GameObject pages; 
    public GameObject pagenum;
    public GameObject GameText;
    public GameObject AllMoneyText;
    public GameObject BuyButton;
    public GameObject SaleButton;
    public GameObject YesButton;
    public GameObject NoButton;

    public GameObject[] page = new GameObject[36];

    int pagei = 0;
    int spriteflg;
    int num;
    int w;
    int Allprice = 0;
    public int Max = 0;
    public int price = 0;
    int salenum = 0;

    public Entity_Sale saledate;

    public Sprite bone;
    public Sprite ant;
    public Sprite cat;
    public Sprite lion;
    public Sprite bean;

    string pagenumtext;
    bool endflg;
    bool SaleEnd;
    bool pick;
    bool last;
    public bool SaleState;

    // Use this for initialization
    void Start () {
        num = 1;
        w = 0;
        endflg = true;

        SaleState = false;

        SaleEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if((Input.GetMouseButton(0)) && (GameText == true))
        {
            GameText.SetActive(false);
        }
	}
    public void PushButtonStart()
    {
        SceneManager.LoadScene(1);
    }

    //左ボタンを押したときの処理
    public void PushButtonLeft()
    {
        pagenumtext = pagenum.GetComponent<Text>().text;        //ページの番号をpagenumtextに
        //1ページ目以外の時は左にページ変更
        if (pagenumtext != "1")
        {
            pages.transform.Translate(360, 0, 0);
            num = num - 1;
            pagenum.GetComponent<Text>().text = num.ToString();
        }
        
    }
    //右ボタンを押したときの処理
    public void PushButtonRight()
    {
        pagenumtext = pagenum.GetComponent<Text>().text;
        //3ページ目以外の時は右にページ変更
        if (pagenumtext != "3")
        {
            pages.transform.Translate(-360, 0, 0);
            num = num + 1;
            pagenum.GetComponent<Text>().text = num.ToString();
        }
    }
    //アイテムを手に入れる処理（）
    public void PushButtonBuy()
    {
        if(page[35].GetComponent<Image>().sprite == null)
        {
            spriteflg = Random.Range(1, 5);
            if(spriteflg == 1)
            {
                page[pagei].GetComponent<Image>().sprite = bone;
                page[pagei].GetComponentInChildren<Text>().text = "bone";
            }
            else if (spriteflg == 2)
            {
                page[pagei].GetComponent<Image>().sprite = ant;
                page[pagei].GetComponentInChildren<Text>().text = "ant";
            }
            else if (spriteflg == 3)
            {
                page[pagei].GetComponent<Image>().sprite = cat;
                page[pagei].GetComponentInChildren<Text>().text = "cat";
            }
            else if (spriteflg == 4)
            {
                page[pagei].GetComponent<Image>().sprite = lion;
                page[pagei].GetComponentInChildren<Text>().text = "lion";
            }
            else if (spriteflg == 5)
            {
                page[pagei].GetComponent<Image>().sprite = bean;
                page[pagei].GetComponentInChildren<Text>().text = "bean";
            }
            pagei = pagei + 1;
            if (pagei == 35)
            {
                endflg = false;
            }
        }
    }
    public void PushButtonSale()
    {
        GameText.SetActive(true);
        GameText.GetComponent<Text>().text = "売却するものを選択してください";
        BuyButton.SetActive(false);
        SaleButton.SetActive(false);
        YesButton.SetActive(true);
        NoButton.SetActive(true);

        SaleState = true;
    }
    
    public void PushButtonNo()
    {
        BuyButton.SetActive(true);
        SaleButton.SetActive(true);
        YesButton.SetActive(false);
        NoButton.SetActive(false);

        SaleState = false;
        SaleEnd   = false;
    }
    public void PushButtonSaleEnter()
    {
        if(SaleEnd == true)
        {
            Allprice = Allprice + price;
            GameText.SetActive(true);
            GameText.GetComponent<Text>().text = "売却値は" + price.ToString() + "です。";
            AllMoneyText.GetComponent<Text>().text = Allprice.ToString() + "G";
            price = 0;
            for(int z = 0; z < 36; z++)
            {
                pick = page[z].GetComponent<ItemButton>().Pick;
                if(pick == true)
                {
                    page[z].GetComponent<Image>().sprite = null;
                    page[z].GetComponentInChildren<Text>().text = "";
                    page[z].GetComponent<ItemButton>().TextnumDelete();
                }

             salenum = 0;
            }

            Max = 0;
            Form_a_line();
        }

    }

    public void SalePriceAdd(int AddPrice)
    {
        price = price + AddPrice;
        SaleEnd = true;

        Max = Max + 1;
    }

    public int SalenumAdd()
    {
        salenum = salenum + 1;
        return salenum;
    }

    void Form_a_line()
    {
        for(int k = 0; k < 36; k++)
        {
            last = true;
            if(page[k].GetComponent<Image>().sprite == null)
            {
                last = false;
                for (int v = k + 1; v < 36; v++)
                {
                    if(page[v].GetComponent<Image>().sprite != null)
                    {
                        page[k].GetComponent<Image>().sprite = page[v].GetComponent<Image>().sprite;
                        if(page[k].GetComponent<Image>().sprite == bone)
                        {
                            page[k].GetComponentInChildren<Text>().text = "bone";
                        }
                        else if (page[k].GetComponent<Image>().sprite == ant)
                        {
                            page[k].GetComponentInChildren<Text>().text = "ant";
                        }
                        else if (page[k].GetComponent<Image>().sprite == cat)
                        {
                            page[k].GetComponentInChildren<Text>().text = "cat";
                        }
                        else if (page[k].GetComponent<Image>().sprite == lion)
                        {
                            page[k].GetComponentInChildren<Text>().text = "lion";
                        }
                        else if (page[k].GetComponent<Image>().sprite == bean)
                        {
                            page[k].GetComponentInChildren<Text>().text = "bean";
                        }
                        page[v].GetComponent<Image>().sprite = null;
                        page[v].GetComponentInChildren<Text>().text = "";
                        k = k + 1;
                        last = true;
                    }
                }
                    pagei = k;
                    break;
            }
        }
    }
}
