using System.IO;

namespace Bastet.AiLite.Util;

internal static class FileExtensions
{
	internal static byte[] ToByteArray(this Stream input)
	{
		var buffer = new byte[64 * 1024];
		using var ms = new MemoryStream();
		int read;
		while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
		{
			ms.Write(buffer, 0, read);
		}

		return ms.ToArray();
	}
}