using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tehtava4
{
    class Pelaaja
    {
        #region Variables
        private string etunimi;
        private string sukunimi;
        private string seura;
        private int hinta;
        #endregion
        #region Properties
        public string Etunimi
        {
            get
            {
                return etunimi;
            }
            set
            {
                etunimi = value;
            }
        }
        public string Sukunimi
        {
            get
            {
                return sukunimi;
            }
            set
            {
                sukunimi = value;
            }
        }
        public string Seura
        {
            get
            {
                return seura;
            }
            set
            {
                seura = value;
            }
        }
        public int Hinta
        {
            get
            {
                return hinta;
            }
            set
            {
                hinta = value;
            }
        }
        public string KokoNimi
        {
            get
            {
                return (etunimi + " " + sukunimi);
            }
        }
        public string EsitysNimi
        {
            get
            {
                return (KokoNimi + ", " + seura);
            }
        }
        #endregion
        #region Constructors
        public Pelaaja(string etu, string suku, string seura, int hinta)
        {
            this.etunimi = etu;
            this.sukunimi = suku;
            this.seura = seura;
            this.hinta = hinta;
        }
        #endregion
        #region Methods
        public static void SaveToFile(List<Pelaaja> pelaajat)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.FileName = "Pelaajat";
            dialog.DefaultExt = ".txt";
            dialog.Filter = "Text documents (.txt)|*.txt";

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                StreamWriter writer = File.AppendText(dialog.FileName);
                foreach(var pelaaja in pelaajat)
                {
                    writer.WriteLine(pelaaja);
                }
                writer.Close();
            }
        }
        public override string ToString()
        {
            return EsitysNimi;
        }
        #endregion


    }
}
