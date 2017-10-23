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
	public partial class PatientDetail : ContentPage
	{
		public event EventHandler<DetailArgs> UpdatedPatient;
		private int pos;
		private Paciente paciente;

		public PatientDetail(Paciente paciente, int pos)
		{
			Title = paciente.FullName;
			InitializeComponent();
			this.paciente = paciente;
			this.pos = pos;
			BindingContext = this.paciente.Diagnosticos;
		}

		private void AddDiagnostico(object sender, EventArgs e)
		{
			DiagnosticForm form = new DiagnosticForm(paciente);
			form.DiagnosticCreated += StoreDiagnostico;
			Navigation.PushModalAsync(form, true);
		}

		private void StoreDiagnostico(object sender, EventArgs eventArgs)
		{
			if (sender is DiagnosticForm form) paciente.AddDiagnostico(form.Diagnostic);
		}

		private async void EditDiagnostico(object sender, EventArgs e)
		{
			MenuItem item = sender as MenuItem;
			string diagnostico = item.CommandParameter as string;

			DiagnosticForm form = new DiagnosticForm(paciente, diagnostico);
			form.DiagnosticCreated += UpdateDiagnostic;
			await Navigation.PushAsync(form);

			DiagnosticsList.SelectedItem = null;
		}

		private void UpdateDiagnostic(object sender, EventArgs eventArgs)
		{
			if (sender is DiagnosticForm form)
			{
				paciente.EditDiagnostico(form.Old, form.Diagnostic);
			}
		}

		private void DeleteDiagnostico(object sender, EventArgs e)
		{
			MenuItem item = sender as MenuItem;
			string diagnostico = item.CommandParameter as string;

			if (paciente.LastDiagnostic != diagnostico)
			{
				DisplayAlert("Error", "Solo se puede eliminar el último diagnostico!", "OK");
				return;
			}

			paciente.DeleteDiagnostico(diagnostico);
		}

		protected override void OnDisappearing()
		{
			UpdatedPatient?.Invoke(this, new DetailArgs() {Paciente = paciente, Pos = pos});
			base.OnDisappearing();
		}

		public class DetailArgs : EventArgs
		{
			public Paciente Paciente { get; set; }
			public int Pos { get; set; }
		}
	}
}