using Bastet.AiLite.OpenAi.Model.Common;

namespace Bastet.AiLite.OpenAi.Model.File;

public class FileDeleteResponse : BaseResponse
{
	public bool deleted { get; set; }

	public string id { get; set; }
}