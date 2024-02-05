using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartCardCRM.Data.Entities;
using SmartCardCRM.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartCardCRM.Data
{
    public class BookingData : BaseData
    {
        public BookingData(SmartCardCRMContext context)
        {
            _context = context;
        }

        public List<BookingObservationsDTO> GetContractBookingObservations(int contractId)
        {
            return new ExceptionLogData<List<BookingObservationsDTO>>(_context).Log(this.GetType().Name,
                () =>
                {
                    return new Mapper(MapperConfig)
                    .Map<List<BookingObservationsDTO>>(_context.BookingObservations
                        .Include("BookingObservationFiles")
                        .Where(x => x.ContractId == contractId).OrderByDescending(x => x.ObservationDate).ToList());
                });
        }

        public BookingObservationFilesDTO GetFileById(int id)
        {
            return new ExceptionLogData<BookingObservationFilesDTO>(_context).Log(this.GetType().Name,
                () =>
                {
                    return new Mapper(MapperConfig).Map<BookingObservationFilesDTO>(_context.BookingObservationFiles.Find(id));
                });
        }

        public void NewBookingObservation(BookingObservationsDTO bookingDTO)
        {
            new ExceptionLogData<int>(_context).LogWithTransaction(this.GetType().Name,
                () =>
                {
                    var contract = _context.Contract.Find(bookingDTO.ContractId);
                    var directoryPath = @"\Documents\BookingFiles\" + contract.ContractNumber;
                    var fullDirectoryPath = Environment.CurrentDirectory + directoryPath;
                    if (!Directory.Exists(fullDirectoryPath))
                    {
                        Directory.CreateDirectory(fullDirectoryPath);
                    }

                    bookingDTO.BookingObservationFiles.ForEach(x =>
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
                    var booking = new Mapper(MapperConfig).Map<BookingObservations>(bookingDTO);
                    _context.BookingObservations.Add(booking);
                    _context.BookingObservationFiles.AddRange(booking.BookingObservationFiles);
                    return _context.SaveChanges();
                });
        }

        public void UpdateObservation(int id, BookingObservationsDTO bookingDTO)
        {
            new ExceptionLogData<int>(_context).Log(this.GetType().Name,
                () =>
                {
                    var booking = _context.BookingObservations.Find(id);
                    booking.Observations = bookingDTO.Observations;
                    _context.Entry(booking).State = EntityState.Modified;
                    return _context.SaveChanges();
                });
        }
    }
}
