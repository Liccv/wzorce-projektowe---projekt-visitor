using System;

namespace visitor
{
    interface IKomponent
    {
        void akceptuj(IVisitor visitor);
    }
    interface IVisitor
    {
        public void odwiedz(Kampania kampania);
    }
    class RaportVisitor : IVisitor
    {
        public void odwiedz(Kampania kampania)
        {
            Console.WriteLine();
            Console.Write("Dane o kampanii ");
            if (kampania is Influencer)
            {
                Console.Write($"Influencer ({((Influencer)kampania).nazwaKonta}):");
                Console.WriteLine();
            }
            else if (kampania is Native)
            {
                Console.Write($"Native ({((Native)kampania).liczbaBillboardow} billboardów):");
                Console.WriteLine();
            }
            else if (kampania is Internet)
            {
                Console.Write($"Internet ({((Internet)kampania).adresStrony} / {((Internet)kampania).liczbaReklam} reklam)");
                Console.WriteLine();
            }
            Console.WriteLine("Wydano " + kampania.kwotaWydana + "PLN, pozyskano " + kampania.iloscKlientow + " klientów");
        }
    }
    class WalidacjaVisitor : IVisitor
    {
        public void odwiedz(Kampania kampania)
        {
            if (kampania is Influencer)
            {
                if (((Influencer)kampania).nazwaKonta == "")
                {
                    Console.WriteLine("Dane niepoprawne");
                }
                else Console.WriteLine("Dane poprawne");
            }
            else if (kampania is Native)
            {
                if (((Native)kampania).liczbaBillboardow == 0)
                {
                    Console.WriteLine("Dane niepoprawne");
                }
                else Console.WriteLine("Dane poprawne");
            }
            else
            {
                if (((Internet)kampania).adresStrony == "" || ((Internet)kampania).liczbaReklam == 0)
                {
                    Console.WriteLine("Dane niepoprawne");
                }
                else Console.WriteLine("Dane poprawne");
            }
        }
    }
    class Kampania
    {
        public double kwotaWydana;
        public int iloscKlientow;

        public Kampania(double kwotaWydana, int iloscKlientow)
        {
            this.kwotaWydana = kwotaWydana;
            this.iloscKlientow = iloscKlientow;
        }
    }
    class Influencer : Kampania, IKomponent
    {
        public string nazwaKonta;

        public Influencer(double kwotaWydana, int iloscKlientow, string nazwaKonta) : base(kwotaWydana, iloscKlientow)
        {
            this.nazwaKonta = nazwaKonta;
        }
        public void akceptuj(IVisitor visitor)
        {
            visitor.odwiedz(this);
        }
    }
    class Native : Kampania, IKomponent
    {
        public int liczbaBillboardow;

        public Native(double kwotaWydana, int iloscKlientow, int liczbaBillboardow) : base(kwotaWydana, iloscKlientow)
        {
            this.liczbaBillboardow = liczbaBillboardow;
        }
        public void akceptuj(IVisitor visitor)
        {
            visitor.odwiedz(this);
        }
    }
    class Internet : Kampania, IKomponent
    {
        public string adresStrony;
        public int liczbaReklam;

        public Internet(double kwotaWydana, int iloscKlientow, string adresStrony, int liczbaReklam) : base(kwotaWydana, iloscKlientow)
        {
            this.adresStrony = adresStrony;
            this.liczbaReklam = liczbaReklam;
        }
        public void akceptuj(IVisitor visitor)
        {
            visitor.odwiedz(this);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Podaj rodzaj kampanii: (Influencer / Native / Internet)");
            string rodzajKampanii = Console.ReadLine();
            if (rodzajKampanii != "Influencer" && rodzajKampanii != "Native" && rodzajKampanii != "Internet")
            {
                Console.WriteLine("Taki rodzaj kampanii nie istnieje.");
                return;
            }

            Console.WriteLine("Podaj obecnie wydaną kwotę: ");
            double wydanaKwota = Double.Parse(Console.ReadLine());
            Console.WriteLine("Podaj ilość pozyskanych klientów: ");
            int iloscKlientow = Int32.Parse(Console.ReadLine());

            if (rodzajKampanii == "Influencer")
            {
                Console.WriteLine("Podaj nazwę konta: ");
                string nazwaKonta = Console.ReadLine();
                Influencer k = new Influencer(wydanaKwota, iloscKlientow, nazwaKonta);

                k.akceptuj(new WalidacjaVisitor());
                k.akceptuj(new RaportVisitor());
            }
            else if (rodzajKampanii == "Native")
            {
                Console.WriteLine("Podaj liczbę wykupionych billboardów: ");
                int liczbaBillboadrow = Int32.Parse(Console.ReadLine());
                Native k = new Native(wydanaKwota, iloscKlientow, liczbaBillboadrow);

                k.akceptuj(new WalidacjaVisitor());
                k.akceptuj(new RaportVisitor());
            }
            else if (rodzajKampanii == "Internet")
            {
                Console.WriteLine("Podaj adres url: ");
                string adresStrony = Console.ReadLine();
                Console.WriteLine("Podaj liczbę reklam na stronie: ");
                int liczbaReklam = Int32.Parse(Console.ReadLine());
                Internet k = new Internet(wydanaKwota, iloscKlientow, adresStrony, liczbaReklam);

                k.akceptuj(new WalidacjaVisitor());
                k.akceptuj(new RaportVisitor());
            }
        }
    }
}
