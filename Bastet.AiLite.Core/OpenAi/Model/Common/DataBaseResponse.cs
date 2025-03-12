using System.Collections.Generic;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class DataBaseResponse<T> : BaseResponse
{
	public T data { get; set; }
}

public class ErrorList : DataBaseResponse<List<Error>>
{
}