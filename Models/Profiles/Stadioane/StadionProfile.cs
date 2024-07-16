using AspNetCoreApp1.Areas.Admin.Controllers;
using AspNetCoreApp1.Models.DTO.Stadioane;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using AutoMapper;

namespace AspNetCoreApp1.Profiles.Stadioane
{

    public class StadionProfile : Profile
    {
        public StadionProfile()
        {
            CreateMap<Models.Enitities.Stadioane, StadionViewDto>()
                .ForMember(d => d.IdStadion, s => s.MapFrom(src => src.IdStadion))
                .ForMember(d => d.Nume, s => s.MapFrom(src => src.Nume))
                .ForMember(d => d.Capacitate, s => s.MapFrom(src => src.Capacitate))
                .ForMember(d => d.Locatie, s => s.MapFrom(src => src.Locatie!.Oras + ", " + src.Locatie.Tara!.Denumire));

            CreateMap<StadionDto, Models.Enitities.Stadioane>()
                .ForMember(d => d.IdLocatie, s => s.MapFrom(src => src.IdLocatie))
                .ForMember(d => d.Nume, s => s.MapFrom(src => src.Nume))
                .ForMember(d => d.Capacitate, s => s.MapFrom(src => src.Capacitate));

            CreateMap<StadionEditDto, Models.Enitities.Stadioane>()
                .ForMember(d => d.Nume, s => s.MapFrom(src => src.NumeNou))
                .ForMember(d => d.Capacitate, s => s.MapFrom(src => src.CapacitateNoua))
                .ForMember(d => d.IdLocatie, s => s.MapFrom(src => src.IdLocatieNou));
        }
    }
}