using Domain.ValueObjects;

namespace Controller.Dto;

public record VideoRequestProcessRequestDto
{
    public string RequestId { get; init; }
    public StorageFile StorageFile { get; init; }
}
