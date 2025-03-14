﻿using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bastet.AiLite.OpenAi.Model.File;
using Bastet.AiLite.Util;
using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Managers;

public partial class OpenAIService : IFileService
{
	public async Task<FileListResponse> ListFile(CancellationToken cancellationToken = default)
	{
		return (await _httpClient.GetFromJsonAsync<FileListResponse>(_endpointProvider.FilesList(),
			cancellationToken))!;
	}

	public async Task<FileUploadResponse> UploadFile(string purpose, byte[] file, string fileName,
		CancellationToken cancellationToken = default)
	{
		var multipartContent = new MultipartFormDataContent
		{
			{ new StringContent(purpose), "purpose" },
			{ new ByteArrayContent(file), "file", fileName }
		};

		return await _httpClient.PostFileAndReadAsAsync<FileUploadResponse>(_endpointProvider.FilesUpload(),
			multipartContent, cancellationToken);
	}

	public async Task<FileDeleteResponse> DeleteFile(string fileId, CancellationToken cancellationToken = default)
	{
		return await _httpClient.DeleteAndReadAsAsync<FileDeleteResponse>(_endpointProvider.FileDelete(fileId),
			cancellationToken);
	}

	public async Task<FileResponse> RetrieveFile(string fileId, CancellationToken cancellationToken = default)
	{
		return (await _httpClient.GetFromJsonAsync<FileResponse>(_endpointProvider.FileRetrieve(fileId),
			cancellationToken))!;
	}

	public async Task<FileContentResponse<T>> RetrieveFileContent<T>(string fileId,
		CancellationToken cancellationToken = default)
	{
		var response = await _httpClient.GetAsync(_endpointProvider.FileRetrieveContent(fileId), cancellationToken);

		if (typeof(T) == typeof(string))
		{
			return new()
			{
				Content = (T)(object)await response.Content.ReadAsStringAsync(cancellationToken)
			};
		}

		if (typeof(T) == typeof(byte[]))
		{
			return new()
			{
				Content = (T)(object)await response.Content.ReadAsByteArrayAsync(cancellationToken)
			};
		}

		if (typeof(T) == typeof(Stream))
		{
			return new()
			{
				Content = (T)(object)await response.Content.ReadAsStreamAsync(cancellationToken)
			};
		}

		var str = await response.Content.ReadAsStringAsync(cancellationToken);
		return new()
		{
			Content = JsonConvert.DeserializeObject<T>(str),
		};
	}
}