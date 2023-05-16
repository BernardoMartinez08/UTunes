using System.Diagnostics.Contracts;

namespace UTunes.Api.DataTransferObjects.Song
{
    public class SongDetailsDataTransferObject
    {
        public string Name { get; set; }

        public string Artist { get; set; }

        public double Price { get; set; }
    }
}
