using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Parejas_Operativos
{
    // heredamos de Button
    internal class BotonCasilla : Button
    {
        // constantes de colores
        // - seleccionado -
        private static readonly Color colorFondoSeleccionado = Color.FromHex("#7bcb03");
        private static readonly Color colorTextoSeleccionado = Color.FromHex("#282828");
        private static readonly FontAttributes atributosSeleccionado = FontAttributes.Bold;
        // - no seleccionado -
        private static readonly Color colorFondoNormal = Color.FromHex("#333333");
        private static readonly Color colorTextoNormal = Color.FromHex("#ffffff");
        private static readonly FontAttributes atributosNormal = FontAttributes.None;
        // - bloqueado -
        private static readonly Color colorFondoBloqueado = Color.FromHex("#51bbfe");
        private static readonly Color colorTextoBloqueado = Color.FromHex("#282828");
        private static readonly FontAttributes atributosBloqueado = FontAttributes.Bold | FontAttributes.Italic;

        int fila;
        int columna;
        bool seleccionado = false;
        bool bloqueado = false;
        readonly string letra;

        public BotonCasilla(int fila, int columna, string letra)
        {
            this.fila = fila;
            this.columna = columna;
            this.letra = letra;
            Text = "-";
        }

        public int Fila
        {
            get { return fila; }
            set { fila = value; }
        }
        public int Columna
        {
            get { return columna; }
            set { columna = value; }
        }
        public bool Seleccionado
        {
            get { return seleccionado; }
            set
            {
                // si esta bloqueado no se puede seleccionar
                if (!bloqueado)
                {
                    seleccionado = value;
                    BackgroundColor = seleccionado ? colorFondoSeleccionado : colorFondoNormal;
                    TextColor = seleccionado ? colorTextoSeleccionado : colorTextoNormal;
                    FontAttributes = seleccionado ? atributosSeleccionado : atributosNormal;
                    Text = seleccionado ? letra : "-";
                }
            }
        }

        public bool Bloqueado
        {
            get { return bloqueado; }
            set
            {
                bloqueado = value;
                IsEnabled = bloqueado;
                if (bloqueado)
                {
                    BackgroundColor = colorFondoBloqueado;
                    TextColor = colorTextoBloqueado;
                    FontAttributes = atributosBloqueado;
                }
                else
                {
                    // retornamos al color de si esta clickeado o no
                    Seleccionado = Seleccionado;
                }
            }
        }

    }
}
