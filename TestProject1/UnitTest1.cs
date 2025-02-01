using Domain.ValueObjects;

using UseCase.UseCase;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var path = @"C:\Users\bruno\Downloads\FIAP_DOTNET_NET1_23\FIAP_DOTNET_NET1_23\FIAPProcessaVideo\Marvel_DOTNET_CSHARP.mp4";
            byte[] readText = File.ReadAllBytes(path);

            var x = new FileModel()
            {
                FileName = "Marvel_DOTNET_CSHARP.mp4",
                ContentType = "video/mp4",
                Data = readText
            };
            var ab = new VideoUseCase();
            
            await ab.SliceAsync(x, TimeSpan.FromSeconds(20), CancellationToken.None);
        }
    }
}