using System.Threading;
using System.Threading.Tasks;
using Bastet.AiLite.OpenAi.Model.File;

namespace Bastet.AiLite.OpenAi.Managers;

public partial class OpenAIService : IFileService
{
	public Task<FileListResponse> ListFile(CancellationToken cancellationToken = default)
	{
		throw new System.NotImplementedException();
	}

	public Task<FileUploadResponse> UploadFile(string purpose, byte[] file, string fileName, CancellationToken cancellationToken = default)
	{
		throw new System.NotImplementedException();
	}

	public Task<FileDeleteResponse> DeleteFile(string fileId, CancellationToken cancellationToken = default)
	{
		throw new System.NotImplementedException();
	}

	public Task<FileResponse> RetrieveFile(string fileId, CancellationToken cancellationToken = default)
	{
		throw new System.NotImplementedException();
	}

	public Task<FileContentResponse<T>> RetrieveFileContent<T>(string fileId, CancellationToken cancellationToken = default)
	{
		throw new System.NotImplementedException();
	}
}