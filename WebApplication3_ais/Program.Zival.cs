using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static WebApplication3_ais.Program;

namespace WebApplication3_ais
{
    public partial class Program
    {
        internal record Zival(int id, string name, string razred, int starost)
        {
            public string opisZivali => name + " , razred:" + razred + ", starost:" +starost;

        }

        internal record Skrbnik(string ime,string specikacija,int delovnaDoba)
        {
            public string opis = ime + " specifikacija: " + specikacija + ", delovna doba:  " + delovnaDoba;
        }

        internal record Zival_v_vrtu(Zival zival, Skrbnik skrbnikZivali, int letoPrihoda, int letoOdhoda, int stSkrbnikov)
        {
            public string opis = zival + ": " + skrbnikZivali + ",od " + letoPrihoda + " ,do.";
        }

     


        public static void Users(WebApplication app)
        {

            Zival zival = new Zival(0, "Delfin", "sesalec", 5);
            Zival zival1 = new Zival(1, "Tiger", "sesalec", 8);
            Zival zival2 = new Zival(2, "Kača", "plazilec", 9);
            Zival zival3 = new Zival(3, "Lev", "sesalec", 5);
            Zival zival4 = new Zival(4, "meduza", "klobučnjak",8);
            Zival zival5 = new Zival(5, "tiger", "sesalec", 7);
            Zival zival6 = new Zival(6, "pajek", "pajkovci", 5);
            Zival zival7 = new Zival(7, "slon", "sesalec", 20);




            Skrbnik skrbnik = new Skrbnik("Maja","Zoolog",20);
            Skrbnik skrbnik1 = new Skrbnik("Andrej", "Biolog", 20);
            Skrbnik skrbnik2 = new Skrbnik("Nika", "Skrbnik", 9);
            //skrbnik skrbnik4 = new skrbnik("Zagrebski zivalski vrt", "Hrvaska", 200);

            //ZivalskiVrt zivalskiVrt3 = new ZivalskiVrt("Madridov zivalski vrt", "Spanija", 200);
            //ZivalskiVrt zivalskiVrt4 = new ZivalskiVrt("Rimski zivalski vrt", "Italija", 200);

            Zival_v_vrtu zival_V_vrtu = new Zival_v_vrtu(zival, skrbnik, 2019,2022,3);
            Zival_v_vrtu zival_V_vrtu1 = new Zival_v_vrtu(zival1, skrbnik1, 2019, 2022, 1);
            Zival_v_vrtu zival_V_vrtu2 = new Zival_v_vrtu(zival2, skrbnik, 2010, 2022, 1);
            Zival_v_vrtu zival_V_vrtu3 = new Zival_v_vrtu(zival3, skrbnik2, 2013, 2022, 6);
            Zival_v_vrtu zival_V_vrtu4 = new Zival_v_vrtu(zival7, skrbnik, 2022, 2025, 2);
            //Zival_v_vrtu zival_V_vrtu5 = new Zival_v_vrtu(zival3, skrbnik2, 2021, 2022, 5);
            Zival_v_vrtu zival_V_vrtu6 = new Zival_v_vrtu(zival3, skrbnik1, 2019, 2022, 3);



            var zivali = new List<Zival>()
            {
             zival,zival1,zival2,zival3,zival4
            };

            var skrbniki = new List<Skrbnik>()
            {
                skrbnik,skrbnik1,skrbnik2
            };

            var zivalskiVrt = new List<Zival_v_vrtu>()
            {
                zival_V_vrtu,zival_V_vrtu1,zival_V_vrtu2,zival_V_vrtu3,zival_V_vrtu4,zival_V_vrtu6
            };

         
            app.MapGet("/zival", () => {
                return vseZivali(zivali);
            });


            app.MapGet("/skrbnik", () => {
                return vsiOskrbniki(skrbniki);
            });

            app.MapGet("/VsiVrtovi", () => {
                return vsiVrtovi(zivalskiVrt);
            });

            app.MapGet("/doloceneZivali/{skrbnik}",(string skrbnik ) => {
                return doloceneZivali(zivalskiVrt, skrbnik);
            });

            app.MapGet("/doloceniSkrbnik/{razred}", (string razred) => {
                return doloceniSkrbniki(zivalskiVrt, razred);
            });

            app.MapGet("/podrobnostiZivali/{id}", (int id) => {
                return podrobnostiZivali(zivali, id);
            });

            app.MapGet("/topSkrbnik", () => {
                return topSkrbnik(zivalskiVrt);
            });

            app.MapGet("/najdljeZaposleni", () => {
                return najdljeZaposleni(skrbniki);
            });

            app.MapGet("/nadpovprecneZivali", () => {
                return nadpovprecneZivali(zivali);
            });

            app.MapPost("/DodajZivali", ([FromBody] Zival ziv) => {
                zivali.Add(ziv);
            });

            app.MapPost("/DodajSkrbnika", ([FromBody] Skrbnik skrb) => {
                skrbniki.Add(skrb);
            });

            app.MapPost("/ZooPost", ([FromBody] Zival_v_vrtu vrt) => {
                zivalskiVrt.Add(vrt);
            });


            app.MapDelete("/DeleteSkrbnik/{ime}", (string ime) => {
                skrbniki.RemoveAll(x => x.ime == ime);
            });


            app.MapDelete("/DeleteZival/{id}", (int id) => {
                zivali.RemoveAll(x => x.id == id);
            });

            app.MapPut("/ChangeAnimal/{id}", (int id,[FromBody]Zival ziv) => {
                
                int idToUpdate = id;              
                int index = zivali.FindIndex(x => x.id == idToUpdate);
                Console.WriteLine(index);

                if (index != -1)
                {
                    zivali[index] = ziv;
                }
                else
                {
                    Console.WriteLine("index not found");
                }
            });


            app.MapPut("/ChangeKeeper/{ime}", (string ime, [FromBody] Skrbnik skrb) => {

                string idToUpdate = ime;
                int index = skrbniki.FindIndex(x => x.ime == idToUpdate);
                Console.WriteLine(index);

                if (index != -1) 
                {
                    skrbniki[index] = skrb;
                }
                else
                {
                    Console.WriteLine("index not found");
                }
            });

            app.MapPut("/ChangeZoo/{id}", (int id, [FromBody] Zival_v_vrtu vrt) => {

                int idToUpdate = id;
                int index = zivalskiVrt.FindIndex(x => x.zival.id == idToUpdate);
                Console.WriteLine(index);

                if (index != -1)
                {
                    zivalskiVrt[index] = vrt;
                }
                else
                {
                    Console.WriteLine("index not found");
                }
            });

            app.MapDelete("/DeleteZivalskiVrt/{id}", (int id) => {
                zivalskiVrt.RemoveAll(x => x.zival.id == id);
            });

        }






