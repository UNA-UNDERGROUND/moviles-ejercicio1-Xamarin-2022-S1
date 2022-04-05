using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Parejas_Operativos
{
    public partial class MainPage : ContentPage
    {
        // la lista de botones
        List<List<BotonCasilla>> buttonList = new List<List<BotonCasilla>>();
        List<List<string>> letras = new List<List<string>>();

        List<Tuple<int, int>> parejas = new List<Tuple<int, int>>();
        public MainPage()
        {
            InitializeComponent();
            init();
        }

        void generarLetras()
        {
            // en este ejemplo usaremos el abecedario
            string caracteres = "abcdefghijklmnopqrstuvwxyz";
            // hay que generar 30 letras(no duplicadas), duplicarlas y mezclarlas
            List<string> lista = new List<string>();
            // escogemos 30 letras al azar
            for (int i = 0; i < 30; i++)
            {
                lista.Add(caracteres[new Random().Next(0, caracteres.Length)].ToString());
            }
            // duplicamos las letras
            lista.AddRange(lista);
            // mezclamos las letras
            lista = lista.OrderBy(a => Guid.NewGuid()).ToList();
            // guardamos las letras en el arreglo de letras
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    letras.Add(new List<string>());
                    letras[i].Add(lista[i * 6 + j]);
                }
            }
        }

        void init()
        {
            generarLetras();

            // creamos y agregamos los botones (6*6)
            for (int i = 0; i < 6; i++)
            {
                List<BotonCasilla> filaBotones = new List<BotonCasilla>();
                buttonList.Add(filaBotones);
                for (int j = 0; j < 6; j++)
                {
                    BotonCasilla button = new BotonCasilla(i, j, letras[i][j]);
                    filaBotones.Add(button);
                    // agregamos el boton al grid
                    gridBotones.Children.Add(button, j, i);
                    // le asignamos el evento
                    button.Clicked += (sender, e) =>
                    {
                        onBtnClick((BotonCasilla)sender);
                    };
                }
            }
        }

        void onBtnClick(BotonCasilla boton)
        {
            int fila = boton.Fila;
            int columna = boton.Columna;
            // revisamos que el boton no este ya seleccionado
            foreach (Tuple<int, int> casilla in parejas)
            {
                if (casilla.Item1 == fila && casilla.Item2 == columna)
                {
                    return;
                }
            }
            // agregamos el boton a la lista de parejas
            parejas.Add(new Tuple<int, int>(fila, columna));
            // si ya hay 2 seleccionados hay que revisar si son pareja
            if (parejas.Count > 2)
            {
                // revisamos si son pareja 
                if (letras[parejas[0].Item1][parejas[0].Item2] == letras[parejas[1].Item1][parejas[1].Item2])
                {
                    // si son pareja los bloqueamos
                    for (int i = 0; i < 2; i++)
                    {
                        buttonList[parejas[i].Item1][parejas[i].Item2].IsEnabled = false;
                    }
                    // los removemos de la lista de parejas
                    parejas.RemoveAt(0);
                    parejas.RemoveAt(0);
                }
                // si no son pareja
                else
                {
                    // los dejamos de seleccionar
                    for (int i = 0; i < 2; i++)
                    {
                        buttonList[parejas[i].Item1][parejas[i].Item2].Seleccionado = false;
                    }
                    // los removemos de la lista de parejas
                    parejas.RemoveAt(0);
                    parejas.RemoveAt(0);
                }
            }
            // la ultima seleccionada siempre es de color verde limon #7bcb03
            if (parejas.Count > 0)
            {
                // obtenemos la ultima seleccionada
                int ultimaCasilla = parejas.Count - 1;
                // la seleccionamos
                buttonList[parejas[ultimaCasilla].Item1][parejas[ultimaCasilla].Item2].Seleccionado = true;

            }
        }
    }
}
