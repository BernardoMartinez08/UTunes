using UTunes.Api.DataTransferObjects.Song;

namespace UTunes.Api.DataTransferObjects.Album
{
    public class AlbumRateDataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Artist { get; set; }

        public string Review { get; set; }

        public int Likes { get; set; }

        public int Rates { get; set; }
    }
}