        //vse živali
        private static List<Zival> vseZivali(List<Zival> zivali) {

            List<Zival> zivaliVse= zivali.FindAll(x=>x.id>= 0).ToList();
            foreach (var x in zivaliVse)
            {
                Console.WriteLine(x);
            }

            return zivaliVse;
        
        }

        //vsi oskrbniki

        private static List<Skrbnik> vsiOskrbniki(List<Skrbnik> skrbniki)
        {
            List<Skrbnik> skrbnikiVsi = skrbniki.FindAll(x => x.ime != "" && x.specikacija != "" && x.delovnaDoba > 0).ToList();
            foreach (var x in skrbnikiVsi)
            {
                Console.WriteLine(x);
            }
            return skrbniki.FindAll(x=>x.ime != "" && x.specikacija != "" && x.delovnaDoba > 0).ToList();
        }

        //Vsi vrtovi
        private static List<Zival_v_vrtu> vsiVrtovi(List<Zival_v_vrtu> vrtovi)
        {
            return vrtovi;

        }

        //Zivali za katere skrbi določen skrbnik

        private static List<Zival> doloceneZivali(List<Zival_v_vrtu> zivalskiVrt, string ime_skrbnika)
        {
            List<Zival> zivali = zivalskiVrt.FindAll(x => x.skrbnikZivali.ime == ime_skrbnika).Select(x=>x.zival).ToList() ;

            foreach (var x in zivali)
            {
                Console.WriteLine(x);
            }
            return zivali;
        }

        //imena vseh skrbnikov, ki skrbijo za določeno vrsto živali
        private static List<Skrbnik> doloceniSkrbniki(List <Zival_v_vrtu> zivalskiVrt, string ime_razreda)
        {

            List<Skrbnik> skrbniki = zivalskiVrt.FindAll(x => x.zival.razred == ime_razreda).Select(x=>x.skrbnikZivali).Distinct().ToList(); ;
            foreach (var x in skrbniki)
            {
                Console.WriteLine(x);
            }
            return skrbniki;
        
        }

        //podrobnosti določene živali na podlagi id-ja
        private static Zival podrobnostiZivali(List<Zival> zivali,int id)
        {
            Zival iskanaZival = zivali.Single(x => x.id == id);
            Console.WriteLine(iskanaZival);
            return iskanaZival;
        }

        //skrbnik, ki skrbi za največ živali
        private static Skrbnik topSkrbnik(List<Zival_v_vrtu> zivalski_vrt)
        {
            Dictionary<string, int> dictionary_skrbniki = zivalski_vrt.GroupBy(x => x.skrbnikZivali.ime).ToDictionary(Group=>Group.Key,Group=> Group.Count());
            string ime = dictionary_skrbniki.OrderByDescending(x => x.Value).First().Key;
            Skrbnik skrbnikNajden =  zivalski_vrt.FirstOrDefault(x=>x.skrbnikZivali.ime==ime).skrbnikZivali;
            Console.WriteLine(skrbnikNajden);
            return skrbnikNajden;
        }

        //skrbnik s najdaljšo delovno dobo

        private static List<Skrbnik> najdljeZaposleni(List<Skrbnik> skrbniki)
        {
            List<Skrbnik> skrbnikiIzbrani= skrbniki.FindAll(x=>x.delovnaDoba == skrbniki.Max(y=>y.delovnaDoba));
            foreach (var x in skrbnikiIzbrani) {
                Console.WriteLine(x);
            }
            return skrbnikiIzbrani;
        }

        //zivali nadpovprecne starosti

        private static List<Zival> nadpovprecneZivali(List<Zival> zivali) {
            
            List<Zival> zivaliNadpovprecne = zivali.Where(x=>x.starost >= zivali.Average(y=>y.starost)).ToList();
            foreach (var x in zivaliNadpovprecne) {
                Console.WriteLine(x);
            
            }
            return zivaliNadpovprecne;
        }


    }
}


