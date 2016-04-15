using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryProject
{
    public static class NumerosRomanosII
    {
        public static string Roman1(int natural)
        {
            var romano = "";

            int centena;
            int decena;
            int unidad;
            var miles = GetMiles(natural, out centena, out decena, out unidad);

            //Miles
            romano += miles == 1 ? "M" :
                      miles == 2 ? "MM" :
                      miles == 3 ? "MMM" : "";
            //Centena
            romano += centena == 1 ? "C" : 
                      centena == 2 ? "CC":
                      centena == 3 ? "CCC":
                      centena == 4 ? "CD":
                      centena == 5 ? "D":
                      centena == 6 ? "DC":
                      centena == 7 ? "DCC":
                      centena == 8 ? "DCCC":
                      centena == 9 ? "CM" : "";
            //Decena
            romano += decena == 1 ? "X" :
                      decena == 2 ? "XX" :
                      decena == 3 ? "XXX" :
                      decena == 4 ? "XL" :
                      decena == 5 ? "L" :
                      decena == 6 ? "LX" :
                      decena == 7 ? "LXX" :
                      decena == 8 ? "LXXX" :
                      decena == 9 ? "XC" : "";
            //unidad
            romano += unidad == 1 ? "I" :
                      unidad == 2 ? "II" :
                      unidad == 3 ? "III" :
                      unidad == 4 ? "IV" :
                      unidad == 5 ? "V" :
                      unidad == 6 ? "VI" :
                      unidad == 7 ? "VII" :
                      unidad == 8 ? "VIII" :
                      unidad == 9 ? "IX" : "";
            
            return romano;
        }
        public static string Roman2(int natural)
        {
           string[][] lista =
           {
               new[] {"","M","MM","MMM"},
               new[] {"","C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"},
               new[] {"","X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"},
               new[] {"","I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"}
           };

           int centena;
           int decena;
           int unidad;
           var miles = GetMiles(natural, out centena, out decena, out unidad);

            return lista[0][miles] + lista[1][centena] + lista[2][decena] + lista[3][unidad];
        }
        public static string Roman3(int natural)
        {
            var romano = "";
            int[] portencia = {1000, 100, 10, 1};

            foreach (int p in portencia)
            {
                var entero = (natural/p)*p;
                natural -= entero;

                if (entero <= 0) continue;

                romano += GetRomans1(entero);
            }

            return romano;
        }
        public static string Roman4(int natural)
        {
            var romano = "";

            var cadena = natural.ToString(CultureInfo.InvariantCulture);

            for (int i = 0, j = cadena.Length - 1; i < cadena.Length; i++, j--)
            {
                var num = Convert.ToInt32(cadena[i].ToString(CultureInfo.InvariantCulture));
                var potencia = (int)Math.Pow(10, j);
                var valor = num * potencia;

                romano += GetRomans1(valor);
            }
            return romano;
        }

        public static string Roman5(int natural)
        {
            return (natural >= 1000
                    ? Roman5(natural, 1000)
                    : (natural >= 100
                        ? Roman5(natural, 100)
                        : (natural >= 10 ? Roman5(natural, 10)
                        : GetRomans1(natural))));
        }
        private static string Roman5(int natural, int potencia)
        {
            var entero = natural / potencia;
            var romano = entero * potencia;
            var resto = natural - romano;

            return GetRomans1(romano) + Roman5(resto);
        }

        public static string Roman6(int natural)
        {
            var romano = "";
            int[] numbers = {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1};

            foreach (var number in numbers)
            {
                while (natural >= number)
                {
                    romano += GetRomans2(number);
                    natural -= number;
                }
            }

            return romano;
        }

        public static string Roman7(int natural) //Convierte de decimal a romano
        {
            List<string> letter = new List<string> { "M", "D", "C", "L", "X", "V", "I" };
            int[] value = { 1000, 500, 100, 50, 10, 5, 1, 0 };

            int meter;
            int rest = natural;
            string roman = "";
            for (int i = 0; i < 7; i++)
            {
                if (rest >= value[i])       //aqui evaluo por q valor iniciar. 
                {                       //El inmediatamente menor o igual al numero de la variable meter en la actialidad
                    meter = rest / value[i];  //Revalorizo meter
                    if (rest >= value[i + i % 2] * (4 + (4 * (i % 2) + i % 2)))//Aqui veo si el numero en cuestion lleva una construccion anormal
                    { //o sea, como en el 4, 9, 40 etc
                        roman += letter[i + (i % 2)] + letter[i - 1]; //aqui pongo el valor mas pequeño antes del mayor para su resta
                        rest += i % 2 * (value[i + 1] - value[i]);   //revalorizo rest en caso de ser necesario
                    }
                    else
                        for (int j = 0; j < meter; j++)  //añado letras q no ayan tenido ninguna dificultad
                            roman += letter[i];
                }
                rest %= value[i];// cambio al siguiente valor
            }
            return roman;
        }

        public static string Roman8(int natural)
        {
            string[] romans = new string[] { "I", "V", "X", "L", "C", "D", "M" };
            string romano = natural.ToString();
            string roman = "";
            //s es un contador
            int s = 4;
            for (int i = 6; i >= 0; i -= 2)
            {
                if (romano.Length == s)
                {
                    var value = romano[0];
                    int back = i;

                    switch (value)
                    {
                        case '1': { roman += romans[i]; } break;
                        case '2': { roman += romans[i] + romans[i]; } break;
                        case '3': { roman += romans[i] + romans[i] + romans[i]; } break;
                        case '4': { roman += romans[i] + romans[i + 1]; } break;
                        case '5': { roman += romans[i + 1]; } break;
                        case '6': { roman += romans[i + 1] + romans[back]; } break;
                        case '7': { roman += romans[i + 1] + romans[back] + romans[back]; } break;
                        case '8': { roman += romans[i + 1] + romans[back] + romans[back] + romans[back]; } break;
                        case '9': { roman += romans[i] + romans[i + 2]; } break;
                        default: { roman += ""; } break;
                    }
                   
                    romano = romano.Remove(0, 1);
                }
                s--;
            }
            return roman;
        }

        private static string GetRomans2(int num)
        {
            switch (num)
            {
                case 1: return "I";
                case 4: return "IV";
                case 5: return "V";
                case 9: return "IX";
                case 10: return "X";
                case 40: return "XL";
                case 50: return "L";
                case 90: return "XC";
                case 100: return "C";
                case 400: return "CD";
                case 500: return "D";
                case 900: return "CM";
                case 1000: return "M";
                default: return "";
            }
        }
        private static string GetRomans1(int num)
        {
            switch (num)
            {
                case 1: return "I";
                case 2: return "II";
                case 3: return "III";
                case 4: return "IV";
                case 5: return "V";
                case 6: return "VI";
                case 7: return "VII";
                case 8: return "VIII";
                case 9: return "IX";
                case 10: return "X";
                case 20: return "XX";
                case 30: return "XXX";
                case 40: return "XL";
                case 50: return "L";
                case 60: return "LX";
                case 70: return "LXX";
                case 80: return "LXXX";
                case 90: return "XC";
                case 100: return "C";
                case 200: return "CC";
                case 300: return "CCC";
                case 400: return "CD";
                case 500: return "D";
                case 600: return "DC";
                case 700: return "DCC";
                case 800: return "DCCC";
                case 900: return "CM";
                case 1000: return "M";
                case 2000: return "MM";
                case 3000: return "MMM";
                default: return "";
            }
        }
        private static int GetMiles(int natural, out int centena, out int decena, out int unidad)
        {
            var miles = natural / 1000;
            var resto = natural % 1000;
            centena = resto / 100;
            resto = resto % 100;
            decena = resto / 10;
            resto = resto % 10;
            unidad = resto;
            return miles;
        }
       

        //----------------------------------------------------
        public static int Number1(string romano)
        {
            var total = 0; var number = 0;

            foreach (var numberActual in romano.Select(GetNumber))
            {
                if (number == 0)
                {
                    number = numberActual;
                    continue;
                }

                if (number < numberActual)
                    number = -number;

                total += number;
                number = numberActual;
            }

            return total + number;
        }
        public static int Number2(string romano)
        {
            var array = new int[romano.Length];

            for (var i = 0; i < romano.Length; i++)
            {
                var n1 = GetNumber(romano[i]);
                
                if (i == 0)
                {
                    array[i] = n1;
                    continue;
                }

                var n0 = array[i - 1];

                if (n0 < n1)
                    array[i - 1] = -n0;

                array[i] = n1;
            }

            return array.Sum();
        }
        public static int Number3(string romano)
        {
            var letter = new List<string> { "M", "D", "C", "L", "X", "V", "I" };
            int[] value = { 1000, 500, 100, 50, 10, 5, 1, 0 };

            romano = romano.ToUpper();
            if (letter.Contains(romano[romano.Length - 1].ToString()) == false)
                return 0;
            var number = value[letter.IndexOf(romano[romano.Length - 1].ToString())];
            if (romano.Length > 1)
                for (var i = romano.Length - 2; i >= 0; i--)
                {
                    if (letter.Contains((romano[i].ToString())))
                    {
                        if (value[letter.IndexOf((romano[i].ToString()))] < value[letter.IndexOf((romano[i + 1].ToString()))])
                            number -= value[letter.IndexOf((romano[i].ToString()))];                 
                        else                                                                                         
                            number += value[letter.IndexOf((romano[i].ToString()))];                   
                    }
                    else
                        return 0;
                }
            return number;
        }
        public static int Number4(string romano)
        {
            var number = 0;
            var back = 0;

            for (var j = romano.Length - 1; j >= 0; j--)
            {
                int compare = GetNumber(romano[j]);
                if (compare < back)
                    number -= compare;
                else
                    number += compare;
                back = compare;
            }
            return number;
        }

        private static int GetNumber(char roman)
        {
            return roman == 'M' ? 1000 :
                   roman == 'D' ? 500 :
                   roman == 'C' ? 100 :
                   roman == 'L' ? 50 :
                   roman == 'X' ? 10 :
                   roman == 'V' ? 5 :
                   roman == 'I' ? 1 : 0;
        }
    }
}
