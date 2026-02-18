namespace Shorty.Api.Services;

public interface ISlugGenerator
{
    string Generate(int chunks = 1, int chunkLength = 8);
}