using System.Net;
using Bastet.AiLite.OpenAi.Model.Common;

namespace Bastet.AiLite.OpenAi.Model.VectorStore;

public class VectorStoreFileListRequest : PaginationRequest
{
	public string filter { get; set; }

	public override string GetQueryParameters()
	{
		var querystring = base.GetQueryParameters();
		if (filter == null)
		{
			return querystring;
		}

		return querystring == null
			? $"filter={WebUtility.UrlEncode(filter)}"
			: $"{querystring}&filter={WebUtility.UrlEncode(filter)}";
	}
}