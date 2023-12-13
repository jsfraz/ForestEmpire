/*
 * Vytvořeno pomocí SharpDevelop.
 * Autor: Josef Ráž
 * Date: 22.05.2020
 * Time: 15:16
 */
 
//NÁVODY
//https://docs.microsoft.com/cs-cz/dotnet/csharp/tour-of-csharp/classes-and-objects
//https://ascii.co.uk/
//https://opengameart.org/
//https://www.youtube.com/watch?v=oHg5SJYRHA0

//barvy		Gray = dialogy/postavy		White = UI text		DarkRed	= enemy		Green = player		DarkYellow = UI		Yellow = fight/dialog UI text

using System;
using System.IO;		//umožňuje čtení a zápis do souborů a datových proudů a typy, které poskytují základní soubor a adresář podpory
using System.Threading;		//knihovna na timing
using System.Text.RegularExpressions;		//na ošetření Regexem

namespace ForestEmpire
{
	public class entity		//enemy a základ pro hráče
	{
	public string name;
	public int hp;
	public int at;
	public int df;
	
	public entity(string name, int hp, int at, int df)		//konstruktor
		{
	        this.name = name;
			this.hp = hp;
			this.at = at;
			this.df = df;
		}
	
	public virtual void stats()		//výpis statistik enemy
		{
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write("     {0} ", name);
		Console.ForegroundColor = ConsoleColor.DarkRed;
		Console.Write("HP: {0}  ATTACK: {1}  DEFENSE: {2}", hp, at, df);
		Console.ResetColor(); 
		}
	
	public virtual void info()
		{
		Console.Write("Jméno: {0}  Životy: {1}  Attack: {2}  Defense. {3}", name, hp, at, df); 
		}
	}
	
	public class hero : entity			//hrdina dědící po entity
	{
		public int gender;
		public int maxhp;
		public int lvl;
		public int money;
		public int material;
		public int heal;
		public int maxheal;
		
		public hero(string name, int gender, int hp, int maxhp, int at, int df, int lvl, int money, int material, int heal, int maxheal) :			//konstruktor
			base(name,hp,at,df)
		{
			this.name = name;
			this.gender = gender;
			this.hp = hp;
			this.maxhp = maxhp;
			this.at = at;
			this.df = df;
			this.lvl = lvl;
			this.money = money;
			this.material = material;
			this.heal = heal;
			this.maxheal = maxheal;
		}
		
		//výpis statistik hrdiny
		public override void stats()		//statistiky
		{
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.Write(" {0} ", name);
		Console.ForegroundColor = ConsoleColor.Green;
		Console.Write("HP: {0} ATTACK: {1} DEFENSE: {2} LEVEL: {3}", hp, at, df, lvl);
		Console.ResetColor(); 
		}
		
		public override void info()
		{
		Console.Write("Jméno: {0} Pohlaví: {1} Životy: {2} Attack: {3} Level: {4} Peníze: {5} $", name, gender, hp, at, df, lvl, money); 
		}
	}
	
	public class buildings		//třída pro město, informace o budovách, obyvatelích, pomocné proměnné
	{
		public int people;
		public int house;
		public int smithy;
		public int saw;
		public int workshop;
		public bool music;
		public bool win;
		
		public buildings(int people, int house, int smithy, int saw, int workshop, bool music, bool win)			//konstruktor
		{
			this.people = people;
			this.house = house;
			this.smithy = smithy;
			this.saw = saw;
			this.workshop = workshop;
			this.music = music;
			this.win = win;
		}
	}
	
	class Program
	{
		//ASCII SEKCE	charakteři mají 21 řádků
		public static void logo()		//logo
		{
			clear();
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(2);
			Console.ForegroundColor = ConsoleColor.DarkYellow; 
			Console.WriteLine("                                       ██████");
			Console.WriteLine("                                      ██      ██████   ██████    █████  █████  ██████");
			Console.WriteLine("                                      ██     ██    ██ ██    ██  ██     ██        ██");
			Console.WriteLine("                                      █████  ██    ██ ██    ██  █████   ██████   ██");
			Console.WriteLine("                                      ██     ██    ██ ███████   ██           ██  ██");
			Console.WriteLine("                                      ██     ██    ██ ██    ██  ██     ██    ██  ██");
			Console.WriteLine("                                      ██      ██████  ██     ██  █████  ██████   ██");
			Console.WriteLine("                                      _______________________________________________");
			Console.WriteLine("");
			Console.WriteLine("                                        ██████");
			Console.WriteLine("                                       ██        █    █   █████  ██  ██████    █████");
			Console.WriteLine("                                       ██       ███  ███ ██   ██ ██ ██    ██  ██");
			Console.WriteLine("                                       █████    ██ ██ ██ ██   ██ ██ ██    ██  █████");
			Console.WriteLine("                                       ██       ██    ██ ██████  ██ ███████   ██");
			Console.WriteLine("                                       ██       ██    ██ ██      ██ ██    ██  ██");
			Console.WriteLine("                                        ██████  ██    ██ ██      ██ ██     ██  █████");
			Console.ResetColor(); 
		}
		
		public static void moni()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(4);
			Console.WriteLine("                                                            x");
			Console.WriteLine("                                                           xxx");
			Console.WriteLine("                                                          xxxxx");
			Console.WriteLine("                                                         xxxxxxx");
			Console.WriteLine("                                                        xxxxxxxxx");
			Console.WriteLine("                                                       xxxxxxxxxxx");
			Console.WriteLine("                                                   xxxxxxxxxxxxxxxxxxxx");
			Console.WriteLine("                                                      /////////////");
			Console.WriteLine("                                                     ///  O   O  ///");
			Console.WriteLine("                                                     //     U     //");
			Console.WriteLine("                                                     //   _____   //");
			Console.WriteLine("");
			Console.WriteLine("                                                      aaaaa   aaaaa");
			Console.WriteLine("                                                   aaaaaaaaaaaaaaaaaaa");
			Console.WriteLine("                                                  aaaaaaa  aaaa   aaaaaa");
			Console.WriteLine("                                                 aaaaa               aaaa");
			Console.WriteLine("                                                aaaaa  aaaaa aaaaaaa  aaa");
			Console.ResetColor();
		}
		
