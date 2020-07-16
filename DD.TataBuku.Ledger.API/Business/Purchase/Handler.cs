using DD.TataBuku.Ledger.API.Context;
using DD.TataBuku.Ledger.API.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace DD.TataBuku.Ledger.API.Business.Purchase
{
    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly ApplicationDbContext entities;
        public Handler(ApplicationDbContext accountingEntities)
        {
            entities = accountingEntities;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = entities.Database.BeginTransaction())
                {

                    var model = new GeneralLedger
                    {
                        Id = Guid.NewGuid(),
                        RowStatus = 0,
                        CreatedBy = request.Username,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = request.Username,
                        ModifiedDate = DateTime.Now,
                        GeneralLedgerNo = "GL0001",
                        GeneralLedgerName = "PEMBELIAN " + request.Reference,
                        GeneralLedgerDate = DateTime.Now,
                        RefDocumentNo = request.Reference,
                        PostingDate = null
                    };

                    entities.GeneralLedgers.Add(model);

                    entities.GeneralLedgerDetails.Add(new GeneralLedgerDetail
                    {
                        Id = Guid.NewGuid(),
                        RowStatus = 0,
                        CreatedBy = request.Username,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = request.Username,
                        ModifiedDate = DateTime.Now,
                        GeneralLedgerId = model.Id,
                        AccountCode = "0001",
                        AccountName = "CASH BANK",
                        AccountType = "C",
                        Amount = request.Ammount,
                        CurrencyCode = "IDR",
                        ExchangeRate = 1,
                        ExchangeAmount = request.Ammount,
                    });

                    entities.GeneralLedgerDetails.Add(new GeneralLedgerDetail
                    {
                        Id = Guid.NewGuid(),
                        RowStatus = 0,
                        CreatedBy = request.Username,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = request.Username,
                        ModifiedDate = DateTime.Now,
                        GeneralLedgerId = model.Id,
                        AccountCode = "0004",
                        AccountName = "PEMBELIAN BARANG",
                        AccountType = "D",
                        Amount = request.Ammount,
                        CurrencyCode = "IDR",
                        ExchangeRate = 1,
                        ExchangeAmount = request.Ammount,
                    });

                    await entities.SaveChangesAsync();
                    scope.Commit();

                    return new Response
                    {
                        msg = "Berhasil Disimpan",
                        success = true,
                        severity = 0,
                        result = true,
                    };
                }
            }
            catch(Exception ex)
            {
                return new Response
                {
                    msg = ex.Message,
                    success = false,
                    severity = 1,
                    result = false,
                };
            }
        }
    }
}
