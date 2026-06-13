using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTracking.Domain.DTO
{
	public class AuthResponseDTO
	{
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
		public List<string> Permissions { get; set; } = new();
		public bool success { get; set; }
		public string message { get; set; } = string.Empty;
	}
}
