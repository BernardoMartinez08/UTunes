using System;
using AutoMapper;
using UTunes.Api.DataTransferObjects.Album;
using UTunes.Api.DataTransferObjects.Song;
using UTunes.Core.Entities;

namespace UTunes.Api.Profiles
{
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<Song, SongDetailsDataTransferObject>();
        }
    }
}