using Bastet.AiLite.OpenAi.Model.Common;

namespace Bastet.AiLite.OpenAi.Model.File;

public class FileContentResponse<T>
{
	public T Content { get; set; }

	public bool Successful => error == null;
	
	public Error error { get; set; }
}