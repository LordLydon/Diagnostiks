using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Diagnostiks.Models
{
	public class Paciente
	{
		public string Nombre { get; set; }
		public string Apellido { get; set; }
		public IList<string> Diagnosticos { get; } = new ObservableCollection<string>();

		public string FullName => Nombre + " " + Apellido;

		public string LastDiagnostic => string.IsNullOrEmpty(Diagnosticos.LastOrDefault())
			? "El paciente no tiene diagnosticos"
			: Diagnosticos.LastOrDefault();

		public void AddDiagnostico(string diagnostico)
		{
			Diagnosticos.Add(diagnostico);
		}

		public void EditDiagnostico(string diagnostico, int pos)
		{
			Diagnosticos[pos] = diagnostico;
		}

		public void EditDiagnostico(string old, string diagnostico)
		{
			int pos = Diagnosticos.IndexOf(old);
			Diagnosticos[pos] = diagnostico;
		}

		public bool DeleteDiagnostico(string diagnostico)
		{
			return Diagnosticos.Remove(diagnostico);
		}
	}
}