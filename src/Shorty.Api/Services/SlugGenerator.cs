using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Shorty.Api.Services;

public sealed class SlugGenerator : ISlugGenerator
{
    private const char SEP = '-';
    private static readonly string _characters = "abcdefghijklmnopqrstuvwxyz0123456789";
    public string Generate(int chunks = 1, int chunkLength = 8)
    {
        if (chunks < 1 || chunks > 3) throw new ArgumentOutOfRangeException(nameof(chunks));
        if (chunkLength*chunks < 1 || chunkLength*chunks > 64) throw new ArgumentOutOfRangeException(nameof(chunkLength));

        var randomChunks = Enumerable.Range(0, chunks)
            .Select(_ => GenerateChunk(chunkLength))
            .ToArray();

        return string.Join(SEP.ToString(), randomChunks);
    }

    private string GenerateChunk(int length)
    {
        return new string(Enumerable.Range(0, length)
            .Select(_ => _characters[RandomNumberGenerator.GetInt32(_characters.Length)])
            .ToArray());
    }
}