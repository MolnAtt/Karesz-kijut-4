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
			Vektor[] kijáratok = new Vektor[]
			{
				new Vektor(0, 15),
				new Vektor(40, 15),
				new Vektor(20, 0),
				new Vektor(20, 30),
			};
			Vektor kijárat = kijáratok[r.Next(4)];
			pálya[kijárat] = 0;

			List<Vektor> nyulvanyok_fent = Pozíciók_Vízszintes(0).OrderBy(x => r.Next()).Take(r.Next(2, 5)).ToList();
			List<Vektor> nyulvanyok_jobbra = Pozíciók_Függőleges(39).OrderBy(x => r.Next()).Take(r.Next(2, 7)).ToList();
			List<Vektor> nyulvanyok_lent = Pozíciók_Vízszintes(29).OrderBy(x => r.Next()).Take(r.Next(2, 5)).ToList();
			List<Vektor> nyulvanyok_balra = Pozíciók_Függőleges(0).OrderBy(x => r.Next()).Take(r.Next(2,7)).ToList();

			Nyulvanyrajzolas(pálya, nyulvanyok_fent, new Vektor(0, 1));
			Nyulvanyrajzolas(pálya, nyulvanyok_jobbra, new Vektor(-1, 0));
			Nyulvanyrajzolas(pálya, nyulvanyok_lent, new Vektor(0, -1));
			Nyulvanyrajzolas(pálya, nyulvanyok_fent, new Vektor(1, 0));

			Frissít();
			
		}

		void Nyulvanyrajzolas(Pálya pálya, List<Vektor> nyulvanyok, Vektor első_irany)
		{
			
		}
	}
}