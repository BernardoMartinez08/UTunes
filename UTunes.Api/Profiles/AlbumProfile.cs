using System;
using AutoMapper;
using UTunes.Api.DataTransferObjects.Album;
using UTunes.Core.Entities;

namespace UTunes.Api.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumDetailsDataTransferObject>();
            CreateMap<Album, AlbumPreviewDataTransferObject>();
            CreateMap<Album, AlbumRateDataTransferObject>();
            CreateMap<AddAlbumDataTransferObject, Album>();
        }
    }
}
