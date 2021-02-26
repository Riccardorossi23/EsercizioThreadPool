using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace EsercizioThreadPool
{
    class Program
    {
        public static List<string> nomi = new List<string>();
        public static string risultato;
        static void Main(string[] args)
        {
            Stopwatch tempo = new Stopwatch();
            StreamReader reader = new StreamReader("nome.txt");
            string file = "nome.txt";
            if (File.Exists(file))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    nomi.Add(line);

                }
                foreach (string nome in nomi)

                    Console.WriteLine($"{nome}");

                Console.Write("\nInserisci qui un nome e cognome da ricercare: ");
                risultato = Console.ReadLine();
                Console.WriteLine("Ricerca eseguita con 10 Thread: ");
                tempo.Start();
                Thread();
                tempo.Stop();
                Console.WriteLine("Tempo impiegato:\n" + tempo.ElapsedTicks.ToString());

                 tempo.Reset();

                Console.WriteLine("Ricerca Ricerca eseguita con il ThreadPool :");
                tempo.Start();
                ThreadPoolUtilizzato();
                tempo.Stop();
                Console.WriteLine("Tempo impiegato: " + tempo.ElapsedTicks.ToString());
            }
            Console.ReadLine();


        }
        public static void Thread()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread t = new Thread(Ricerca);
                t.Start();

            }

        }

        public static void ThreadPoolUtilizzato()
        {
            for (int i = 0; i < 10; i++)
                ThreadPool.QueueUserWorkItem(new WaitCallback(Ricerca));
        }
        static void Ricerca(object callback)
        {
            int i;
            for (i = 0; i < 100; i++)
            {
                if (risultato == nomi[i])
                    Console.WriteLine($"{risultato} è stato trovato in posizione {i}");
            }
            Console.WriteLine($"{risultato} non è stato trovato");
        }
    }
}
