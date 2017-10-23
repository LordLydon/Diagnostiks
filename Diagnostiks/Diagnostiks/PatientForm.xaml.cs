using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagnostiks.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diagnostiks
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PatientForm : ContentPage
	{
		public event EventHandler CreatedPatient;
		public Paciente Paciente;

		public PatientForm(Paciente paciente = null)
		{
			InitializeComponent ();
			if (paciente != null)
			{
				NombreEntry.Text = paciente.Nombre;
				ApellidoEntry.Text = paciente.Apellido;
			}
		}

		private void Create(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(NombreEntry.Text) || string.IsNullOrWhiteSpace(ApellidoEntry.Text))
			{
				DisplayAlert("Error!", "Debe llenar todos los campos para continuar", "OK");
				return;
			}

			Paciente = new Paciente()
			{
				Nombre = NombreEntry.Text,
				Apellido = ApellidoEntry.Text
			};
			
			CreatedPatient?.Invoke(this, EventArgs.Empty);
			Navigation.PopModalAsync(true);
		}

		private void Cancel(object sender, EventArgs e)
		{
			Navigation.PopModalAsync(true);
		}
	}
}