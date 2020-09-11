using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScryfallConnector.Classes
{
    class Deck
    {
        public List<ScryfallCard> cards = new List<ScryfallCard>();
        private static Random rng = new Random();

        public List<string> cardNames 
        {
            get { 
                if (cards != null)
                        {
                    return cards.Select(o => o.Name).ToList();
                } else
                {
                    return new List<string>();
                }
                }
            private set {
                if (cards != null)
                {
                    cardNames = cards.Select(o => o.Name).ToList();
                }
                else
                {
                    cardNames = new List<string>();
                }
            }
        }

        public ScryfallCard commander { get; set; }

        public void ShuffleCards()
        {
            try
            {
                int n = this.cards.Count;
                for (int i = 0; i < 10; i++)
                {
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        ScryfallCard value = cards[k];
                        cards[k] = cards[n];
                        cards[n] = value;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
