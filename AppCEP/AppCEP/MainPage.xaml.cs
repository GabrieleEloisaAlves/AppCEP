using AppCEP.Servico;
using AppCEP.Servico.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCEP
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ImgLogo.Source = ImageSource.FromResource("AppCEP.Img.Logo.png");
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            String cep = txtCEP.Text.Trim();

            if (isValidCEP(cep))
            {
                try
                {

                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);

                    if (end != null)
                    {
                        RESULTADO.Text = string.Format("Endereco: {2} \nBairro: {3} \nCidade: {0}-{1}", end.localidade, end.uf, end.logradouro, end.bairro);

                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }

                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÌTICO", ex.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool valido = true;

            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve conter 8 caracteres.", "OK");

                valido = false;
            }
            int NovoCEP = 0;
            if (!int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido! O CEP deve ser composto apenas por números.", "OK");
                valido = false;
            }
            return valido;
        }
    }
}
