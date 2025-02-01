namespace Domain.ValueObjects;

public struct StorageFile
{
    public StorageFile()
    {
        
    }
    public string Key { get; set; } = string.Empty; 
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty; 
}
