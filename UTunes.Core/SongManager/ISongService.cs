using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTunes.Core.Entities;

namespace UTunes.Core.SongManager
{
    public interface ISongService
    {
        Task<OperationResult<IReadOnlyList<Song>>> GetAllAsync();
        Task<OperationResult<IReadOnlyList<Song>>> GetByAlbum(int albumId);
        Task<OperationResult<Song>> GetById(int id);
    }
}
