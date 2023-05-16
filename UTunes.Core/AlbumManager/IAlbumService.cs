using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.AlbumManager
{
    public interface IAlbumService
    {
        Task<OperationResult<IReadOnlyList<Album>>> GetAllAsync();
        Task<OperationResult<Album>> GetById(int id);
        Task<OperationResult<Album>> LikeAlbumAsync(int id);
        Task<OperationResult<Album>> DislikeAlbumAsync(int id);

    }
}
