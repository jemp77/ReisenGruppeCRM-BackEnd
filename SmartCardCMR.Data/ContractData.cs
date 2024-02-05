using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Enumerables;
using SmartCardCRM.Model.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SmartCardCRM.Data
{
    public class ContractData : BaseData
    {
        public ContractData(SmartCardCRMContext context)
        {
            _context = context;
        }

        public List<ContractDTO> GetContractsByDateRange(DateTime startDate, DateTime endDate)
        {
            return new ExceptionLogData<List<ContractDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    var listContractDTO = new List<ContractDTO>();
                    var ListContracts = _context.Contract.Include("ContractBeneficiaries").Where(x => x.ContractDate >= startDate && x.ContractDate <= endDate).ToList();
                    if (ListContracts != null || ListContracts.Count > 0)
                    {
                        listContractDTO = new Mapper(MapperConfig).Map<List<ContractDTO>>(ListContracts);
                        listContractDTO.ForEach(x => 
                            {
                                var paymentList = new List<string>();
                                if (x.DebitCardPayment > 0)
                                    paymentList.Add("T. Debito");

                                if (x.CreditCardPayment > 0)
                                    paymentList.Add("T. Credito");

                                if (x.CashPayment > 0)
                                    paymentList.Add("Efectivo");

                                if (x.TransferPayment > 0)
                                    paymentList.Add("Transferencia");

                                x.PaymentMethods = string.Join(" - ", paymentList.ToArray());
                            });
                    }

                    return listContractDTO;
                });
        }

        public List<ContractDTO> GetContract(int id)
        {
            return new ExceptionLogData<List<ContractDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    var contractList = new List<ContractDTO>();
                    var contract = _context.Contract.Include("Client").Include("ContractBeneficiaries").Where(x => x.Id == id).FirstOrDefault();
                    if (contract != null)
                    {
                        _context.Entry(contract).State = EntityState.Detached;
                        var contractDTO = new Mapper(MapperConfig).Map<ContractDTO>(contract);
                        contractDTO.CustomDateTime = string.Format("{0} {1} de {2} del {3}",
                                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(contractDTO.ContractDate.ToString("dddd", CultureInfo.CreateSpecificCulture("es-ES"))),
                                contractDTO.ContractDate.Day.ToString(),
                                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(contractDTO.ContractDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES"))),
                                contractDTO.ContractDate.Year);
                        contractList.Add(contractDTO);
                    }
                    
                    return contractList;
                });
        }

        public List<ContractDTO> GetActiveContractsByFilters(string filter)
        {
            return new ExceptionLogData<List<ContractDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    var contracts = _context.Contract.Include("Client").Where(x =>
                        x.Status != ContractStatus.Canceled.ToString()
                        && (x.Client.DocumentNumber.Contains(filter)
                        || x.Client.Name.Contains(filter)
                        || x.Client.LastName.Contains(filter)
                        || (x.Client.Name + " " + x.Client.LastName).Contains(filter)
                        || (x.Client.LastName + " " + x.Client.Name).Contains(filter)
                        || x.Client.Email.Contains(filter)
                        || x.Client.PhoneNumber.Contains(filter)
                        || x.ContractNumber.Contains(filter))
                    ).ToList();
                    return new Mapper(MapperConfig).Map<List<ContractDTO>>(contracts);
                });
        }

        public List<ContractDTO> GetAllContractsByFilters(string filter)
        {
            return new ExceptionLogData<List<ContractDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    var contracts = _context.Contract.Include("Client").Where(x =>
                        x.Client.DocumentNumber.Contains(filter)
                        || x.Client.Name.Contains(filter)
                        || x.Client.LastName.Contains(filter)
                        || (x.Client.Name + " " + x.Client.LastName).Contains(filter)
                        || (x.Client.LastName + " " + x.Client.Name).Contains(filter)
                        || x.Client.Email.Contains(filter)
                        || x.Client.PhoneNumber.Contains(filter)
                        || x.ContractNumber.Contains(filter)
                    ).ToList();
                    return new Mapper(MapperConfig).Map<List<ContractDTO>>(contracts);
                });
        }

        public ContractDTO CreateContract(ContractDTO contractDTO)
        {
            return new ExceptionLogData<ContractDTO>(_context).LogWithTransaction(this.GetType().Name,
                () =>
                {
                    var contractNumber = new List<Contract>();
                    var client = _context.Client.Find(contractDTO.ClientId);
                    
                    do
                    {
                        contractNumber = _context.Contract.Where(x => x.ContractNumber == contractDTO.ContractNumber).ToList();
                        if (contractNumber.Count > 0)
                        {
                            contractDTO.ContractNumber = "BOG" + (int.Parse(contractDTO.ContractNumber.Split("BOG")[1]) + 1).ToString();
                        }
                    }
                    while (contractNumber.Count > 0);
                    contractDTO.Status = ContractStatus.Generated;
                    client.Name = contractDTO.Client.Name;
                    client.LastName = contractDTO.Client.LastName;
                    client.Profession = contractDTO.Client.Profession;
                    client.DocumentType = contractDTO.Client.DocumentType;
                    client.DocumentNumber = contractDTO.Client.DocumentNumber;
                    client.Address = contractDTO.Client.Address;
                    client.CellPhone = contractDTO.Client.CellPhone;
                    client.Office = contractDTO.Client.Office;
                    client.Country = contractDTO.Client.Country;
                    client.Department = contractDTO.Client.Department;
                    client.City = contractDTO.Client.City;
                    client.BirthDate = contractDTO.Client.BirthDate;
                    client.PhoneNumber = contractDTO.Client.PhoneNumber;
                    client.Email = contractDTO.Client.Email;
                    client.CoOwnerProfession = contractDTO.Client.CoOwnerProfession;
                    client.CoOwnerDocumentType = contractDTO.Client.CoOwnerDocumentType;
                    client.CoOwnerDocumentNumber = contractDTO.Client.CoOwnerDocumentNumber;
                    client.CoOwnerAddress = contractDTO.Client.CoOwnerAddress;
                    client.CoOwnerCellPhone = contractDTO.Client.CoOwnerCellPhone;
                    client.CoOwnerOffice = contractDTO.Client.CoOwnerOffice;
                    client.CoOwnerCountry = contractDTO.Client.CoOwnerCountry;
                    client.CoOwnerDepartment = contractDTO.Client.CoOwnerDepartment;
                    client.CoOwnerCity = contractDTO.Client.CoOwnerCity;
                    client.CoOwnerBirthDate = contractDTO.Client.CoOwnerBirthDate;
                    client.CoOwnerPhoneNumber = contractDTO.Client.CoOwnerPhoneNumber;
                    client.CoOwnerEmail = contractDTO.Client.CoOwnerEmail;
                    contractDTO.ContractBeneficiaries = contractDTO.ContractBeneficiaries.Where(x => !string.IsNullOrEmpty(x.Names)).ToList();

                    var contract = new Mapper(MapperConfig).Map<Contract>(contractDTO);
                    contract.Client = client;
                    _context.Entry(contract.Client).State = EntityState.Modified;
                    _context.Contract.Add(contract);
                    _context.SaveChanges();
                    return new Mapper(MapperConfig).Map<ContractDTO>(contract);
                });
        }

        public void UpdateContract(int id, ContractDTO contractDTO)
        {
            new ExceptionLogData<int>(_context).LogWithTransaction(this.GetType().Name,
                () =>
                {
                    contractDTO.Status = ContractStatus.Edited;
                    contractDTO.ContractBeneficiaries = contractDTO.ContractBeneficiaries.Where(x => 
                        !string.IsNullOrEmpty(x.Names) 
                        || !string.IsNullOrEmpty(x.LastNames)).ToList();
                    var contract = new Mapper(MapperConfig).Map<Contract>(contractDTO);
                    contract.Client = _context.Client.Find(contractDTO.ClientId);
                    contract.Client.Profession = contractDTO.Client.Profession;
                    contract.Client.DocumentType = contractDTO.Client.DocumentType;
                    contract.Client.DocumentNumber = contractDTO.Client.DocumentNumber;
                    contract.Client.Address = contractDTO.Client.Address;
                    contract.Client.CellPhone = contractDTO.Client.CellPhone;
                    contract.Client.Office = contractDTO.Client.Office;
                    contract.Client.Country = contractDTO.Client.Country;
                    contract.Client.Department = contractDTO.Client.Department;
                    contract.Client.City = contractDTO.Client.City;
                    contract.Client.BirthDate = contractDTO.Client.BirthDate;
                    contract.Client.PhoneNumber = contractDTO.Client.PhoneNumber;
                    contract.Client.Email = contractDTO.Client.Email;
                    contract.Client.CoOwnerProfession = contractDTO.Client.CoOwnerProfession;
                    contract.Client.CoOwnerDocumentType = contractDTO.Client.CoOwnerDocumentType;
                    contract.Client.CoOwnerDocumentNumber = contractDTO.Client.CoOwnerDocumentNumber;
                    contract.Client.CoOwnerAddress = contractDTO.Client.CoOwnerAddress;
                    contract.Client.CoOwnerCellPhone = contractDTO.Client.CoOwnerCellPhone;
                    contract.Client.CoOwnerOffice = contractDTO.Client.CoOwnerOffice;
                    contract.Client.CoOwnerCountry = contractDTO.Client.CoOwnerCountry;
                    contract.Client.CoOwnerDepartment = contractDTO.Client.CoOwnerDepartment;
                    contract.Client.CoOwnerCity = contractDTO.Client.CoOwnerCity;
                    contract.Client.CoOwnerBirthDate = contractDTO.Client.CoOwnerBirthDate;
                    contract.Client.CoOwnerPhoneNumber = contractDTO.Client.CoOwnerPhoneNumber;

                    var beneficiaries = new Mapper(MapperConfig).Map<List<ContractBeneficiaries>>(_context.ContractBeneficiaries.Where(x => x.ContractId == id).ToList());
                    beneficiaries.ForEach(x => _context.Entry(x).State = EntityState.Deleted);
                    _context.SaveChanges();

                    _context.Entry(contract.Client).State = EntityState.Modified;
                    _context.Entry(contract).State = EntityState.Modified;
                    _context.ContractBeneficiaries.AddRange(contract.ContractBeneficiaries);
                    return _context.SaveChanges();
                });
        }

        public bool CancelContract(int id)
        {
            return new ExceptionLogData<bool>(_context).Log(this.GetType().Name,
                () =>
                {
                    var success = false;
                    var contract = _context.Contract.Find(id);
                    if (contract != null)
                    {
                        contract.Status = ContractStatus.Canceled.ToString();
                        _context.Entry(contract).State = EntityState.Modified;
                        _context.SaveChanges();
                        success = true;
                    }

                    return success;
                });
        }
    }
}
