using System;

namespace _01_01
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Campainha campainha = new Campainha();
                campainha.OnCompanhiaTocou += CampanhiaTocou1;
                campainha.OnCompanhiaTocou += CampanhiaTocou2;
                Console.WriteLine("A campanhia será tocada.");
                campainha.Tocar("101");

                campainha.OnCompanhiaTocou -= CampanhiaTocou1;
                Console.WriteLine("Será tocada de novo.");
                campainha.Tocar("102");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            //campainha.OnCompanhiaTocou(null, new EventArgs());




            Console.ReadKey();
        }

        static void CampanhiaTocou1(object sender, CampanhiaEventArgs args)
        {
            Console.WriteLine("A campanhia tocou no AP "+args.Apartamento+" (1)");
            throw new Exception("Ocorreu um erro em campanhiaTocou1");
        }
        static void CampanhiaTocou2(object sender, CampanhiaEventArgs args)
        {
            Console.WriteLine("A campanhia tocou no AP " +args.Apartamento+" (2)");
            throw new Exception("Ocorreu um erro em campanhiaTocou2");
        }
    }

    class Campainha
    {
        public event EventHandler<CampanhiaEventArgs> OnCompanhiaTocou;
        public void Tocar(string apartamento)
        {
            foreach (var manipulador in OnCompanhiaTocou.GetInvocationList())
            {
                try
                {
                    manipulador.DynamicInvoke(this, new CampanhiaEventArgs(apartamento));
                }
                catch (Exception e)
                {

                }
            }
            
            
            
        }
    }

    class CampanhiaEventArgs: EventArgs
    {
        public CampanhiaEventArgs(string apartamento)
        {
            Apartamento = apartamento;
        }

        public string Apartamento { get; }

    }
}
