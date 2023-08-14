using System;
namespace DAL.Responses
{
	public record BreedsResponse(Dictionary<string, string[]> message, string succcess);	
}

