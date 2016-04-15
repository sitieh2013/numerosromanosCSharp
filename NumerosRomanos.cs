using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LibraryProject
{
    public static class NumerosRomanos
    {
        public static string ConvertRomanoRecursivo(int numero)
        {
            return (numero >= 1000
                    ? ConvertRomanoRecursivo(numero, 1000)
                    : (numero >= 100
                        ? ConvertRomanoRecursivo(numero, 100)
                        : (numero >= 10 ? ConvertRomanoRecursivo(numero, 10)
                        : RomanosNomenclador(numero))));
        }
        private static string ConvertRomanoRecursivo(int numero, int potencia)
        {
            var entero = numero / potencia;
            var romano = entero * potencia;
            var resto = numero - romano;

            return RomanosNomenclador(romano) + ConvertRomanoRecursivo(resto);
        }

        public static string ConvertRomanoIteracion(int numero)
        {
            var romano = "";

            var cadena = numero.ToString(CultureInfo.InvariantCulture);

            for (int i = 0, j = cadena.Length - 1; i < cadena.Length; i++, j--)
            {
                var num = Convert.ToInt32(cadena[i].ToString(CultureInfo.InvariantCulture));
                var potencia = (int)Math.Pow(10, j);
                var valor = num * potencia;

                romano += RomanosNomenclador(valor);
            }

            return romano;
        }

        public static string ConvertRomano(int numero)
        {
            var romano = "";

            if (numero >= 1000)
            {
                var temp = ConvertRomano(numero, 1000);
                romano += RomanosNomenclador(temp);
                numero -= temp;
            }

            if (numero >= 100)
            {
                var temp = ConvertRomano(numero, 100);
                romano += RomanosNomenclador(temp);
                numero -= temp;
            }

            if (numero >= 10)
            {
                var temp = ConvertRomano(numero, 10);
                romano += RomanosNomenclador(temp);
                numero -= temp;
            }

            romano += RomanosNomenclador(numero);

            return romano;
        }
        private static int ConvertRomano(int numero, int potencia)
        {
            var entero = numero / potencia;
            var romano = entero * potencia;
            return romano;
        }

        private static string RomanosNomenclador(int num)
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

        public static int ConvertEntero(string romano)
        {
            var array = new List<int> { NumeroNomenclador(romano[0]) };

            for (var i = 1; i < romano.Length; i++)
            {
                var n1 = NumeroNomenclador(romano[i]);
                var n0 = array[i - 1];

                if (n0 < n1)
                    array[i - 1] = n0 * -1;

                array.Add(n1);
            }

            return array.Sum();
        }
        private static int NumeroNomenclador(char romano)
        {
            switch (romano)
            {
                case 'I': return 1;
                case 'V': return 5;
                case 'X': return 10;
                case 'L': return 50;
                case 'C': return 100;
                case 'D': return 500;
                case 'M': return 1000;
                default: return 0;
            }
        }
    }
}
