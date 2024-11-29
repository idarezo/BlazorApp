namespace BlazorApp3.NewFolder
{

    public class Zival
    {
        public int id { get; set; }
        public string name { get; set; }
        public string razred { get; set; }
        public int starost { get; set; }


        public Zival()
        {

        }

        public Zival(int id,string name, string razred, int starost)
        {
            this.id = id;
            this.name = name;
            this.razred = razred;
            this.starost = starost;
        }
    }


    public class Skrbnik
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string specikacija { get; set; }
        public int delovnaDoba { get; set; }


    }

    public class Zival_v_vrtu
    {

        public Zival zival { get; set; }
        public Skrbnik skrbnikZivali { get; set; }
        public int letoPrihoda { get; set; }
        public int letoOdhoda { get; set; }

        public int stSkrbnikov { get; set; }


    }
}
