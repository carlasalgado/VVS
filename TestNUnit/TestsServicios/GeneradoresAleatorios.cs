using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.TestNUnit.TestsServicios
{
    public static class GeneradoresAleatorios
    {
        public static string RandomString(int maxLength = 80)
        {
            return Arb.Generate<string>().Sample(maxLength, 1).ToString();
        }
        public static string randomDistinctString(List<string> cadenasRepetidas, int maxLength = 80)
        {

            string s = RandomString(maxLength);

            if (cadenasRepetidas.Contains(s))
            {
                return randomDistinctString(cadenasRepetidas, maxLength);
            }
            else
            {
                cadenasRepetidas.Add(s);
                return s;
            }
        }


        public static Int32 RandomInteger()
        {
            string s = Arb.Generate<Int32>().Sample(10, 1).ToString();
            if (!string.IsNullOrEmpty(s))
            {
                s = s.Replace('[', ' ');
                s = s.Replace(']', ' ');
                s = s.Replace('-', ' ');
                s = s.Trim();
                return Int32.Parse(s);
            }
            else
            {
                return RandomInteger();
            }
        }

    }
}
