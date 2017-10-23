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
	public partial class DiagnosticForm : ContentPage
	{
		public event EventHandler DiagnosticCreated;
		public string Old;
		public string Diagnostic;

		public DiagnosticForm(Paciente paciente, string diagnostico = null, bool edit = false)
		{
			Title = paciente.FullName;
			InitializeComponent();
			if (diagnostico != null)
			{
				DiagnosticEntry.Text = diagnostico;
				Old = diagnostico;
			}
		}

		private void Create(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(DiagnosticEntry.Text))
			{
				DisplayAlert("Error!", "Debe ingresar el diagnostico para continuar.", "OK");
				return;
			}

			Diagnostic = DiagnosticEntry.Text;
			DiagnosticCreated?.Invoke(this, EventArgs.Empty);
			Navigation.PopModalAsync(true);
		}

		private void Cancel(object sender, EventArgs e)
		{
			Navigation.PopModalAsync(true);
		}
	}
}