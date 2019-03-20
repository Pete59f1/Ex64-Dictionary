using System;
using System.Linq;

namespace Dictionary
{
    public delegate Member EnterMemberDelegate();

    class MemberUI
    {
        public static char exitValue = '0';
        private char[] _legalChoices = { exitValue, '1', '2', '3', '4' };
        public MemberController MC { get; set; }

        public MemberUI(MemberController memberController)
        {
            this.MC = memberController;
        }

        public void Run()
        {
            bool running = true;
            char choice;
            do
            {
                ShowMenu();
                choice = GetChoice();
                running = (choice != exitValue);
                if (running)
                {
                    MenuAction(choice);
                }
            } while (running);
        }

        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Tilføj aktivt medlem");
            Console.WriteLine("2 - Tilføj passivt medlem");
            Console.WriteLine("3 - Slet et medlem");
            Console.WriteLine("4 - Vis alle medlemmer");
            Console.WriteLine("");
            Console.WriteLine(exitValue + " - Afslut");
            Console.WriteLine("");
        }

        private char GetChoice()
        {
            char input;
            bool ok;
            do
            {
                Console.Write("Indtast dit valg : ");
                input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                ok = _legalChoices.Contains<char>(input);
                if (!ok)
                {
                    Console.WriteLine($"Input er ikke gyldigt - {input} er ikke et muligt valg. Prøv venligst igen.");
                }
            } while (!ok);
            Console.Clear();
            return input;
        }

        private void MenuAction(char choice)
        {
            switch (choice)
            {
                case '1': MC.AddMember(MemberType.active, EnterMember()); break;
                case '2': MC.AddMember(MemberType.passive, EnterMember()); break;
                case '3': MC.DeleteMember(EnterId()); break;
                case '4': ShowMembers(); break;
            }
        }

        private void ShowMembers()
        {
            Console.WriteLine("Alle medlemmer : ");
            //Pre: None
            //Post: None
            //Output: All members (active and passive) are listed on the screen
            foreach (Member m in MC.Getmembers())
            {
                Console.WriteLine(m);
            }
            Pause();
        }

        public void Pause()
        {
            Console.Write("Tryk på en tast for at forsætte...");
            Console.ReadKey();
        }

        public int EnterId()
        {
            bool ok = false;
            int id = 0;
            string name = string.Empty;
            do
            {
                Console.Write("Indtast Id : ");
                ok = int.TryParse(Console.ReadLine(), out id);
                if (ok == false)
                {
                    Console.WriteLine("Id skal være et heltal. Prøv venligst igen - tast " +
                        exitValue + " for at fortryde.");
                }
            } while (ok == false);
            return id;
        }

        public string EnterName()
        {
            Console.Write("Indtast navn : ");
            return Console.ReadLine();
        }

        public Member EnterMember()
        {
            Member result = null;

            int id = EnterId();
            if (MC.IdAlreadyUsed(id))
            {
                Console.WriteLine("Id er allerede i brug. ");
                Pause();
            }
            else
            {
                if (id != exitValue)
                {
                    result = new Member() { Id = id, Name = EnterName() };
                }
            }
            return result;
        }
    }
}