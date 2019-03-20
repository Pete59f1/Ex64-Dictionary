namespace Dictionary
{
    public enum MemberType
    {
        active, passive
    }
    class Program
    {
        static void Main(string[] args)
        {
            MemberUI ui = new MemberUI(new MemberController());
            ui.Run();
        }
    }
    //Ex64 øvelse 1
    //Der er anvendt et løst tre lags arkitektur
    //SOLID bla bla bla
}
