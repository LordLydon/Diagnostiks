using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diagnostiks.Models;
using Xamarin.Forms;

namespace Diagnostiks
{
	public partial class MainPage : ContentPage
	{
		private IList<Paciente> pacientes = new ObservableCollection<Paciente>();

		public MainPage()
		{
			Title = "Pacientes";
			InitializeComponent();
			BindingContext = pacientes;
		}

		private void AddPaciente(object sender, EventArgs e)
		{
			PatientForm form = new PatientForm();
			form.CreatedPatient += StorePaciente;
			Navigation.PushModalAsync(form, true);
		}

		private void StorePaciente(object sender, EventArgs e)
		{
			if (sender is PatientForm form) pacientes.Add(form.Paciente);
		}


		private async void NavigateToPatientView(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem != null)
			{
				Paciente paciente = e.SelectedItem as Paciente;
				PatientDetail detail = new PatientDetail(paciente, pacientes.IndexOf(paciente));
				detail.UpdatedPatient += UpdatePatient;
				await Navigation.PushAsync(detail, true);

				((ListView) sender).SelectedItem = null;
			}
		}

		private void UpdatePatient(object sender, PatientDetail.DetailArgs e)
		{
			pacientes[e.Pos] = e.Paciente;
		}
	}
}