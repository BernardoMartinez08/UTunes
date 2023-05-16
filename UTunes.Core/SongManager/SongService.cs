using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.SongManager
{
    public class SongService : ISongService
    {
        private readonly IRepository<Song> songRepository;
        private readonly IRepository<Album> albumRepository;

        public SongService(
            IRepository<Song> songRepository,
            IRepository<Album> albumRepository)
        {
            this.songRepository = songRepository;
            this.albumRepository = albumRepository;
        }

        public async Task<OperationResult<IReadOnlyList<Song>>> GetAllAsync() => (await this.songRepository.AllAsync
            ()).ToList();

        public async Task<OperationResult<IReadOnlyList<Song>>> GetByAlbum(int albumId)
        {
            if (albumId == -1)
            {
                return (await this.songRepository.AllAsync()).ToList();
            }
            return this.songRepository.Filter(x => x.AlbumId == albumId).ToList();
        }

        public async Task<OperationResult<Song>> GetById(int id)
        {
            var song = await this.songRepository.GetById(id);
            if (song is null)
            {
                return new OperationResult<Song>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "The song you're trying to return doesn't exist"
                });
            }

            return song;
        }
    }
}
