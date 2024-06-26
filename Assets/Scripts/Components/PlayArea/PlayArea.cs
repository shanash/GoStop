using UnityEngine;

public class PlayArea
{
    PlayAreaModel model;
    PlayAreaView view;

    public PlayArea(PlayAreaView view)
    {
        model = new PlayAreaModel();
        this.view = view;
    }

    public void DisplayCards(int number)
    {
        for (int i = 0; i < number; i++)
        {
            var card = GameManager.I.Deck.Pop();
            model.Add(card);
        }
        view.UpdateView(model);
    }
}
