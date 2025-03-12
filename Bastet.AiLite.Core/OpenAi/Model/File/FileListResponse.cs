using System.Collections.Generic;
using Bastet.AiLite.OpenAi.Model.Common;

namespace Bastet.AiLite.OpenAi.Model.File;

public class FileListResponse : BaseResponse
{
	public List<FileResponse> data { get; set; }
}