using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace JAMK.IT.IIO11300
{
  [Serializable]
  public class MittausData
  {
        #region Variables
        private string kello;
        #endregion
        #region Properties
        public string Kello
        {
          get { return kello; }
          set { kello = value; }
        }
        private string mittaus;

        public string Mittaus
        {
          get { return mittaus; }
          set { mittaus = value; }
        }
        #endregion
        #region CONSTRUCTORS
        //luokalle tehdään kaksi konstruktoria
        public MittausData()
        {
          kello = "0:00";
          mittaus = "empty";
        }
        public MittausData(string klo, string mdata)
        {
          this.kello = klo;
          this.mittaus = mdata;
        }
        #endregion
        #region Methods
        //ylikirjoitetaan ToString
        public override string ToString()
        {
          //return base.ToString();
          return kello + "=" + mittaus;
        }
        //tiedoston käsittely
        public static void SaveToFileV2(string filename, List<MittausData> mittaukset)
        {
            try
            {
                StreamWriter writer = File.AppendText(filename);
                foreach (var item in mittaukset)
                {
                    writer.WriteLine(item);
                }
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static List<MittausData> ReadFromFile(string filename)
        {
            try
            {
                if(File.Exists(filename))
                {
                    //luetaan tiedosto ja muutetaan Mittausdata-olioksi
                    MittausData data;
                    List<MittausData> luetut = new List<MittausData>();
                    string rivi = "";
                    StreamReader reader = File.OpenText(filename);
                    while((rivi = reader.ReadLine()) != null)
                    {
                        if(rivi.Length > 3 && rivi.Contains("="))
                        {
                            string[] split = rivi.Split(new char[] { '=' });
                            //alimerkkijonoista luodaan olio
                            data = new MittausData(split[0], split[1]);
                            luetut.Add(data);
                        }
                    }
                    reader.Close();
                    //palautetaan muuttuja
                    return luetut;
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
