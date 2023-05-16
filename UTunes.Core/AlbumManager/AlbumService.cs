using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.AlbumManager
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> albumRepository;

        public AlbumService(IRepository<Album> albumRepository)
        {
            this.albumRepository = albumRepository;
        }

        public async Task<OperationResult<IReadOnlyList<Album>>> GetAllAsync() => (await this.albumRepository.AllAsync
            ()).ToList();

        public async Task<OperationResult<Album>> GetById(int id)
        {
            var album = await this.albumRepository.GetById(id);
            if (album is null)
            {
                return new OperationResult<Album>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "The album you're trying to return doesn't exist"
                });
            }

            return album;
        }

        public async Task<OperationResult<Album>> LikeAlbumAsync(int id)
        {
            var album = await this.albumRepository.GetById(id);
            if (album is null)
            {
                return new OperationResult<Album>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "The album you're trying to rate doesn't exist."
                });
            }

            album.Likes += 1;
            album.Rates += 1;

            var score = await this.albumRepository.UpdateAsync(album);

            await this.albumRepository.CommitAsync();
            return score;
        }

        public async Task<OperationResult<Album>> DislikeAlbumAsync(int id)
        {
            var album = await this.albumRepository.GetById(id);
            if (album is null)
            {
                return new OperationResult<Album>(new Error
                {
                    Code = ErrorCode.NotFound,
                    Message = "The album you're trying to rate doesn't exist."
                });
            }

            album.Likes -= 1;
            album.Rates += 1;

            var score = await this.albumRepository.UpdateAsync(album);

            await this.albumRepository.CommitAsync();
            return score;
        }
    }
}
