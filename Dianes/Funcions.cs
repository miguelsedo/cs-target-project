using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dianes
{
    public static class Funcions
    {
        // ********************************************************************************************
        //                               Funcions de sortida per pantalla
        // ********************************************************************************************

        /// <summary>
        /// Mostra el text donat per pantalla i afegeix un salt de línia al final.
        /// </summary>
        /// <param name="text"></param>
        public static void printLF(String text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        /// Mostra el text doant en el TextBox especificat, afegint un salt de línia al final.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tb"></param>
        public static void printLF(String text, TextBox tb)
        {
            tb.AppendText(text + Environment.NewLine);
        }

        /// <summary>
        /// Mostra el text donat per pantalla, però no afegeix un salt de línia al final.
        /// Això vol dir que si després tornem a imprimir el text quedarà a continuació d'aquest.
        /// </summary>
        /// <param name="text"></param>
        public static void print(String text)
        {
            Console.Write(text);
        }

        /// <summary>
        /// Mostra el text donat  en el TextBox especificat, però no afegeix un salt de línia al final.
        /// Això vol dir que si després tornem a imprimir el text quedarà a continuació d'aquest.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tb"></param>
        public static void print(String text, TextBox tb)
        {
            tb.Text += text;
        }

        // ********************************************************************************************
        //                                  Funcions per netejar pantalla
        // ********************************************************************************************

        /// <summary>
        /// Esborra tot el text de la pantalla.
        /// </summary>
        public static void clearText()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
                // Si dóna error, no fer res (Clear() falla si es fa servir en un projecte que no sigui de consola).
            }
        }

        /// <summary>
        /// Esborra tot el text de la pantalla.
        /// </summary>
        public static void clearText(TextBox tb)
        {
            tb.Text = "";
        }

        // ********************************************************************************************
        //                                Funcions d'entrada per teclat
        // ********************************************************************************************

        /// <summary>
        /// Llegeix caràcters per teclat (fins a entrar un RETURN) i els retorna com a String.
        /// </summary>
        /// <returns></returns>
        public static String input()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Llegeix caràcters des del TextBox donat i els retorna com a String.
        /// </summary>
        /// <returns></returns>
        public static String input(TextBox tb)
        {
            return tb.Text;
        }


        // ********************************************************************************************
        //                                   Funcions per fer pauses
        // ********************************************************************************************
        /// <summary>
        /// Fa una pausa demanant una tecla. Només serveix per programes per consola.
        /// </summary>
        public static void pause()
        {
            Console.ReadKey();
        }

        // ********************************************************************************************
        //                                      Funcions vàries
        // ********************************************************************************************

        // Determina si unes coordenades donades cauen dins del cercle donat.
        // Entrda:
        //   x, y:             coordenades del punt que es vol consultar.
        //   centerX, centerY: coordenades del centre del cercle del qual es vol mirar.
        //   radius:           radi del cercle donat.
        //
        // Sortida:
        //   'true' si el punt cau dins del cercle, 'false' en cas contrari.
        public static bool isInside(int x, int y, int centerX, int centerY, int radius)
        {
            // Correcció perquè la coordenada no és del centre sinó de la contonada superior esquerra.
            radius = radius / 2;
            centerX = centerX + radius;
            centerY = centerY + radius;

            if (Math.Pow((x - centerX), 2) + Math.Pow((y - centerY), 2) < Math.Pow(radius, 2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
