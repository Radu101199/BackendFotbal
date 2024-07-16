using AspNetCoreApp1.Models.DTO.IstoricEchipe;
using AutoMapper;

namespace AspNetCoreApp1.Profiles.IstoricEchipe
{
    public class IstoricEchipaProfile : Profile
    {
        public IstoricEchipaProfile()
        {
            
            
            CreateMap<Models.Enitities.IstoricEchipe, IstoricEchipeViewDto>()
                .ForMember(d => d.IdIstoricEchipa, s => s.MapFrom(src => src.IdIstoricEchipa))
                .ForMember(d => d.DataInceput, s => s.MapFrom(src => src.DataInceput))
                .ForMember(d => d.DataFinal, s => s.MapFrom(src => src.DataFinal))
                .ForMember(d=>d.Echipa,s=>s.MapFrom(src=>src.Echipa!.Nume))
                .ForMember(d=>d.Jucator,s=>s.MapFrom(src=>src.Jucator!.Nume + " " + src.Jucator.Prenume));
            
            // CreateMap<Models.Enitities.IstoricEchipe, IstoricEchipeViewDto>()
            //     .ForMember(d => d.IdIstoricEchipa, s => s.MapFrom(src => src.IdIstoricEchipa))
            //     .ForMember(d => d.DataInceput, s => s.MapFrom(src => src.DataInceput))
            //     .ForMember(d => d.DataFinal, s => s.MapFrom(src => src.DataFinal));
                // .ForMember(d => d.DenumireEchipa, s => s.MapFrom(src => 
                //     src.Echipa!.Nume 
                //     +", cu emblema " + src.Echipa!.Emblema 
                //     +", in orasul " + src.Echipa.Locatie!.Oras 
                //     +", din tara " + src.Echipa.Locatie.Tara!.Denumire
                //     +", pe stadionul " + src.Echipa.Stadion!.Nume
                //     +", antrenata de " + src.Echipa.Antrenor!.Nume + " " + src.Echipa.Antrenor.Prenume
                //     +", nascut in " + src.Echipa.Antrenor.Tara!.Denumire
                //     +", la data de " + src.Echipa.Antrenor.DataNasterii
                //     +", cu poza de profil " + src.Echipa.Antrenor.PozaProfil
                //     +", ce joaca in campionatul cu numele " + src.Echipa.Campionat!.Denumire
                //     +", echipa fiind evaluate la " + src.Echipa.ValoareEchipa + " RON "
                //     +", infintata la data de " + src.Echipa.DataInfiintare))
                // .ForMember(d => d.DenumireJucator,s => s.MapFrom(src => 
                //     src.Jucator!.Nume + " " + src.Jucator!.Prenume
                //     +", ce joaca la echipa " + src.Jucator.Echipa!.Nume
                //     +", cu emblema " + src.Jucator.Echipa!.Emblema 
                //     +", in orasul " + src.Jucator.Echipa.Locatie!.Oras 
                //     +", din tara " + src.Jucator.Echipa.Locatie.Tara!.Denumire
                //     +", pe stadionul " + src.Jucator.Echipa.Stadion!.Nume
                //     +", antrenata de " + src.Jucator.Echipa.Antrenor!.Nume + " " + src.Jucator.Echipa.Antrenor.Prenume
                //     +", nascut in " + src.Jucator.Echipa.Antrenor.Tara!.Denumire
                //     +", la data de " + src.Jucator.Echipa.Antrenor.DataNasterii
                //     +", cu poza de profil " + src.Jucator.Echipa.Antrenor.PozaProfil
                //     +", ce joaca in campionatul cu numele " + src.Jucator.Echipa.Campionat!.Denumire
                //     +", echipa fiind evaluate la " + src.Jucator.Echipa.ValoareEchipa + " RON "
                //     +", infintata la data de " + src.Jucator.Echipa.DataInfiintare
                //     +", cu salariul de " + src.Jucator.Salariu
                //     +", ce joaca pe pozitia de " + src.Jucator.Pozitie!.Denumire
                //     +", nascut in "+src.Jucator.Tara!.Denumire
                //     +", la data de "+src.Jucator.DataNasterii
                //     +", cu poza de profil " +src.Jucator.PozaProfil));

            CreateMap<IstoricEchipeDto, Models.Enitities.IstoricEchipe>()
                .ForMember(d => d.IdEchipa, s => s.MapFrom(src => src.IdEchipa))
                .ForMember(d => d.IdJucator, s => s.MapFrom(src => src.IdJucator))
                .ForMember(d => d.DataInceput, s => s.MapFrom(src => src.DataInceput))
                .ForMember(d => d.DataFinal, s => s.MapFrom(src => src.DataSfarsit));

            CreateMap<IstoricEchipaEditDto, Models.Enitities.IstoricEchipe>()
                .ForMember(d => d.DataInceput, s => s.MapFrom(src => src.DataInceputNoua))
                .ForMember(d => d.DataFinal, s => s.MapFrom(src => src.DataSfarsitNoua))
                .ForMember(d => d.IdEchipa, s => s.MapFrom(src => src.IdEchipaNoua))
                .ForMember(d => d.IdJucator, s => s.MapFrom(src => src.IdJucatorNou));
            
        }
    }
}