		public static void punch_moni()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(4);
			Console.WriteLine("                                                            x");
			Console.WriteLine("                                                           xxx");
			Console.WriteLine("                                                          xxxxx");
			Console.WriteLine("                                                         xxxxxxx");
			Console.WriteLine("                                                        xxxxxxxxx");
			Console.WriteLine("                                                       xxxxxxxxxxx");
			Console.WriteLine("                                                   xxxxxxxxxxxxxxxxxxxx");
			Console.WriteLine("                                                      /////////////");
			Console.WriteLine("                                                     ///  >   <  ///");
			Console.WriteLine("                                                     //     U     //");
			Console.WriteLine("                                                     //     ___   //");
			Console.WriteLine("");
			Console.WriteLine("                                                      aaaaa   aaaaa");
			Console.WriteLine("                                                   aaaaaaaaaaaaaaaaaaa");
			Console.WriteLine("                                                  aaaaaaa  aaaa   aaaaaa");
			Console.WriteLine("                                                 aaaaa               aaaa");
			Console.WriteLine("                                                aaaaa  aaaaa aaaaaaa  aaa");
			Console.ResetColor();
		}
		
		public static void major()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(5);
			Console.WriteLine("                                                       .;;;;;;;;;.");
			Console.WriteLine("                                                      ;;;;;;;;;;;;;");
			Console.WriteLine("                                                      ;;    '    ;;");
			Console.WriteLine("                                                      ;   _   _   ;");
			Console.WriteLine("                                                     n; ___\\^/___ ;n");
			Console.WriteLine("                                                     )| `-'  '`- `|(");
			Console.WriteLine("                                                     `(      .    )'");
			Console.WriteLine("                                                       \\ / `-' \\ /");
			Console.WriteLine("                                                       |  _____  |");
			Console.WriteLine("                                                       `.  ---  ,'");
			Console.WriteLine("                                                         \\  ^  /");
			Console.WriteLine("                                                      ____-----____");
			Console.WriteLine("                                                 /----    |>-<|    ----\\");
			Console.WriteLine("                                                /                       \\");
			Console.WriteLine("                                               |                         |");
			Console.WriteLine("                                               |                         |");
		}
		
		public static void major_smile()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(5);
			Console.WriteLine("                                                       .;;;;;;;;;.");
			Console.WriteLine("                                                      ;;;;;;;;;;;;;");
			Console.WriteLine("                                                      ;;    '    ;;");
			Console.WriteLine("                                                      ;   _   _   ;");
			Console.WriteLine("                                                     n; ___\\^/___ ;n");
			Console.WriteLine("                                                     )| `-'  '`- `|(");
			Console.WriteLine("                                                     `(      .    )'");
			Console.WriteLine("                                                       \\ / `-' \\ /");
			Console.WriteLine("                                                       |  \\___/  |");
			Console.WriteLine("                                                       `.  ---  ,'");
			Console.WriteLine("                                                         \\  ^  /");
			Console.WriteLine("                                                      ____-----____");
			Console.WriteLine("                                                 /----    |>-<|    ----\\");
			Console.WriteLine("                                                /                       \\");
			Console.WriteLine("                                               |                         |");
			Console.WriteLine("                                               |                         |");
		}
		
		public static void dragon()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(3);
			Console.WriteLine("                                           <>=======()");
			Console.WriteLine("                                          (/\\___   /|\\\\          ()==========<>_");
			Console.WriteLine("                                                \\_/ | \\\\        //|\\   ______/ \\)");
			Console.WriteLine("                                                  \\_|  \\\\      // | \\_/");
			Console.WriteLine("                                                    \\|\\/|\\_   //  /\\/");
			Console.WriteLine("                                                     (oo)\\ \\_//  /");
			Console.WriteLine("                                                    //_/\\_\\/ /  |");
			Console.WriteLine("                                                   @@/  |=\\  \\  |");
			Console.WriteLine("                                                        \\_=\\_ \\ |");
			Console.WriteLine("                                                          \\==\\ \\|\\_");
			Console.WriteLine("                                                       __(\\===\\(  )\\");
			Console.WriteLine("                                                      (((~) __(_/   |");
			Console.WriteLine("                                                           (((~) \\  /");
			Console.WriteLine("                                                           ______/ /");
			Console.WriteLine("                                                           '------'");                                          
			writeline(3);
		}
		
		public static void ogre()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(1);
			Console.WriteLine("                                                     __,='`````'=/__");      
			Console.WriteLine("                                                    '//  (o) \\(o) \\ `'         _,-,");
			Console.WriteLine("                                                    //|     ,_)   (`\\      ,-'`_,-\\");
			Console.WriteLine("                                                  ,-~~~\\  `===='  /-,      \\==```` \\__");
			Console.WriteLine("                                                 /        `----'     `\\     \\       \\/");
			Console.WriteLine("                                              ,-`                  ,   \\  ,.-\\       \\");
			Console.WriteLine("                                             /      ,               \\,-`\\`_,-`\\_,..--'\\");
			Console.WriteLine("                                            ,`    ,/,              ,>,   )     \\--`````\\");
			Console.WriteLine("                                            (      `\\`---'`  `-,-'`_,<   \\      \\_,.--'`");
			Console.WriteLine("                                             `.      `--. _,-'`_,-`  |    \\");
			Console.WriteLine("                                              [`-.___   <`_,-'`------(    /");
			Console.WriteLine("                                              (`` _,-\\   \\ --`````````|--`");
			Console.WriteLine("                                               >-`_,-`\\,-` ,          |");
			Console.WriteLine("                                             <`_,'     ,  /\\          /");
			Console.WriteLine("                                              `  \\/\\,-/ `/  \\/`\\_/V\\_/");
			Console.WriteLine("                                                 (  ._. )    ( .__. )");
			Console.WriteLine("                                                 |      |    |      |");
			Console.WriteLine("                                                  \\,---_|    |_---./");
			Console.WriteLine("                                                  ooOO(_)    (_)OOoo");
			writeline(1);
		}
		
		public static void centaur_female()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("                                                            (_______");
			Console.WriteLine("                                                         -.__\\     __\\");
			Console.WriteLine("                                                        _)        /  \\");
			Console.WriteLine("                                                         \\_ _    ( '('");
			Console.WriteLine("                                                          _>_     \\_-/");
			Console.WriteLine("                                                             )/ ,-' (-.");
			Console.WriteLine("                                                              )/ _ - - )");
			Console.WriteLine("                                                              /,'| _)_)|");
			Console.WriteLine("                                               (_____        //  |   /||");
			Console.WriteLine("                                             .___\\   \\----._//___/ '( \\\\");
			Console.WriteLine("                                              _>    /    __//    ',,,\\ )\\");
			Console.WriteLine("                                             _)   /|    /,-/          )'\\|");
			Console.WriteLine("                                             \\   ( |     ,            |");
			Console.WriteLine("                                             /_,\\(  \\     \\-.__\\  (_, /");
			Console.WriteLine("                                            (    '   \\    |  |  ) |\\ /");
			Console.WriteLine("                                                     _) _/ _/  /, )/ )");
			Console.WriteLine("                                                    _) <\\ (     ) |) |");
			Console.WriteLine("                                                      ) \\)_\\_   / / \\(");
			Console.WriteLine("                                                      /_,\\ \\_\\   )| /_\\");
			Console.WriteLine("                                                        )_\\     /_,) )_\\");
			Console.WriteLine("                                                                 |_\\");
		}
		
		public static void centaur_male()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("                                                              ___,      //\\");
			Console.WriteLine("                                                             /,===      \\//");
			Console.WriteLine("                                                             ( '|'      (# )");
			Console.WriteLine("                                                             '\\_-/       :|");
			Console.WriteLine("                                                          ,---'  \\---.   ||");
			Console.WriteLine("                                                         (     - -    )  |:");
			Console.WriteLine("                                                         |  \\_. '  _./\\ ,'/\\");
			Console.WriteLine("                                                         |  )       / ,-||\\/");
			Console.WriteLine("                                               ___       ( < \\     (\\___/||");
			Console.WriteLine("                                              /   \\,----._\\ \\(   '  )    #;");
			Console.WriteLine("                                             (   /         \\|'',, ,'\\");
			Console.WriteLine("                                             )   |          )\\   '   |");
			Console.WriteLine("                                             (  (|     ,    \\_)      |");
			Console.WriteLine("                                              )  )\\     \\-.__\\   |_, /");
			Console.WriteLine("                                              ( (  \\    )  )  ]  |  (");
			Console.WriteLine("                                               ) ) _) _/ _/   /, )) /");
			Console.WriteLine("                                               (/  \\ <\\ \\      \\ |\\ |");
			Console.WriteLine("                                                ) ._) \\__\\_    ) | )(");
			Console.WriteLine("                                                   )_,,\\ )_\\    )|<,_\\");
			Console.WriteLine("                                                      )_\\      /_(  |_\\");
			Console.WriteLine("                                                                )_\\");
		}
		
		public static void skeleton()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("                                                         _.--''-._");
			Console.WriteLine("                             .                         .'         '.'");
			Console.WriteLine("                            / \\    ,^.         /(     Y             |      )\\");
			Console.WriteLine("                           /   `---. |--'\\    (  \\__..'--   -   -- -'''-.-'  )");
			Console.WriteLine("                           |        :|    `>   '.     l_..-------.._l      .'");
			Console.WriteLine("                           |      __l;__ .'      '-.__.||_.-'v'-._||.____--");
			Console.WriteLine("                            \\  .-' | |  `              l._       _.'");
			Console.WriteLine("                             \\/    | |                   l`^^'^^'j");
			Console.WriteLine("                                   | |                _   \\_____/     _");
			Console.WriteLine("                                   | |               l `--__)-'(__.--' |");
			Console.WriteLine("                                   | |               | /`---``-----''\\ |  ,-----.");
			Console.WriteLine("                                   | |               )/  `--' '---'   \\'-'  ___  `-.");
			Console.WriteLine("                                   | |              //  `-'  '`----'  /  ,-'   I`.  \\");
			Console.WriteLine("                                 _ L |_            //  `-.-.'`-----' /  /  |   |  `. \\");
			Console.WriteLine("                                '._' / \\         _/(   `/   )- ---' ;  /__.J   L.__.\\ :");
			Console.WriteLine("                                 `._;/7(-.......'  /        ) (     |  |            | |");
			Console.WriteLine("                                 `._;l _'--------_/        )-'/     :  |___.    _._./ ;");
			Console.WriteLine("                                   | |                 .__ )-'\\  __  \\  \\  I   1   / /");
			Console.WriteLine("                                   `-'                /   `-\\-(-'   \\ \\  `.|   | ,' /");
			Console.WriteLine("                                                      \\__  `-'    __/  `-. `---'',-'");
			Console.WriteLine("                                                         )-._.-- (        `-----'");
		}
		
		public static void wolf()
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("                                                                     __");
			Console.WriteLine("                                                                   .d$$b");
			Console.WriteLine("                                                                 .' TO$;\\");
			Console.WriteLine("                                                                /  : TP._;");
			Console.WriteLine("                                                               / _.;  :Tb|");
			Console.WriteLine("                                                              /   /   ;j$j");
			Console.WriteLine("                                                          _.-'       d$$$$");
			Console.WriteLine("                                                        .' ..       d$$$$;");
			Console.WriteLine("                                                       /  /P'      d$$$$P. |\\");
			Console.WriteLine("                                                      /   '      .d$$$P' |\\^'l");
			Console.WriteLine("                                                    .'           `T$P^'''''  :");
			Console.WriteLine("                                                ._.'      _.'                ;");
			Console.WriteLine("                                             `-.-'.-'-' ._.       _.-'    .-'");
			Console.WriteLine("                                           `.-' _____  ._              .-'");
			Console.WriteLine("                                          -(.g$$$$$$$b.              .'");
			Console.WriteLine("                                            ''^^T$$$P^)            .(:");
			Console.WriteLine("                                              _/  -'  /.'         /:/;");
			Console.WriteLine("                                           ._.'-'`-'  ')/         /;/;");
			Console.WriteLine("                                        `-.-'..--''   ' /         /  ;");
			Console.WriteLine("                                       .-' ..--''        -'          :");
			Console.WriteLine("                                       ..--''--.-'         (\\      .-(\\");
		}
		
		public static void knight(hero player)		//25 řádků
		{
			//pohlaví na text
			string gndr = "transgender bojová helikoptéra";
            if(player.gender == 1)
            {
            	gndr = "muž";
            }
            if(player.gender == 2)
            {
            	gndr = "žena";
            }
            if(player.gender == 3)
            {
            	gndr = "jiné";
            }
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("                                                                  ██   ██");
			Console.WriteLine("                             .I.                                  ██   ██ █████  █████  ██ ███    ██");
			Console.WriteLine("                            / : \\                                 ██   ██ ██  ██ ██  ██ ██ ████   ██  ████");
			Console.WriteLine("                            |===|                                 ███████ ██  ██ ██  ██ ██ ██ ██  ██ ██  ██");
			Console.WriteLine("                            >._.<                                 ██   ██ █████  ██  ██ ██ ██  ██ ██ ██████");
			Console.WriteLine("                        .=-<     >-=.                             ██   ██ ██ ██  ██  ██ ██ ██   ████ ██  ██");
			Console.WriteLine("                       /.'`(`-+-')'`.\\                            ██   ██ ██  ██ █████  ██ ██    ███ ██  ██");
			Console.WriteLine("                     _/`.__/  :  \\__.'\\_");
			Console.WriteLine("                    ( `._/\\`. : .'/\\_.' )                         Jméno: " + player.name);
			Console.WriteLine("                     >-(_) \\ `:' / (_)-<                          Pohlaví: " + gndr);
			Console.WriteLine("                     | |  / \\___/ \\  | |                          Zdraví: " + player.maxhp + " HP");
			Console.WriteLine("                     )^( | .' : `. | )^(                          Útok: " + player.at);
			Console.WriteLine("                    |  _\\|`-._:_.-'| \\  |                         Obrana: " + player.df);
			Console.WriteLine("                    '-<\\)| :  |  : |  '-'                         Level: " + player.lvl);
			Console.WriteLine("                      (\\\\| : / \\ : |");
			Console.WriteLine("                        \\\\-:-| |-:-')                             Vylepšení stojí vždy na základě levelu.");
			Console.WriteLine("                         \\\\:_/ \\_:_/                              První stojí 500 $");
			Console.WriteLine("                         |\\\\_| |_:_|");
			Console.WriteLine("                         (;\\\\/ \\__;)");
			Console.WriteLine("                         |: \\\\  | :|");
			Console.WriteLine("                         \\: /\\\\ \\ :/");
			Console.WriteLine("                         |==| \\\\|==|");
			Console.WriteLine("                        /v-'(  \\\\`-v\\");
			Console.WriteLine("                       // .-'   \\\\. \\\\");
			Console.WriteLine("                       `-'       \\\\`-'");
			Console.WriteLine("                                  \\|                              1 VYLEPŠIT                         2 ZPĚT");
		}
		
		public static void knight_female(hero player)
		{
			//pohlaví na text
			string gndr = "transgender bojová helikoptéra";
            if(player.gender == 1)
            {
            	gndr = "muž";
            }
            if(player.gender == 2)
            {
            	gndr = "žena";
            }
            if(player.gender == 3)
            {
            	gndr = "jiné";
            }
			Console.WriteLine("                                                                  ██   ██");
			Console.WriteLine("                                                                  ██   ██ █████  █████  ██ ███    ██");
			Console.WriteLine("                                                                  ██   ██ ██  ██ ██  ██ ██ ████   ██  ████");
			Console.WriteLine("                                                                  ███████ ██  ██ ██  ██ ██ ██ ██  ██ ██  ██");
			Console.WriteLine("                                 _A_                              ██   ██ █████  ██  ██ ██ ██  ██ ██ ██████");
			Console.WriteLine("                                / | \\                             ██   ██ ██ ██  ██  ██ ██ ██   ████ ██  ██");
			Console.WriteLine("                               |.-=-.|                            ██   ██ ██  ██ █████  ██ ██    ███ ██  ██");
			Console.WriteLine("                               )\\_|_/(");
			Console.WriteLine("                            .=='\\   /`==.                         Jméno: " + player.name);
			Console.WriteLine("                          .'\\   (`:')   /`.                       Pohlaví: " + gndr);
			Console.WriteLine("                        _/_ |_.-' : `-._|__\\_                     Zdraví: " + player.maxhp + " HP");
			Console.WriteLine("                       <___>'\\    :   / `<___>                    Útok: " + player.at);
			Console.WriteLine("                       /  /   >=======<  /  /                     Obrana: " + player.df);
			Console.WriteLine("                     _/ .'   /  ,-:-.  \\/=,'                      Level: " + player.lvl);
			Console.WriteLine("                    / _/    |__/v^v^v\\__) \\");
			Console.WriteLine("                    \\(\\)     |V^V^V^V^V|\\_/                       Vylepšení stojí vždy na základě levelu.");
			Console.WriteLine("                     (\\\\     \\`---|---'/                          První stojí 500 $");
			Console.WriteLine("                       \\\\     \\-._|_,-/");
			Console.WriteLine("                        \\\\     |__|__|");
			Console.WriteLine("                         \\\\   <___X___>");
			Console.WriteLine("                          \\\\   \\..|../");
			Console.WriteLine("                           \\\\   \\ | /");
			Console.WriteLine("                            \\\\  /V|V\\");
			Console.WriteLine("                             \\|/  |  \\");
			Console.WriteLine("                              '--' `--`                           1 VYLEPŠIT                         2 ZPĚT");
			Console.WriteLine("");
		}
		
		//PREMADE TEXT SEKCE
		public static void sign()		//copyright
		{
			writeline(4);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                                                     (c)2020 Josef Ráž");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("                              Doporučuje se hrát scrollnuto nahoru a bez změny velikosti okna.");
			Console.WriteLine("                                       Stiskněte libovolnou klávesu pro pokračování.");
			writeline(2);
			Console.ResetColor(); 
			Console.ReadKey(true);
		}
		
		public static void menu()
		{
			sound("\\sound\\music\\main_menu.wav", 1);
			//volba v menu
			int menu_choice;
			while(true)		//ošetření
			{
				logo();
				writeline(3);
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("                                                      [1]");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(" Nová Hra");
				writeline(2);
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("                                                      [2]");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(" Konec");
				writeline(6);
				shell();
				//ošetření
				string string_menu_choice = Console.ReadLine();
				if(Regex.IsMatch(string_menu_choice, "^[1-2]$"))	//kontroluje jestli je proměnná 1-2
				{
					menu_choice = int.Parse(string_menu_choice);	//převede string na int
					break;		//zastaví cyklus
				}
			}
			//akce
			switch(menu_choice)
			{
				case 1: //1
				clear();
				break;
				
				case 2: //2
				Environment.Exit(0);
				break;
			}
		}
		
		public static void shell()		//zakončení >
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(">");
			Console.ForegroundColor = ConsoleColor.Yellow;
		}
		
		public static void mini_table(string mini_table_text)		//tabulka
		{
			writeline(10);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("                                            |=================================|");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.Write(mini_table_text);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("");
			Console.WriteLine("                                            |=================================|");
			Console.ResetColor(); 
		}
		
		public static void line_enemy()		//červená čára
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("________________________________________________________________________________________________________________________");
			Console.ResetColor(); 
		}
		
		public static void line_friendly()		//žlutá čára
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("________________________________________________________________________________________________________________________");
			Console.ResetColor(); 
		}
		
		public static void fight_buttons()		//fightovací ui
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			writeline(2);
			Console.WriteLine("             =============             =============             =============             =============");
			Console.WriteLine("             | 1 FIGHT   |             |  2 HEAL   |             |  3 MERCY  |             |   4 RUN   |");
			Console.WriteLine("             =============             =============             =============             =============");
			Console.WriteLine("");
			shell();
		}
			
		//UI SEKCE
		public static void first()		//hlavní menu
		{
			logo();
			sign();
		}
		
		public static void major_ui(string txt)		//dialog starosty	text = animovaný text
		{
			clear();
			major();
			line_friendly();
            text(txt);
            writeline(6);
            Console.ReadKey(true);
		}
		
		public static void u_fight_ui(hero player , entity enemy, int enemy_type)		//fight ui     enemy_type = enemy_type (ASCII typu enemy)
		{
			clear();
			if(enemy_type == 1 | enemy_type == 3)
			{
				dragon();
			}
			if(enemy_type == 2 | enemy_type == 4)
			{
				ogre();
			}
			if(enemy_type == 5 | enemy_type == 7)
			{
				if(enemy.name == "Kentaur Ivan")
				{
					centaur_male();
				}
				else
				{
					centaur_female();
				}
			}
			if(enemy_type == 6 | enemy_type == 9)
			{
				skeleton();
			}
			if(enemy_type == 8 | enemy_type == 10)
			{
				wolf();
			}
			//monikalda
			if(enemy_type == 11)
			{
				moni();
			}
			//punch moni
			if(enemy_type == 12)
			{
				punch_moni();
			}
			line_enemy();
			Console.ForegroundColor = ConsoleColor.Green;
			player.stats();
			Console.ForegroundColor = ConsoleColor.DarkRed;
			enemy.stats();
			fight_buttons();
		}
		
		public static void enemy_fight_ui(string txt, int color, int enemy_type, entity enemy)		//text = animovaný text     color = 1 - červená 2 - zelená 3- šedivá     enemy_type = enemy_type (ASCII typu enemy)
		{
			clear();
			if(enemy_type == 1 | enemy_type == 3)
			{
				dragon();
			}
			if(enemy_type == 2 | enemy_type == 4)
			{
				ogre();
			}
			if(enemy_type == 5 | enemy_type == 7)
			{
				if(enemy.name == "Kentaur Ivan")
				{
					centaur_male();
				}
				else
				{
					centaur_female();
				}
			}
			if(enemy_type == 6 | enemy_type == 9)
			{
				skeleton();
			}
			if(enemy_type == 8 | enemy_type == 10)
			{
				wolf();
			}
			//monikalda
			if(enemy_type == 11)
			{
				moni();
			}
			//punch moni
			if(enemy_type == 12)
			{
				punch_moni();
			}
			line_enemy();
			if(color == 1)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
			}
			if(color == 2)
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			if(color == 3)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
			}
            text(txt);
            Console.ResetColor();
            writeline(6);
            Console.ReadKey(true);
            clear();
		}
		
		//FUNKČNÍ (custom pomocné metody)
		public static void sound(string file, int play)		//volá se sound("\\cesta\\k\\souboru);
		{
			string path = Directory.GetCurrentDirectory();		//soubor pto přehrání
			int path_length = path.Length;		//cesta k exe souboru (aktuální adresář)
           	path = path.Insert(path_length, file);		//spočítá délku názvu adresáře
           	bool exist = File.Exists(path);		//pokud soubor v umístění path existuje, je hodnota true
           	//pokud se zde soubor nachází, přehraje se. Zamezuje tak errorům, kdy soubor v umístění chybí.
           	if(exist == true)
           	{
				System.Media.SoundPlayer snd = new System.Media.SoundPlayer(@path);		//vloží nízev souboru "file" za adresář "path"
				//Stop
				if(play == 0)
				{
					snd.Stop();
				}
				//Loop
				if(play == 1)
				{
					snd.PlayLooping();
				}
				//Hrát jednou
				if(play == 2)
				{
					snd.Play();
				}
           	}
		}
		
		public static void writeline(int ivalue)	//procedura na libovolné opakování writeline
		{
		for (int i = 0; i < ivalue; i++)
			{
			Console.WriteLine("");
			}
		}
		
		public static void clear()		//zástupce Console.Clear();
		{
		Console.Clear();	
		}
		
		public static void text(string text)		//animace textu
		{
			foreach (var endword in text)
    		{
      			Console.Write(""+ endword + "");
        		Thread.Sleep(35);
			}
		}
		
		public static string [,] vlg = new string[27,27];		//město
		
		public static void village_create(hero player, buildings blackridge)		//naplnění pole
		{
			for(int line = 1; line < 26; line++)
            {
				for(int column = 1; column < 26; column++)
				{
					vlg[line,column]= "██";
				}
            }
            //řádky
            int x = 1;
            string x_string;
            for(int column = 1; column < 27; column++)
            {
            	x_string = x.ToString("D2");
				vlg[0,column]= x_string;
				x++;
				for(int line = 0; line < 27; line++)
				{
					vlg[26,column]= "==";
				}
            }
            //sloupce
            x = 0;
            x_string = "00";
            for(int line = 0; line < 27; line++)
            {
            	x_string = x.ToString("D2");
				vlg[line,0]= x_string;
				x++;
				for(int column = 0; column < 27; column++)
				{
					vlg[line,26]= "|";
				}
            }
            vlg[0,0] = "  ";
         	vlg[0,26] = "Y ██████";
         	vlg[1,26] = "| ██   ██ ██            ████  ██  ██ █████  ██ █████   ████  █████";
         	vlg[2,26] = "| ██   ██ ██     ████  ██  ██ ██ ██  ██  ██ ██ ██  ██ ██  ██ ██";
			vlg[3,26] = "| ██████  ██    ██  ██ ██     ████   ██  ██ ██ ██  ██ ██     ████";
			vlg[4,26] = "| ██   ██ ██    ██████ ██     ████   █████  ██ ██  ██ ██ ██  ██";
         	vlg[5,26] = "| ██   ██ ██    ██  ██ ██  ██ ██ ██  ██ ██  ██ ██  ██ ██  ██ ██";
			vlg[6,26] = "| ██████  █████ ██  ██  ████  ██  ██ ██  ██ ██ █████   ████  █████";
			vlg[7,26] = "| ________________________________________________________________";
			vlg[2,2] = "AA";
			vlg[2,3] = "AA";
			vlg[2,4] = "AA";
			vlg[3,2] = "[]";
			vlg[4,2] = "<>";
			vlg[5,2] = "÷÷";
			vlg[26,0] = " X";
		}
		
		//aktualizace pole village_menu
		public static void village_menu_actual(hero player, buildings blackridge)
		{
			//pohlaví pro info tabulku v poli
            string gndr = "transgender bojová helikoptéra";
            if(player.gender == 1)
            {
            	gndr = "muž";
            }
            if(player.gender == 2)
            {
            	gndr = "žena";
            }
            if(player.gender == 3)
            {
            	gndr = "jiné";
            }
			//základní budovy	[] kovárna		<> pila		÷÷ dílna		AA ubytování
			vlg[9,26] = "|      Obyvatelé: " + blackridge.people + "";
			vlg[10,26] = "|      Materiál: " + player.material + "";
			vlg[11,26] = "|      AA ubytování: " + blackridge.house + "";
			vlg[12,26] = "|      [] kovárny: " + blackridge.smithy + "";
			vlg[13,26] = "|      <> pily: " + blackridge.saw + "";
			vlg[14,26] = "|      ÷÷ dílny: " + blackridge.workshop + "";
			vlg[16,26] = "|      Jméno: " + player.name + "";		//von Blackridge
			vlg[17,26] = "|      Pohlaví: " + gndr + "";
			vlg[18,26] = "|      Peníze: " + player.money + " $";
			vlg[19,26] = "|      Zdraví: " + player.maxhp + "";
			vlg[20,26] = "|      Útok: " + player.at + "";
			vlg[21,26] = "|      Obrana: " + player.df + "";
			vlg[22,26] = "|      Level: " + player.lvl + "";
			vlg[24,26] = "|      1 STAVĚT              2 BOJOVAT             3 UPGRADES";
			vlg[26,26] = "|      4 BOSSFIGHT           5 NÁPOVĚDA            6 KONEC";		//v budoucnu ukládání?
		}
		
		//aktualizace pole village_build_menu
		public static void village_build_actual(hero player, buildings blackridge)
		{
			//základní budovy	[] kovárna		<> pila		÷÷ dílna		AA ubytování
			vlg[9,26] = "|      Obyvatelé: " + blackridge.people + "";
			vlg[10,26] = "|      Materiál: " + player.material + "";
			vlg[11,26] = "|      AA ubytování: " + blackridge.house + "";
			vlg[12,26] = "|      [] kovárny: " + blackridge.smithy + "";
			vlg[13,26] = "|      <> pily: " + blackridge.saw + "";
			vlg[14,26] = "|      ÷÷ dílny: " + blackridge.workshop + "";
			//přepíše sloupec 26 na |
			for(int i = 16; i < 26; i++)
			{
				vlg[i,26] = "|";
			}
			vlg[16,26] = "|      Souřadnice budov se udávají ve formátu:";
			vlg[17,26] = "|      Řádek (X)";
			vlg[18,26] = "|      Sloupec (Y)";
			vlg[26,26] = "|                  1 POSTAVIT             2 ZPĚT";
		}
		
		public static void village_print()		//vypsání pole
		{
			Console.ForegroundColor = ConsoleColor.White;
            for (int line = 0; line < 27; line++)
			{
				for (int column = 0; column < 27; column++)
				{
					Console.Write(vlg[line,column]);
				}
				Console.WriteLine();
			}
            writeline(2);
            shell();
		}
		
		public static void village_counter(hero player, buildings blackridge)		//počítá budovy a obyvatele
		{
			int house = 0;
			int smithy = 0;
			int saw = 0;
			int workshop = 0;
			//cyklus projíždějící pole
			for(int line = 1; line < 26; line++)
            {
				for(int column = 1; column < 26; column++)
				{
					//ubytování
					if(vlg[line,column] == "AA")
					{
						house ++;
					}
					//kovárna
					if(vlg[line,column] == "[]")
					{
						smithy ++;
					}
					//pila
					if(vlg[line,column] == "<>")
					{
						saw ++;
					}
					//dílna
					if(vlg[line,column] == "÷÷")
					{
						workshop ++;
					}
				}
            }
			//počítání obyvatel
			blackridge.people = house * 4;
			blackridge.house = house;
			blackridge.smithy = smithy;
			blackridge.saw = saw;
			blackridge.workshop = workshop;
		}
		
		public static void money(hero player, buildings blackridge)
		{
			int money_plus;		//kolik se přičte
			//do jednoho domu se vejdou 4 lidi, to znamená že když mám 3 funkčí budovy, potřebuju 3 domy	(funknčí budovy = domy)
			//za každou funkční budovu je za jeden cyklus boje 10 money
			int all_buildings = blackridge.saw + blackridge.smithy + blackridge.workshop;		//šechny funkční budovy
			//pokud je stejně domů jako funkčních budov
			if(all_buildings == blackridge.house)
			{
				money_plus = blackridge.house * 10;
				player.money += money_plus;
			}
			//pokud je méně domů než funkčních budov
			if(blackridge.house < all_buildings)
			{
				money_plus = blackridge.house * 10;
				player.money += money_plus;
			}
			//pokud je více domů než funkčních budov
			if(blackridge.house > all_buildings)
			{
				money_plus = all_buildings * 10;
				player.money += money_plus;
			}
		}
		
		public static void village_menu(hero player, entity enemy, buildings blackridge)
		{
			if(blackridge.music == false)
			{
				blackridge.music = true;
				sound("\\sound\\music\\village.wav", 1);
			}
			int action;
			while(true)		//ošetření
			{
				clear();
				player.hp = player.maxhp;
				player.heal = player.maxheal;
				village_counter(player,blackridge);
				village_menu_actual(player, blackridge);
				village_print();
				string string_action = Console.ReadLine();
				if(Regex.IsMatch(string_action, "^[1-6]$|^3011$"))	//kontroluje jestli je proměnná 1-6
				{
					action = int.Parse(string_action);	//převede string na int
					break;		//zastaví cyklus
				}
			}
			//akce
			switch(action)
			{
				case 1:
				village_build_menu(player,enemy,blackridge);
				break;
				
				case 2:
				fight(player,enemy,blackridge);
				break;
				
				case 3:
				upgrades(player,enemy,blackridge);
				break;
				
				case 4:
				if(blackridge.win == false)
				{
					if(player.lvl == 3)
					{
						monfight(player,enemy,blackridge,0);
					}
					else
					{
						clear();
						mini_table("                                                     Musíte mít level 3!");
						writeline(17);
						Console.ReadKey();
						village_menu(player,enemy,blackridge);
					}
				}
				else
				{
					clear();
					mini_table("                                                     Monikaldu poražena!");
					writeline(17);
					Console.ReadKey();
					village_menu(player,enemy,blackridge);
				}
				break;
				
				case 5:
				help(player,enemy,blackridge);
				break;
				
				case 6:
				//
				Environment.Exit(0);
				break;
				
				case 3011:
				player.money += 100000;
				player.material += 100000;
				village_menu(player,enemy,blackridge);
				break;
			}
		}
		
		public static void upgrades(hero player, entity enemy, buildings blackridge)
		{
			int action_upgrades;
			while(true)		//ošetření
			{
				clear();
				Console.ForegroundColor = ConsoleColor.Gray;
				//pohlaví hráče
				if(player.gender == 2)
				{
					knight_female(player);
				}
				else
				{
					knight(player);
				}
				writeline(3);
				shell();
				string string_action_upgrades = Console.ReadLine();
				if(Regex.IsMatch(string_action_upgrades, "^[1-2]$"))	//kontroluje jestli je proměnná 1-2
				{
					action_upgrades = int.Parse(string_action_upgrades);	//převede string na int
					break;		//zastaví cyklus
				}
			}
			//kupování
			switch(action_upgrades)
			{
					case 1:
					//level 1
					if(player.lvl == 1)
					{
						if(player.money >= 500)
						{
							player.lvl = 2;
							player.maxhp = 22;
							player.at = 12;
							player.maxheal = 4;
							player.money -= 500;
							clear();
							mini_table("                                                     Nyní máte level " + player.lvl + "!");
							writeline(17);
							Console.ReadKey();
							upgrades(player,enemy,blackridge);
						}
						else
						{
							clear();
							mini_table("                                                   Nemáte dostatek peněz!");
							writeline(17);
							Console.ReadKey();
							upgrades(player,enemy,blackridge);
						}
						
					}
					
					if(player.lvl == 2)
					{
						if(player.money >= 1000)
						{
							player.lvl = 3;
							player.maxhp = 27;
							player.at = 17;
							player.df = 8;
							player.maxheal = 6;
							player.money -= 1000;
							clear();
							mini_table("                                                     Nyní máte level " + player.lvl + "!");
							writeline(17);
							Console.ReadKey();
							upgrades(player,enemy,blackridge);
						}
						else
						{
							clear();
							mini_table("                                                   Nemáte dostatek peněz!");
							writeline(17);
							Console.ReadKey();
							upgrades(player,enemy,blackridge);
						}
						
					}
					if(player.lvl == 3)
					{
							clear();
							mini_table("                                                 Již máte maximální level!");
							writeline(17);
							Console.ReadKey();
							upgrades(player,enemy,blackridge);
					}
					//oznámení
					break;
					
					case 2:
					village_menu(player,enemy,blackridge);
					break;
			}
			//akce
			switch(action_upgrades)
			{
				case 1:
				break;
				
				case 2:
				village_menu(player,enemy,blackridge);
				break;
			}
		}
		
		public static void village_build_menu(hero player, entity enemy, buildings blackridge)		//menu stavba vesnice
		{
			int action_build_menu;
			while(true)		//ošetření
			{
				clear();
				village_counter(player,blackridge);
				village_build_actual(player,blackridge);
				village_print();
				string string_action_build_menu = Console.ReadLine();
				if(Regex.IsMatch(string_action_build_menu, "^[1-2]$|^3011$"))	//kontroluje jestli je proměnná 1-2
				{
					action_build_menu = int.Parse(string_action_build_menu);	//převede string na int
					break;		//zastaví cyklus
				}
			}
			//akce
			switch(action_build_menu)
			{
				case 1:
				village_build(player,enemy,blackridge);
				break;
				
				case 2:
				village_menu(player,enemy,blackridge);
				break;
				
				case 3011:
				//cheat
				for(int line = 1; line < 13; line++)
            	{
					for(int column = 1; column < 26; column++)
					{
						vlg[line,column]= "AA";
					}
            	}
				for(int line = 13; line < 25; line++)
            	{
					for(int column = 1; column < 26; column++)
					{
						vlg[line,column]= "[]";
					}
            	}
				village_build_menu(player,enemy,blackridge);
				break;
			}
		}
		
		public static void village_build(hero player, entity enemy, buildings blackridge)		//stavení vesnice		x = řádek		y = sloupec
		{
			
			//zadání X
			int x_value = 0;
			clear();
			mini_table("                                                     Zadejte pozici X:");
			writeline(16);
			shell();
			string string_x_value = Console.ReadLine();
			while(string_x_value != "1" & string_x_value != "2" & string_x_value != "3" & string_x_value != "4" & string_x_value != "5" & string_x_value != "6" & string_x_value != "7" & string_x_value != "8" & string_x_value != "9" & string_x_value != "10" & string_x_value != "11" & string_x_value != "12" & string_x_value != "13" & string_x_value != "14" & string_x_value != "15" & string_x_value != "16" & string_x_value != "17" & string_x_value != "18" & string_x_value != "19" & string_x_value != "20" & string_x_value != "21" & string_x_value != "22" & string_x_value != "23" & string_x_value != "24" & string_x_value != "25")	//kontroluje jestli je proměnná 1-25
			{	
				clear();
				mini_table("                                                     Zadejte pozici X:");
				writeline(16);
				shell();
				string_x_value = Console.ReadLine();
				x_value = int.Parse(string_x_value);	//převede string na int
				break;		//zastaví cyklus
			}
			//zadání Y
			int y_value = 0;
			clear();
			mini_table("                                                     Zadejte pozici Y:");
			writeline(16);
			shell();
			string string_y_value = Console.ReadLine();
			while(string_y_value != "1" & string_y_value != "2" & string_y_value != "3" & string_y_value != "4" & string_y_value != "5" & string_y_value != "6" & string_y_value != "7" & string_y_value != "8" & string_y_value != "9" & string_y_value != "10" & string_y_value != "11" & string_y_value != "12" & string_y_value != "13" & string_y_value != "14" & string_y_value != "15" & string_y_value != "16" & string_y_value != "17" & string_y_value != "18" & string_y_value != "19" & string_y_value != "20" & string_y_value != "21" & string_y_value != "22" & string_y_value != "23" & string_y_value != "24" & string_y_value != "25")	//kontroluje jestli je proměnná 1-25
			{
				clear();
				mini_table("                                                     Zadejte pozici Y:");
				writeline(16);
				shell();
				string_y_value = Console.ReadLine();
				y_value = int.Parse(string_y_value);	//převede string na int
				break;		//zastaví cyklus
			}
			//volba budovy
			int building_type = 0;
			clear();
			mini_table("                                                   Zadejte typ budovy:");
			writeline(2);
			Console.WriteLine("                            1) AA ubytování     2) [] kovárna     3) <> pila     4) ÷÷ dílna");
			writeline(13);
			shell();
			string string_building_type = Console.ReadLine();
			while(string_building_type != "1" & string_building_type != "2" & string_building_type != "3" & string_building_type != "4")	//kontroluje jestli je proměnná 1-4
			{
				clear();
				mini_table("                                                   Zadejte typ budovy:");
				writeline(2);
				Console.WriteLine("                            1) AA ubytování     2) [] kovárna     3) <> pila     4) ÷÷ dílna");
				writeline(13);
				shell();
				string_building_type = Console.ReadLine();
				building_type = int.Parse(string_building_type);	//převede string na int
				break;		//zastaví cyklus
			}
			
			//pokud je dostatek peněz
			if(player.material >= 50)
			{
				//kotrola jestli je plné pole
				if(vlg[x_value,y_value] == "██")
				{
					//postavení budovy
					clear();
					if(building_type == 1)
					{
						vlg[x_value,y_value] = "AA";	//uytování
					}
					if(building_type == 2)
					{
						vlg[x_value,y_value] = "[]";	//kovárna
					}
					if(building_type == 3)
					{
						vlg[x_value,y_value] = "<>";	//pila
					}
					if(building_type == 4)
					{
						vlg[x_value,y_value] = "÷÷";	//dílna
					}
					player.material -= 50;		//odečtení materiálu
					writeline(12);
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.Write("                                               ");
					text("Budova postavena. -50 materiálu!");
					writeline(17);
					Console.ReadKey(true);
					village_build_menu(player,enemy,blackridge);
			}
			else
			{
			//obsazeno
			clear();
			writeline(12);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("                                                        ");
			text("Pole obsazeno.");
			writeline(17);
			Console.ReadKey(true);
			village_build_menu(player,enemy,blackridge);
			}
		}
		else
		{
			//nedostatek materiálu
			clear();
			writeline(12);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("                                                     ");
			text("Nedostatek materiálu.");
			writeline(17);
			Console.ReadKey(true);
			village_build_menu(player,enemy,blackridge);
		}
		}
		
		//OSTATNÍ (fight, dialogy atd)	
		public static void help(hero player, entity enemy, buildings blackridge)
		{
			clear();
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine(" Manuál:");
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(" [1] Budovy");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" Jsou čtyři typy budov: AA - domy    [] - kovárny    <> - pily    ÷÷ - dílny");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" Každý dům má kapacitu čtyř obyvatel. Na každou kovárnu, pilu nebo dílnu musí být jeden dům, nebo nebude produktivní.");
			Console.WriteLine(" Každý jeden cyklus (boj) vygeneruje každá jedna kovárna, pila nebo dílna 10 $. Staví se pomocí zadání souřadnice X");
			Console.WriteLine(" a Y. Dál se zadá typ budovy. Každá stavba stojí 50 materiálu. Materiál se získává zabíjením nepřátel.");
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(" [2] Boj");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" V boji jsou čtyři možnosti interakce: Fight (útok), Heal (léčení), Mercy (milost) a Run (útěk). Celý boj začíná");
			Console.WriteLine(" vaším tahem, po kterém následuje tak protivníka. První možnost Fight zaútočí na nepřítele a odečte mu HP podle");
			Console.WriteLine(" síl vašeho útoku. Nepřítel může nějaké poškození vyblokovat, záleží na jeho Defense (obraně). To platí i když");
			Console.WriteLine(" útočí on na vás. Druhá možnost Heal vám přidá maximální HP, můžete ji použít celkem třikrát během boje. Třetí");
			Console.WriteLine(" Mercy dává šanci na to, že váš nepřítel nechá na pokoji. Poslední Run dává šanci na to, že nepříteli uniknete. Za");
			Console.WriteLine(" zabití nepřítele získáváte materiál. Za každého poraženého nepřítele vygeneruje každá funkční budova peníze.");
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(" [3] Vylepšování");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" Ve vylepšovacím menu lze za peníze generované městem koupit levely. S každým levelem se postavě vylepší maximální");
			Console.WriteLine(" zdraví a útok. Při posledním levelu se vylepší obrana. Levely stojí 500 a 1000 $. Maximální level je 3.");
			Console.WriteLine("");
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(" [4] Bossfight");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" Závěrečný boj, ve kterém se pomstíte čarodějnici, která vás praštila v lese do hlavy a naplníte tak poslání");
			Console.WriteLine(" celého příběhu. Odemkne se, až bude mít hráč level 3.");
			writeline(6);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Stiskněte libovolnou klávesu pro návrat.");
			Console.ReadKey(true);
			clear();
			village_menu(player,enemy,blackridge);
		}

		public static void major_dialog1(hero player)		//dialog se starostou
		{
			sound("\\sound\\sfx\\night.wav", 1);
			clear();
			Console.ForegroundColor = ConsoleColor.Gray;
			writeline(12);
			Console.Write("                                                          ");
			text("0 HP...");
			writeline(17);
			Console.ReadKey(true);
			clear();
			writeline(12);
			Console.Write("                                               ");
			text("Před očima se ti zatemnilo...");
			writeline(17);
			Console.ReadKey(true);
			clear();
			writeline(12);
			Console.Write("                                                          ");
			text("Tma...");
			writeline(17);
			Console.ReadKey(true);
			clear();
			writeline(12);
			Console.Write("                                                     ");
			text("Ale neumíráš...");
			writeline(17);
			Console.ReadKey(true);
			clear();
			writeline(12);
			Console.Write("                                                          ");
			text("Co to?");
			writeline(17);
			Console.ReadKey(true);
			major_ui(" Brej den, já jsem starosta místní osady.");
			if(player.gender == 1)
            {
            major_ui(" Víš žes tam málem umřel, kdybych tam nebyl?");
            }
			if(player.gender == 2)
            {
            major_ui(" Víš žes tam málem umřela, kdybych tam nebyl?");
          	major_ui(" Neměla bys po lese chodit sama.");
            }
           	if(player.gender == 3)
            {
            major_ui(" Víš žes tam málem umřelo, kdybych tam nebyl?");
            major_ui(" Ehm.");
            major_ui(" Nevím co jsi zač, ale málem bylo po tobě.");
            }
			major_ui(" Už jsem se zmiňoval o tom, jak je to tady nebezpečný?");
           	major_ui(" Bejt tady starosta je docela vopruz. Tady člověk nemůže mít klid.");
            major_ui(" A pak ta pitomá čarodějnice co tě málem zabila.");
            major_ui(" Ty nemluvíš?");
            major_ui(" *Snažíš se odpovědět, ale jsi němé individuum.*");
            major_ui(" *Proto tě přece opustil tvůj otec.*");
           	major_ui(" Jak se jmenuješ?");
            major_ui(" *Snažíš se odpovědět, ale jsi přece němé individuum.*");
            major_ui(" Konečně přišla moje šance!");
            major_ui(" Tady máš papír: *Starosta vytáhl jakýsi pochybný kus papíru, vzal ti ruku a inkoustem obtiskl tvůj palec místo podpisu*");
            clear();
            major_smile();
			line_friendly();
            text(" Teď jsi oficiálně starosta.");
            writeline(6);
            Console.ReadKey(true);
           	clear();
            major_smile();
			line_friendly();
            text(" *Starosta se usmál a vmžiku zmizel neznámo kam.*");
            writeline(6);
            Console.ReadKey(true);
            clear();
            writeline(21);
            line_friendly();
            text(" *Z dálky se ještě ozvalo: Ta osada se jmenuje Blackridge!*");
            writeline(6);
            Console.ReadKey(true);
            clear();
            mini_table("                                             Blackridge získalo nového vůdce!");
            writeline(17);
            Console.ReadKey(true);
            clear();
            mini_table("                                                Dobrodružství právě začíná!");
            writeline(17);
            Console.ReadKey(true);
            clear();
            writeline(12);
            Console.Write("                  ");
            text("Začíná tvá epická cesta za pomstou čarodějnici, která tě praštila v lese do hlavy.");
            writeline(17);
            Console.ReadKey(true);
            clear();
		}
		
		public static void fight(hero player , entity enemy, buildings blackridge)	//univerzální fight
		{
			//nastaví hudbu ve village_menu na false, aby mohla začít znovu hrát
			blackridge.music = false;
			//u_fight_ui, enemy_fight_ui a if podmínky na vykreslení enemy musí mít hodnoty enemy_type
			clear();
			Random number = new Random();
			//random typ enemy
			int enemy_type = number.Next(1,10);	//drak, ogr, kentaur, skeleton
			//typ drak
			if(enemy_type == 1 | enemy_type == 3)
			{	
				int dragon_name = number.Next(1,6);
				if(dragon_name == 1 | dragon_name == 2)
				{
					enemy.name = "Drak Alfred";
				}
				if(dragon_name == 3 | dragon_name == 4)
				{
					enemy.name = "Drak Morgan";
				}
				if(dragon_name == 5 | dragon_name == 6)
				{
					enemy.name = "Dračice Katherine";
				}
				enemy.hp = number.Next(25,30);
				enemy.at = number.Next(10,15);
				enemy.df = number.Next(4,6);
			}
			//typ ogr
			if(enemy_type == 2 | enemy_type == 4 | enemy_type == 6)
			{	
				int ogre_name = number.Next(1,6);
				if(ogre_name == 1 | ogre_name == 2)
				{
					enemy.name = "Ogr Declan";
				}
				if(ogre_name == 3 | ogre_name == 4)
				{
					enemy.name = "Ogr Hugo";
				}
				if(ogre_name == 5 | ogre_name == 6)
				{
					enemy.name = "Ogr Joel";
				}
				enemy.hp = number.Next(22,25);
				enemy.at = number.Next(10,12);
				enemy.df = number.Next(4,6);
			}
			//typ kentaur
			if(enemy_type == 5 | enemy_type == 7)
			{	
				int ogre_name = number.Next(1,6);
				if(ogre_name == 1 | ogre_name == 2)
				{
					enemy.name = "Kentaur Madison";
				}
				if(ogre_name == 3 | ogre_name == 4)
				{
					enemy.name = "Kentaur Diacalla";
				}
				if(ogre_name == 5 | ogre_name == 6)
				{
					enemy.name = "Kentaur Ivan";
				}
				enemy.hp = number.Next(24,26);
				enemy.at = number.Next(10,13);
				enemy.df = number.Next(4,6);
			}
			//typ skeleton
			if(enemy_type == 6 | enemy_type == 9)
			{	
				enemy.name = "Kostlivec";
				enemy.hp = number.Next(20,24);
				enemy.at = number.Next(10,12);
				enemy.df = number.Next(4,6);
			}
			//typ wolf
			if(enemy_type == 8 | enemy_type == 10)
			{	
				enemy.name = "Vlk";
				enemy.hp = number.Next(15,20);
				enemy.at = number.Next(8,12);
				enemy.df = number.Next(5,6);
			}
			//hudba
			int music = number.Next(1,4);
			if(music == 1)
			{
				sound("\\sound\\music\\fight1.wav", 1);
			}
			if(music == 2)
			{
				sound("\\sound\\music\\fight2.wav", 1);
			}
			if(music == 3)
			{
				sound("\\sound\\music\\fight3.wav", 1);
			}
			if(music == 4)
			{
				sound("\\sound\\music\\fight4.wav", 1);
			}
			//fight
			while(player.hp > 0 & enemy.hp > 0)		//cyklus platící dokud player nebo monikalda hp nejsou 0
			{
				int action;
				while(true)		//ošetření
				{
					u_fight_ui(player, enemy, enemy_type);
					string string_action = Console.ReadLine();
					if(Regex.IsMatch(string_action, "^[1-4]$|^3011$"))	//kontroluje jestli je proměnná 1-4
					{
						action = int.Parse(string_action);	//převede string na int
						break;		//zastaví cyklus
					}
				}
				//akce boje
				switch(action)
				{
					case 1:		//attack
					//ošetření bugu
					if(enemy.df < player.at)
					{
						enemy.hp -= player.at;
						enemy.hp += enemy.df;
						enemy_fight_ui(" " + enemy.name + " je pod útokem! -" + player.at + " HP! " + enemy.df + " HP vyblokováno!", 2, enemy_type, enemy);
					}
					if(enemy.df > player.at)
					{
						//nic se nestane
						enemy_fight_ui(" Žádné poškození!", 1, enemy_type, enemy);
					}
					break;
					
					case 2:		//heal
					//nedostatek healu
					if(player.heal <= 0 )
					{
					enemy_fight_ui(" Nemáš se čím healovat! Lékarničky nerostou na stromech.", 3, enemy_type, enemy);
					}
					
					//dostatek healu
					if(player.heal > 0)
					{
						//pokud má player max hp
						if(player.hp == player.maxhp)
						{
							enemy_fight_ui(" Již máš maximální HP!", 3, enemy_type, enemy);
						}
						//pokud má player méně než player.maxhp
						if(player.hp != player.maxhp)
						{
						player.heal -= 1;
						player.hp = player.maxhp;
						enemy_fight_ui(" Nyní máš maximum " + player.maxhp + " HP! Můžeš se dohealovat ještě " + player.heal + "x.", 2, enemy_type, enemy);
						}
					}
					break;
				
					case 3:		//mercy
					int mercy = number.Next(1,3);		//šance 1:3 na milost
					enemy_fight_ui(" *Zkoušíš nepřítele přesvědčit znakovou řečí o tom, že sdílíš ideologii hippies o světovém míru a lásce.*", 3, enemy_type, enemy);
					if(mercy == 1)		//šance 1:3 na mercy
					{
						sound("\\sound\\sfx\\win.wav", 2);
						enemy_fight_ui(" " + enemy.name + " chápe, že existence je cenný dar. Nechává tě být. + 10 materiálu!", 2, enemy_type, enemy);
						player.material += 10;		//přičtení materiálu
						money(player,blackridge);		//přičtení peněz
						vlg[10,26] = "|      Materiál: " + player.material + "";		//je třeba aktualizovat pole s materiálem
						village_menu(player,enemy,blackridge);
					}
					else
					{
						enemy_fight_ui(" Dělat psí oči na nepřítele není dobrý nápad.", 1, enemy_type, enemy);
					}
					break;
					
					case 4:		//run
					int run = number.Next(1,4);		//šance 1:4 na útěk
					enemy_fight_ui(" *Snažíš se utéct.*", 3, enemy_type, enemy);
					if(run == 1)		//šance 1:4 na útěk
					{
						sound("\\sound\\sfx\\win.wav", 2);
						enemy_fight_ui(" Jsi srab a podařilo se ti utéct. + 0 materiálu!", 2, enemy_type, enemy);
						village_menu(player,enemy,blackridge);
					}
					else
					{
						enemy_fight_ui(" Nepříteli neutečeš. " + player.name + " není zdatný bežec. Navíc tlustý.", 1, enemy_type, enemy);
					}
					break;
					
					case 3011:
					//cheat
					sound("\\sound\\sfx\\win.wav", 2);
					enemy_fight_ui(" VÝHRA! +20 materiálu!", 2, enemy_type, enemy);
					player.material += 20;		//přičtní materiálu
					money(player,blackridge);		//přičtení peněz
					player.hp = player.maxhp;		//max HP
					player.heal = 3;
					vlg[10,26] = "|      Materiál: " + player.material + "";		//je třeba aktualizovat pole s materiálem
					village_menu(player,enemy,blackridge);
					break;
				}
				//po fightu
				//výhra
				if(enemy.hp <= 0)
				{
					sound("\\sound\\sfx\\win.wav", 2);
					enemy_fight_ui(" VÝHRA! +20 materiálu!", 2, enemy_type, enemy);
					player.material += 20;		//přičtní materiálu
					money(player,blackridge);		//přičtení peněz
					player.hp = player.maxhp;		//max HP
					player.heal = 3;
					vlg[10,26] = "|      Materiál: " + player.material + "";		//je třeba aktualizovat pole s materiálem
					village_menu(player,enemy,blackridge);
				}
				//tah enemy, pokud žije
				if(enemy.hp > 0)
				{
					enemy_fight_ui(" " + enemy.name + " útočí!", 1, enemy_type, enemy);
					//ošetření bugu, kdy je player.df větší než enemy.at a player dostává nesmyslně HP
					if(player.df < enemy.at)
					{
						player.hp -= enemy.at;
						player.hp += player.df;
						enemy_fight_ui(" -" + enemy.at + " HP! " + player.df + " HP vyblokováno!", 1, enemy_type, enemy);
					}
					if(player.df > enemy.at)
					{
						//nic se nestane 
						enemy_fight_ui(" Žádné poškození!", 1, enemy_type, enemy);
					}
					
				}
			}
			//konec cyklu
			//pokud je hráč mrtvý
			if(player.hp <= 0)
			{
				sound("\\sound\\sfx\\death.wav", 2);
				enemy_fight_ui(" 0 HP!", 1, enemy_type, enemy);
				writeline(12);
				Console.Write("                                       ");
				text(" Konec hry! Jsi pro mě velkým zklamáním.");
				writeline(17);
				Console.ReadKey(true);
				Environment.Exit(0);
			}
		}
		
		public static void monfight(hero player , entity enemy, buildings blackridge, int playing)		//monfight, volat s playing 1, pokud je to první fight (nezobrazí se death)
		{
			//proměnná pro zapamatování prvního souboje
			int play = playing;
			//nastaví hudbu ve village_menu na false, aby mohla začít znovu hrát
			blackridge.music = false;
			//u_fight_ui, enemy_fight_ui a if podmínky na vykreslení enemy musí mít hodnoty enemy_type
			int enemy_type = 11;
			//hudba
			sound("\\sound\\music\\mon2.wav", 1);
			//statistiky enemy
			enemy.name = "Monikalda";
			enemy.hp = 100;
			enemy.at = 15;
			enemy.df = 7;
			clear();
			//fight
			while(player.hp > 0 & enemy.hp > 0)		//cyklus platící dokud player nebo monikalda hp nejsou 0
			{
				int action;
				while(true)		//ošetření
				{
					u_fight_ui(player, enemy, enemy_type);
					string string_action = Console.ReadLine();
					if(Regex.IsMatch(string_action, "^[1-4]$|^3011$"))	//kontroluje jestli je proměnná 1-4
					{
						action = int.Parse(string_action);	//převede string na int
						break;		//zastaví cyklus
					}
				}
				//akce boje
				switch(action)
				{
					case 1:		//attack
					//ošetření bugu
					if(enemy.df < player.at)
					{
						enemy.hp -= player.at;
						enemy.hp += enemy.df;
						enemy_type = 12;
						enemy_fight_ui(" " + enemy.name + " dostala dělo do ksichtu! -" + player.at + " HP! " + enemy.df + " HP vyblokováno!", 2, enemy_type, enemy);
						enemy_type = 11;
					}
					if(enemy.df > player.at)
					{
						//nic se nestane
						enemy_fight_ui(" Žádné poškození!", 1, enemy_type, enemy);
					}
					break;
					
					case 2:		//heal
					//nedostatek healu
					if(player.heal <= 0 )
					{
					enemy_fight_ui(" Nemáš se čím healovat! Lékarničky nerostou na stromech.", 3, enemy_type, enemy);
					}
					
					//dostatek healu
					if(player.heal > 0)
					{
						//pokud má player max hp
						if(player.hp == player.maxhp)
						{
							enemy_fight_ui(" Již máš maximální HP!", 3, enemy_type, enemy);
						}
						//pokud má player méně než player.maxhp
						if(player.hp != player.maxhp)
						{
						player.heal -= 1;
						player.hp = player.maxhp;
						enemy_fight_ui(" Nyní máš maximum " + player.maxhp + " HP! Můžeš se dohealovat ještě " + player.heal + "x.", 2, enemy_type, enemy);
						}
					}
					break;
				
					case 3:		//mercy
					enemy_fight_ui(" *Zkoušíš nepřítele přesvědčit znakovou řečí o tom, že sdílíš ideologii hippies o světovém míru a lásce.*", 3, enemy_type, enemy);
					enemy_fight_ui(" *Monikalda to ale nechápe, protože je úplně blbá.*", 3, enemy_type, enemy);
					break;
					
					case 4:		//run
					enemy_fight_ui(" *Snažíš se utéct.*", 3, enemy_type, enemy);
					enemy_fight_ui(" Neutečeš hloupé čarodějnici.", 3, enemy_type, enemy);
					break;
					
					case 3011:
					//cheat
					if(play == 1)		//fix bugu - pokud hráč hraje poprvé a zadá 3011 tak vyhraje první fight, ale ne celou hru
					{
						 enemy.hp = 0;
					}
					else
					{
						blackridge.win = true;
						sound("\\sound\\music\\end.wav", 1);
						enemy_fight_ui(" VÝHRA! Monikalda je mrtvá!", 2, enemy_type, enemy);
						clear();
						writeline(12);
						Console.Write("                 ");
						text("Zde končí tvá epická cesta za pomstou čarodějnici, která tě praštila v lese do hlavy.");
						writeline(17);
						Console.ReadKey(true);
						clear();
						writeline(12);
						Console.Write("                         ");
						text("Za dohrání této rozbité hry máš můj obdiv a obrovský dík. -Josef Ráž");
						writeline(17);
						Console.ReadKey(true);
						village_menu(player,enemy,blackridge);
					}
					break;
				}
				//po fightu
				//výhra
				if(enemy.hp <= 0)
				{
					if(play == 1)		//fix bugu - pokud hráč hraje poprvé a zadá 3011 tak vyhraje první fight, ale ne celou hru
					{
						//konec
					}
					else
					{
						blackridge.win = true;
						sound("\\sound\\music\\end.wav", 1);
						enemy_fight_ui(" VÝHRA! Monikalda je mrtvá! Hra dokončena!", 2, enemy_type, enemy);
						clear();
						writeline(12);
						Console.Write("                 ");
						text("Zde končí tvá epická cesta za pomstou čarodějnici, která tě praštila v lese do hlavy.");
						writeline(17);
						Console.ReadKey(true);
						clear();
						writeline(12);
						Console.Write("                         ");
						text("Za dohrání této rozbité hry máš můj obdiv a obrovský dík. -Josef Ráž");
						writeline(17);
						Console.ReadKey(true);
						village_menu(player,enemy,blackridge);
					}
				}
				//tah enemy, pokud žije
				if(enemy.hp > 0)
				{
					enemy_fight_ui(" " + enemy.name + " útočí!", 1, enemy_type, enemy);
					//ošetření bugu, kdy je player.df větší než enemy.at a player dostává nesmyslně HP
					if(player.df < enemy.at)
					{
						player.hp -= enemy.at;
						player.hp += player.df;
						enemy_fight_ui(" -" + enemy.at + " HP! " + player.df + " HP vyblokováno!", 1, enemy_type, enemy);
					}
					if(player.df > enemy.at)
					{
						//nic se nestane
						enemy_fight_ui(" Žádné poškození!", 1, enemy_type, enemy);
					}
					
				}
			}
			//konec cyklu
			if(play != 1)
			{
				//pokud je hráč mrtvý
				if(player.hp <= 0)
				{
					sound("\\sound\\sfx\\death.wav", 2);
					enemy_fight_ui(" 0 HP!", 1, enemy_type, enemy);
					writeline(12);
					Console.Write("                                       ");
					text(" Konec hry! Jsi pro mě velkým zklamáním.");
					writeline(17);
					Console.ReadKey(true);
					Environment.Exit(0);
				}
			}
		}
		
		public static void Main(string[] args)		//main
		{
			//velikost
			Console.SetWindowSize(120,30);
			//vytvoření objektů
			hero player = new hero("",0,20,20,10,5,1,0,0,0,3);		//name, gender, hp, maxhp, at, df, lvl, money, material, heal
			buildings blackridge = new buildings(0, 0, 0, 0,0,false,false);	//obyvatelé ubytování kovárny pily dílny
			entity enemy = new entity("",0,0,0);
			first();
			menu();
			//pohlaví
			while(true)		//ošetření
			{
				clear();
				mini_table("                                               Je tvůj hrdina muž nebo žena?");
				writeline(3);
				Console.WriteLine("                                       1 - muž          2 - žena           3 - jiná");
				writeline(12);
				shell();
				//ošetření
				string gender = Console.ReadLine();		//zadání pohlaví
				if(Regex.IsMatch(gender, "^[1-3]$"))	//kontroluje jestli je proměnná 1-3
				{
					player.gender = int.Parse(gender);	//převede string na int
					break;		//zastaví cyklus
				}
			}	
			//jméno
			clear();
			mini_table("                                                 Jak s jmenuje tvůj hrdina?");
			writeline(16);
			shell();
			player.name = Convert.ToString(Console.ReadLine());
			while(player.name == "")		//ošetření
			{
				clear();
				mini_table("                                                 Jak s jmenuje tvůj hrdina?");
				writeline(16);
				shell();
				 player.name = Convert.ToString(Console.ReadLine());
			}
			//vytvoření hráče
			if(player.name == "3011")		//cheat
			{
				goto game;
			}	
			//začátek hry
			sound("\\sound\\sfx\\night.wav", 1);
			clear();
			Console.ForegroundColor = ConsoleColor.Yellow;
			writeline(13);
			Console.Write("                                                       ");
			text("Je temná noc.");
			writeline(16);
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write("Pro pokračování stiskněte libovolnou klávesu.");
			Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.ReadKey(true);
            clear();
            writeline(13);
           	Console.Write("                                                    ");
	        text("Lokace: Temný hvozd");
	        Console.WriteLine();
	        Console.Write("                                                   ");
	        text(DateTime.Now.AddYears(-666).ToLongDateString());
	        writeline(15);
            Console.ReadKey(true);
            clear();
            writeline(13);
            Console.Write("                                                  ");
            if(player.gender == 1)
            {
            text(player.name + " dostal ránu do hlavy.");
            }
			if(player.gender == 2)
            {
            text(player.name + " dostala ránu do hlavy.");
            }
            if(player.gender == 3)
            {
            text(player.name + " dostalo ránu do hlavy.");
            }
            writeline(16);
            Console.ReadKey(true);
            clear();
            writeline(13);
            Console.Write("                                        ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            text("Čarodějnice Monikalda: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            text("Připrav se na smrt,");
            Console.WriteLine("");
            Console.Write("                                                     ");
            text("muhahahahahaha!");
            writeline(15);
            Console.ReadKey(true);  
            //první fight
            monfight(player,enemy,blackridge,1);
            major_dialog1(player);
			game:
            village_create(player,blackridge);
            //von Blackridge
            int name_length = player.name.Length; 
            player.name = player.name.Insert(name_length, " von Blackridge");
            village_menu(player,enemy,blackridge);
			Console.ReadKey(true);
		}
	}
}