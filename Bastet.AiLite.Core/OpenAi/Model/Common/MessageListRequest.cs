using System.Net;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class MessageListRequest : PaginationRequest
{
	public string run_ID { get; set; }

	public override string GetQueryParameters()
	{
		// get querystring from base class
		var querystring = base.GetQueryParameters();
		if (string.IsNullOrWhiteSpace(run_ID))
		{
			return querystring;
		}

		return querystring == null
			? $"run_id={WebUtility.UrlEncode(run_ID)}"
			: $"{querystring}&run_id={WebUtility.UrlEncode(run_ID)}";
	}
}