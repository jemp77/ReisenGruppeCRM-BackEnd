using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Enumerables;
using SmartCardCRM.Model.Models;
using SmartCardCRM.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCardCRM.Data
{
    public class ClientData : BaseData
    {
        public ClientData(SmartCardCRMContext context)
        {
            _context = context;
        }

        public ClientDTO GetClientById(int id)
        {
            return new ExceptionLogData<ClientDTO>(_context).Log(this.GetType().Name,
                () =>
                {
                    var client = _context.Client.Include("ClientDebitCreditCards").Where(x => x.Id == id).ToList().FirstOrDefault();
                    if (client != null)
                    {
                        _context.Entry(client).State = EntityState.Detached;
                    }

                    return new Mapper(MapperConfig).Map<ClientDTO>(client);
                });
        }

        public ContractDTO GetClientByContractNumber(string contractNumber)
        {
            return new ExceptionLogData<ContractDTO>(_context).Log(this.GetType().Name,
                () =>
                {
                    var contract = _context.Contract.Include("Client").Where(x => x.ContractNumber == contractNumber).ToList().FirstOrDefault();
                    if (contract != null)
                    {
                        _context.Entry(contract).State = EntityState.Detached;
                    }

                    return new Mapper(MapperConfig).Map<ContractDTO>(contract);
                });
        }

        public List<ClientDTO> GetClientByFilters(string filter)
        {
            return new ExceptionLogData<List<ClientDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    var clients = _context.Client.Include("Contract").Where(x =>
                        x.Id.ToString() == filter
                        || x.DocumentNumber.Contains(filter)
                        || x.Name.Contains(filter)
                        || x.LastName.Contains(filter)
                        || (x.Name + " " + x.LastName).Contains(filter)
                        || (x.LastName + " " + x.Name).Contains(filter)
                        || x.Email.Contains(filter)
                        || x.PhoneNumber.Contains(filter)
                    ).ToList();
                    clients.RemoveAll(x => x.Contract.Where(c => c.Status == ContractStatus.Generated.ToString() || c.Status == ContractStatus.Edited.ToString()).ToList().Count > 0);
                    return new Mapper(MapperConfig).Map<List<ClientDTO>>(clients);
                });
        }

        public List<ClientDTO> GetClientsByDateRange(DateTime startDate, DateTime endDate)
        {
            return new ExceptionLogData<List<ClientDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    var listClientDTO = new List<ClientDTO>();
                    var listClient = _context.Client.Include(x => x.ClientDebitCreditCards).Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
                    if (listClient != null)
                    {
                        listClientDTO = new Mapper(MapperConfig).Map<List<ClientDTO>>(listClient);
                        listClientDTO.ForEach(x =>
                        {
                            x.CreditCardsCount = x.ClientDebitCreditCards.Where(x => !x.CardType.Equals("Debito")).ToList().Count + x.CoOwnerDebitCreditCards.Where(x => !x.CardType.Equals("Debito")).ToList().Count;
                            x.CreditCardsFranchiseSum = string.Join(" - ", x.ClientDebitCreditCards.Where(x => !x.CardType.Equals("Debito")).ToList().Union(x.CoOwnerDebitCreditCards.Where(x => !x.CardType.Equals("Debito")).ToList()).Select(c => c.FranchiseName).ToArray());
                        });
                    }

                    return listClientDTO;
                });
        }

        public List<ClientDTO> GetClientsForDashboard(DateTime startDate, DateTime endDate)
        {
            return new ExceptionLogData<List<ClientDTO>>(_context).Log(this.GetType().Name,
                () => 
                {
                    var clients = _context.Client.Include("Contract").Where(x => x.Date >= startDate.Date && x.Date <= endDate.Date.AddDays(1).AddTicks(-1)).ToList();
                    return new Mapper(MapperConfig).Map<List<ClientDTO>>(clients);
                });
        }

        public int CreateClient(ClientDTO clientDTO)
        {
            return new ExceptionLogData<int>(_context).LogWithTransaction(this.GetType().Name,
                () =>
                {
                    clientDTO.ClientDebitCreditCards = clientDTO.ClientDebitCreditCards.Where(x => !string.IsNullOrEmpty(x.FranchiseName)).ToList();
                    clientDTO.CoOwnerDebitCreditCards = clientDTO.CoOwnerDebitCreditCards.Where(x => !string.IsNullOrEmpty(x.FranchiseName)).ToList();
                    clientDTO.ClientDebitCreditCards.ForEach(x => x.IsClientCard = true);
                    clientDTO.ClientDebitCreditCards.AddRange(clientDTO.CoOwnerDebitCreditCards);
                    clientDTO.Date = CustomDateTime.Now;
                    var client = new Mapper(MapperConfig).Map<Client>(clientDTO);
                    _context.Client.Add(client);
                    _context.SaveChanges();
                    return client.Id;
                });
        }

        public void UpdateClient(ClientDTO clientDTO)
        {
            new ExceptionLogData<int>(_context).LogWithTransaction(this.GetType().Name,
                () => 
                {
                    clientDTO.ClientDebitCreditCards = clientDTO.ClientDebitCreditCards.Where(x => !string.IsNullOrEmpty(x.FranchiseName)).ToList();
                    clientDTO.CoOwnerDebitCreditCards = clientDTO.CoOwnerDebitCreditCards.Where(x => !string.IsNullOrEmpty(x.FranchiseName)).ToList();
                    clientDTO.ClientDebitCreditCards.ForEach(x => x.IsClientCard = true);
                    clientDTO.ClientDebitCreditCards.AddRange(clientDTO.CoOwnerDebitCreditCards);

                    var clientDebitCreditCards = _context.ClientDebitCreditCards.Where(x => x.ClientId == clientDTO.Id).ToList();
                    clientDebitCreditCards.ForEach(x => _context.Entry(x).State = EntityState.Deleted);
                    _context.SaveChanges();

                    var client = new Mapper(MapperConfig).Map<Client>(clientDTO);
                    _context.Entry(client).State = EntityState.Modified;
                    _context.ClientDebitCreditCards.AddRange(client.ClientDebitCreditCards);
                    return _context.SaveChanges();
                });
        }

        public void UpdateClientPartialData(ClientDTO clientDTO)
        {
            new ExceptionLogData<int>(_context).Log(this.GetType().Name,
                () =>
                {
                    _context.Entry(new Mapper(MapperConfig).Map<Client>(clientDTO)).State = EntityState.Modified;
                    return _context.SaveChanges();
                });
        }
    }
}
