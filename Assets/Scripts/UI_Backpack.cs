using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Backpack : MonoBehaviour
{

    private Dictionary<int, Card> deck;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBackpack(Deck backpack)
    {
        this.deck = Deck.GetInstance().GetCardsList(); 

        

        RefreshUI();
    }


    private void OnEnable()
    {
        Deck.GetInstance().OnDeckChanged += Backpack_OnBackpackChanged;


        RefreshUI();
    }

    private void OnDisable()
    {
        Deck.GetInstance().OnDeckChanged -= Backpack_OnBackpackChanged;

    }

    private void Backpack_OnBackpackChanged(object sender, System.EventArgs e)
    {
        Debug.Log("Backpack changed, update UI accordingly.");
        // Update the UI to reflect changes in the backpack
        RefreshUI();
        
    }

    private void RefreshUI()
    {
        
        // Clear existing UI elements
        // Iterate through backpack.GetCardsList() and create UI elements for each card
    }
}
