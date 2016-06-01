using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayUI : MonoBehaviour 
{
    public Text toxinText;
    public Text hungerText;
    public Image hungerbar;
    public Image toxinbar;

    private StatManager _statManager;

	void Start () 
	{
        _statManager = GameObject.Find("StatManager").GetComponent<StatManager>();
        _statManager.ToxinHalf();
        _statManager.HungerHalf();
	}
	
	void Update () 
	{
        int myToxin = (int)StatManager.toxin;
        toxinText.text = "toxin: " + myToxin.ToString();

        int myHunger = (int)StatManager.hunger;
        hungerText.text = "hunger: " + myHunger.ToString();
        //hunger and toxin bar
        hungerbar.fillAmount = StatManager.hunger / _statManager.maxHunger;
        toxinbar.fillAmount = StatManager.toxin / _statManager.maxToxin;
	}
}
