using AutoMapper;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Enumerables;
using SmartCardCRM.Model.Models;
using SmartCardCRM.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SmartCardCRM.Data
{
    public class AutoMapperConfiguration
    {
        public static IConfigurationProvider Configure()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Client, ClientDTO>()
                    .ForMember(x => x.ArrivalTime, o => o.MapFrom(x => x.ArrivalTime.ToString())) //// Change TimeSpan to string in DTO
                    .ForMember(x => x.WayOutTime, o => o.MapFrom(x => x.WayOutTime == null ? null : x.WayOutTime.Value.ToString(@"hh\:mm\:ss"))) //// Change TimeSpan to string in DTO
                    .ForMember(x => x.CoOwnerDebitCreditCards, o => o.MapFrom(x => x.ClientDebitCreditCards.Where(c => c.IsClientCard == false).ToList()))
                    .ForMember(x => x.ClientDebitCreditCards, o => o.MapFrom(x => x.ClientDebitCreditCards.Where(c => c.IsClientCard == true).ToList()))
                    .ForMember(x => x.DateSpanishFormated, o => o.MapFrom(x => x.Date.ToString("dddd, dd/MM/yyyy", new CultureInfo("es-CO"))))
                    .ForMember(x => x.HasContract, o => o.MapFrom(x => x.Contract != null && x.Contract.Count > 0 && x.Contract.Where(c => c.Status == ContractStatus.Generated.ToString() || c.Status == ContractStatus.Edited.ToString()).ToList().Count > 0));

                cfg.CreateMap<ClientDTO, Client>()
                    .ForMember(x => x.ArrivalTime, o => o.MapFrom(x => TimeSpan.Parse(x.ArrivalTime)))
                    .ForMember(x => x.WayOutTime, o => o.MapFrom(x => string.IsNullOrEmpty(x.WayOutTime) ? new TimeSpan?() : TimeSpan.Parse(x.WayOutTime)))
                    .ForMember(x => x.ClientDebitCreditCards, o => o.MapFrom(x => x.ClientDebitCreditCards.Union(x.CoOwnerDebitCreditCards)));
                    
                cfg.CreateMap<ClientDebitCreditCards, ClientDebitCreditCardsDTO>().ReverseMap();

                cfg.CreateMap<User, UserDTO>().ReverseMap();
                
                cfg.CreateMap<UserRoles, UserRolesDTO>().ReverseMap();

                cfg.CreateMap<Role, RoleDTO>()
                    .ForMember(x => x.Scopes, o => o.MapFrom(x => x.Scopes.Split(",", StringSplitOptions.None)))
                    .ReverseMap();

                cfg.CreateMap<Contract, ContractDTO>()
                    .ForMember(x => x.Status, o => o.MapFrom(x => (ContractStatus)Enum.Parse(typeof(ContractStatus), x.Status)))
                    .ForMember(x => x.LendInstallmentPriceInWords, o => o.MapFrom(x => Convert.ToDecimal(x.LendInstallmentPrice).ToWords() + " PESOS"))
                    .ForMember(x => x.ContractMembershipInWords, o => o.MapFrom(x => Convert.ToDecimal(x.MembershipPrice).ToWords() + " PESOS"))
                    .ForMember(x => x.EntryPriceInWords, o => o.MapFrom(x => Convert.ToDecimal(x.EntryPrice).ToWords() + " PESOS"))
                    .ForMember(x => x.ContractBeneficiariesCount, o => o.MapFrom(x => x.ContractBeneficiaries.Count))
                    .ForMember(x => x.ContractBeneficiariesCountInWords, o => o.MapFrom(x => Convert.ToDecimal(x.ContractBeneficiaries.Count).ToWords()))
                    .ForMember(x => x.ContractorSignatureBytes, o => o.MapFrom(x => Convert.FromBase64String(x.ContractorSignature.Split(",", StringSplitOptions.None)[1])))
                    .ForMember(x => x.CoOwnerSignatureBytes, o => o.MapFrom(x => Convert.FromBase64String(x.CoOwnerSignature.Split(",", StringSplitOptions.None)[1])))
                    .ForMember(x => x.AdviserSignature1Bytes, o => o.MapFrom(x => Convert.FromBase64String(x.Adviser1Signature.Split(",", StringSplitOptions.None)[1])))
                    .ForMember(x => x.AdviserSignature2Bytes, o => o.MapFrom(x => Convert.FromBase64String(x.Adviser2Signature.Split(",", StringSplitOptions.None)[1])))
                    .ForMember(x => x.AuthorizationSignatureBytes, o => o.MapFrom(x => Convert.FromBase64String(x.AuthorizationSignature.Split(",", StringSplitOptions.None)[1])))
                    .ReverseMap();

                cfg.CreateMap<ContractBeneficiaries, ContractBeneficiariesDTO>().ReverseMap();
                
                cfg.CreateMap<ConfigurationSettings, ConfigurationSettingsDTO>().ReverseMap();

                cfg.CreateMap<BookingObservations, BookingObservationsDTO>().ReverseMap();

                cfg.CreateMap<BookingObservationFiles, BookingObservationFilesDTO>()
                    .ForMember(x => x.FilePath, o => o.MapFrom(x => Environment.CurrentDirectory + x.FilePath)).ReverseMap();

                cfg.CreateMap<CustomerServiceObservations, CustomerServiceObservationsDTO>().ReverseMap();

                cfg.CreateMap<CustomerServiceObservationFiles, CustomerServiceObservationFilesDTO>()
                    .ForMember(x => x.FilePath, o => o.MapFrom(x => Environment.CurrentDirectory + x.FilePath)).ReverseMap();

                cfg.CreateMap<Quoter, QuoterDTO>().ReverseMap();

                cfg.CreateMap<Room, RoomDTO>()
                .ForMember(x => x.KidsAges, o => o.MapFrom(x => x.KidsAges.Split(",", StringSplitOptions.None).Select(x => Int32.Parse(x)).ToList()));

                cfg.CreateMap<RoomDTO, Room>()
                .ForMember(x => x.KidsAges, o => o.MapFrom(x => string.Join(",", x.KidsAges.Select(n => n))));
            });
        }
    }
}
