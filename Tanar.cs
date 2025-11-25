using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;


namespace Karesz
{
	public partial class Form1 : Form
	{
		static Random r = new Random();
		string betöltendő_pálya = "palya01.txt";

		IEnumerable<Vektor> Pozíciók_Vízszintes(int y) => Enumerable.Range(1, 40).Select(x => new Vektor(x, y));
		IEnumerable<Vektor> Pozíciók_Függőleges(int x) => Enumerable.Range(1, 30).Select(y => new Vektor(x, y));


		void TANÁR_ROBOTJAI()
		{
			Robot karesz = new Robot("Karesz", 1000, 1000, 1000, 1000, 0, 20, 15, r.Next(4));
			Betölt(betöltendő_pálya);

			List<Vektor> nyulvanyok_fent = Pozíciók_Vízszintes(0).OrderBy(x => r.Next()).Take(r.Next(5, 10)).ToList();
			List<Vektor> nyulvanyok_jobbra = Pozíciók_Függőleges(40).OrderBy(x => r.Next()).Take(r.Next(5, 10)).ToList();
			List<Vektor> nyulvanyok_lent = Pozíciók_Vízszintes(30).OrderBy(x => r.Next()).Take(r.Next(5, 10)).ToList();
			List<Vektor> nyulvanyok_balra = Pozíciók_Függőleges(0).OrderBy(x => r.Next()).Take(r.Next(5,10)).ToList();

			Nyulvanyrajzolas(pálya, nyulvanyok_fent);
			Nyulvanyrajzolas(pálya, nyulvanyok_jobbra);
			Nyulvanyrajzolas(pálya, nyulvanyok_lent);
			Nyulvanyrajzolas(pálya, nyulvanyok_balra);

			HashSet<Vektor> foglaltak = nyulvanyok_balra.Union(nyulvanyok_fent.Union(nyulvanyok_jobbra.Union(nyulvanyok_lent))).ToHashSet();

			HashSet<Vektor> pozíciók = Pozíciók_Vízszintes(0).Union(Pozíciók_Vízszintes(30).Union(Pozíciók_Függőleges(0).Union(Pozíciók_Függőleges(40)))).ToHashSet();
			HashSet<Vektor> sarkok = new HashSet<Vektor> 
			{ 
				new Vektor(0, 0), 
				new Vektor(40, 0), 
				new Vektor(0, 30), 
				new Vektor(40, 30), 
			};

			foreach (Vektor foglalt in foglaltak)
			{
				pozíciók.Remove(foglalt);
			}

			foreach (Vektor sarok in sarkok)
			{
				pozíciók.Remove(sarok);
			}

			Vektor kijárat = pozíciók.ToList().OrderBy(x => r.Next()).First();

			pálya[kijárat] = 0;

			Frissít();
			
		}

		int Hossz(Vektor p)
		{
			if (p.Y == 0 || p.Y == 30)
			{
				if (p.X < 20)
					return p.X - 2;
				else
					return 40 - p.X - 2;
			}
			else if (p.X == 0 || p.X == 40)
			{
				if (p.Y < 15)
					return p.Y - 2;
				else
					return 30 - p.Y - 2;
			}
			else return -1;
		}

		void Nyulvanyrajzolas(Pálya pálya, List<Vektor> nyulvanyok)
		{
			foreach (Vektor nyulvany in nyulvanyok)
			{
				int hossz = Math.Max(1,Math.Min(Hossz(nyulvany), 14));
				Vektor pont = nyulvany;

				Vektor irány;

				if (nyulvany.X == 0)
				{
					irány = new Vektor(1, 0);
				}
				else if (nyulvany.X == 40)
				{
					irány = new Vektor(-1, 0);
				}
				else if (nyulvany.Y == 0)
				{
					irány = new Vektor(0, 1);
				}
				else if (nyulvany.Y == 30)
				{
					irány = new Vektor(0, -1);
				}
				else {
					irány = new Vektor(-1, -1);
				}

				for (int i = 0; i < hossz; i++)
					{
						pálya[pont] = 1;
						pont += irány;
					}
			}
		}
	}
}