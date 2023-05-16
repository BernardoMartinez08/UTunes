using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UTunes.Api.DataTransferObjects.Album;
using UTunes.Api.DataTransferObjects.Song;
using UTunes.Core;
using UTunes.Core.AlbumManager;
using UTunes.Core.Entities;
using UTunes.Core.SongManager;

namespace UTunes.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : UTunesBaseController
{
    private readonly IAlbumService albumService;
    private readonly ISongService songService;
    private readonly IMapper mapper;

    public AlbumsController(
        IAlbumService albumService,
        ISongService songService,
        IMapper mapper)
    {
        this.albumService = albumService;
        this.songService = songService;
        this.mapper = mapper;
    }

    [HttpGet("{albumId}/songs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSongsByAlbumAsync(int albumId)
    {
        var result = await this.songService.GetByAlbum(albumId);
        var songs = this.mapper.Map<IList<SongDetailsDataTransferObject>>(result.Result);
        return result.Succeeded ? Ok(songs) : GetErrorResult<IReadOnlyList<Core.Entities.Song>>(result);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlbumsAsync()
    {
        var result = await this.albumService.GetAllAsync();
        var albums = this.mapper.Map<IList<AlbumPreviewDataTransferObject>>(result.Result);
        return result.Succeeded ? Ok(albums) : GetErrorResult<IReadOnlyList<Core.Entities.Album>>(result);
    }

    [HttpGet("{albumId}/details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlbumDetailsAsync(int albumId)
    {
        var result_songs = await this.songService.GetByAlbum(albumId);
        var songs = this.mapper.Map<IList<SongDetailsDataTransferObject>>(result_songs.Result);

        if (!result_songs.Succeeded)
        {
            return GetErrorResult<IReadOnlyList<Song>>(result_songs);
        }

        var result_album = await this.albumService.GetById(albumId);
        var album = this.mapper.Map<AlbumDetailsDataTransferObject>(result_album.Result);
        album.Songs = songs;

        if(result_album.Result.Likes > 0)
            album.Score = result_album.Result.Likes / result_album.Result.Rates;

        album.Price = result_songs.Result.Sum(song => song.Price);

        return result_album.Succeeded ? Ok(album) : GetErrorResult<Album>(result_album);
    }

    [HttpPut("{albumId}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LikeAlbumAsync(int albumId)
    {
        var result = await this.albumService.LikeAlbumAsync(albumId);
        var updatedAlbum = this.mapper.Map<AlbumRateDataTransferObject>(result.Result);
        return result.Succeeded ? Ok(updatedAlbum) : GetErrorResult<Album>(result);
    }

    [HttpPut("{albumId}/dislike")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DislikeAlbumAsync(int albumId)
    {
        var result = await this.albumService.DislikeAlbumAsync(albumId);
        var updatedAlbum = this.mapper.Map<AlbumRateDataTransferObject>(result.Result);
        return result.Succeeded ? Ok(updatedAlbum) : GetErrorResult<Album>(result);
    }

    [HttpGet("{albumId}/songs/{songName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAlbumsByNameAsync(int albumId, string songName)
    {
        var result = await this.songService.GetAllAsync();
        var filteredResult = result.Result.FirstOrDefault(song => song.Name.Contains(songName) && song.AlbumId == albumId);
        var songs = this.mapper.Map<SongDetailsDataTransferObject>(filteredResult);
        return result.Succeeded ? Ok(songs) : GetErrorResult<IReadOnlyList<Core.Entities.Song>>(result);
    }
}

