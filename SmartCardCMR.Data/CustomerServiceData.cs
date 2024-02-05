using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmartCardCRM.Data
{
    public class CustomerServiceData : BaseData
    {
        public CustomerServiceData(SmartCardCRMContext context)
        {
            _context = context;
        }

        public List<CustomerServiceObservationsDTO> GetContractCustomerServiceObservations(int contractId)
        {
            return new ExceptionLogData<List<CustomerServiceObservationsDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    return new Mapper(MapperConfig)
                    .Map<List<CustomerServiceObservationsDTO>>(_context.CustomerServiceObservations
                        .Include("CustomerServiceObservationFiles")
                        .Where(x => x.ContractId == contractId).OrderByDescending(x => x.ObservationDate).ToList());
                });
        }

        public CustomerServiceObservationFilesDTO GetFileById(int id)
        {
            return new ExceptionLogData<CustomerServiceObservationFilesDTO>(_context).Log(this.GetType().Name,
                () =>
                {
                    return new Mapper(MapperConfig).Map<CustomerServiceObservationFilesDTO>(_context.CustomerServiceObservationFiles.Find(id));
                });
        }

        public void NewCustomerServiceObservation(CustomerServiceObservationsDTO customerServiceDTO)
        {
            new ExceptionLogData<int>(_context).LogWithTransaction(this.GetType().Name,
                () =>
                {
                    var contract = _context.Contract.Find(customerServiceDTO.ContractId);
                    var directoryPath = @"\Documents\CustomerServiceFiles\" + contract.ContractNumber;
                    var fullDirectoryPath = Environment.CurrentDirectory + directoryPath;
                    if (!Directory.Exists(fullDirectoryPath))
                    {
                        Directory.CreateDirectory(fullDirectoryPath);
                    }

                    customerServiceDTO.CustomerServiceObservationFiles.ForEach(x =>
                    {
                        var guid = Guid.NewGuid();
                        var filePath = string.Format(@"{0}\{1}", fullDirectoryPath, guid);
                        using (Stream stream = new FileStream(filePath, FileMode.Create))
                        {
                            byte[] fileBytes = Convert.FromBase64String(x.FileBase64.Split(",")[1]);
                            stream.Write(fileBytes, 0, fileBytes.Length);
                            x.FilePath = string.Format(@"{0}\{1}", directoryPath, guid);
                        }
                    });
                    var customerService = new Mapper(MapperConfig).Map<CustomerServiceObservations>(customerServiceDTO);
                    _context.CustomerServiceObservations.Add(customerService);
                    _context.CustomerServiceObservationFiles.AddRange(customerService.CustomerServiceObservationFiles);
                    return _context.SaveChanges();
                });
        }

        public void UpdateObservation(int id, CustomerServiceObservationsDTO customerServiceDTO)
        {
            new ExceptionLogData<int>(_context).Log(this.GetType().Name,
                () =>
                {
                    var customerService = _context.CustomerServiceObservations.Find(id);
                    customerService.Observations = customerServiceDTO.Observations;
                    _context.Entry(customerService).State = EntityState.Modified;
                    return _context.SaveChanges();
                });
        }
    }
}
