using System;
namespace DAL.Models
{
	public class BreedsResponse
	{
		public Dictionary<string, string[]> message {
			get; set;
		}

		public string success {
			get; set;
		}

	}
}

