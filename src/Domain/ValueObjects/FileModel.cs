namespace Domain.ValueObjects;
public struct FileModel
{

    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required byte[] Data { get; init; }
}
