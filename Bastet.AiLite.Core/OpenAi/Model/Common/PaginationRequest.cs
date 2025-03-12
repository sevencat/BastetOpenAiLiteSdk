using System.Collections.Generic;
using System.Net;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class PaginationRequest
{
	public int? limit { get; set; }

	public string order { get; set; }


	public string after { get; set; }

	public string before { get; set; }

	public virtual string GetQueryParameters()
	{
		var build = new List<string>();
		if (limit != null)
		{
			build.Add($"limit={limit}");
		}

		if (order != null)
		{
			build.Add($"order={WebUtility.UrlEncode(order)}");
		}

		if (after != null)
		{
			build.Add($"after={WebUtility.UrlEncode(after)}");
		}

		if (before != null)
		{
			build.Add($"before={WebUtility.UrlEncode(before)}");
		}

		if (build.Count <= 0) return null;

		return string.Join("&", build);
	}
}