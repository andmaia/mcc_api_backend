using Application.services.PaymentForm;
using AutoMapper;
using Common.requests.PaymentForm;
using Common.Responses.PaymentForm;
using Common.Responses.wrappers;
using Domain.models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.paymentForm
{
    internal class PaymentFormService : IPaymentFormService
    {

        private ApplicationDbContext _context;
        private IMapper _mapper;

        public PaymentFormService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> CreatePaymentForm(CreatePaymentFormRequest request)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == request.CompanyId);
            if (company == null)
                return ResponseWrapper.Fail("Company not found.");

            var paymentForm = _mapper.Map<PaymentForm>(request);
            paymentForm.Id = Guid.NewGuid().ToString();
            paymentForm.IsActive = true;
            paymentForm.CreationDate = DateTime.Now;
            paymentForm.UpdatedDate = DateTime.MinValue;
            paymentForm.CompletionDate = DateTime.MinValue;
            paymentForm.CompanyId = company.Id;

            _context.PaymentForms.Add(paymentForm);
            var result  = await _context.SaveChangesAsync();
            return result > 0
               ? await ResponseWrapper<string>.SuccessAsync(paymentForm.Id)
               : await ResponseWrapper.FailAsync("Failed to register paymentForm.");
           
        }


        public async Task<IResponseWrapper> DisablePaymentForm(string id, string companyId)
        {
            var paymentForm = await _context.PaymentForms.FirstOrDefaultAsync(pf => pf.Id == id && pf.CompanyId == companyId);
            if (paymentForm == null)
                return ResponseWrapper.Fail("Payment form not found or does not belong to the specified company.");

            if (!paymentForm.IsActive)
                return ResponseWrapper.Fail("Payment form is already disabled.");

            paymentForm.IsActive = false;
            paymentForm.CompletionDate = DateTime.Now;

            _context.PaymentForms.Update(paymentForm);
            var result = await _context.SaveChangesAsync();
            return result > 0
              ? await ResponseWrapper.SuccessAsync()
              : await ResponseWrapper.FailAsync("Failed to disable paymentForm.");

        }


        public async Task<IResponseWrapper> EnablePaymentForm(string id, string companyId)
        {
            var paymentForm = await _context.PaymentForms.FirstOrDefaultAsync(pf => pf.Id == id && pf.CompanyId == companyId);
            if (paymentForm == null)
                return ResponseWrapper.Fail("Payment form not found or does not belong to the specified company.");

            if (paymentForm.IsActive)
                return ResponseWrapper.Fail("Payment form is already enabled.");

            paymentForm.IsActive = true;
            paymentForm.CompletionDate = DateTime.MinValue;

            _context.PaymentForms.Update(paymentForm);
            var result = await _context.SaveChangesAsync();
            return result > 0
              ? await ResponseWrapper.SuccessAsync()
              : await ResponseWrapper.FailAsync("Failed to enable paymentForm.");
        }


        public async Task<IResponseWrapper> GetAllPaymentFormsFromCompanyId(string id)
        {
            var paymentForms = await _context.PaymentForms
                .AsNoTracking()
                .Where(pf => pf.CompanyId == id)
                .ToListAsync();

            if (!paymentForms.Any())
                return ResponseWrapper.Fail("No payment forms found for the given company.");

            var response = _mapper.Map<List<PaymentFormResponse>>(paymentForms);
            return ResponseWrapper<List<PaymentFormResponse>>.Success(response);

        }

        public async Task<IResponseWrapper> GetAllEnablePaymentFormsFromCompanyId(string id)
        {
            var paymentForms = await _context.PaymentForms
                .AsNoTracking()
                .Where(pf => pf.CompanyId == id && pf.IsActive)
                .ToListAsync();

            if (!paymentForms.Any())
                return ResponseWrapper.Fail("No active payment forms found for the given company.");

            var response = _mapper.Map<List<PaymentFormResponse>>(paymentForms);
            return ResponseWrapper<List<PaymentFormResponse>>.Success(response);
        }

        public async Task<IResponseWrapper> GetAllDisablePaymentFormsFromCompanyId(string id)
        {
            var paymentForms = await _context.PaymentForms
                .AsNoTracking()
                .Where(pf => pf.CompanyId == id && !pf.IsActive)
                .ToListAsync();

            if (!paymentForms.Any())
                return ResponseWrapper.Fail("No inactive payment forms found for the given company.");

            var response = _mapper.Map<List<PaymentFormResponse>>(paymentForms);
            return ResponseWrapper<List<PaymentFormResponse>>.Success(response);
        }


        public async Task<IResponseWrapper> GetAPaymentFormById(string id)
        {
            var paymentForm = await _context.PaymentForms
                .AsNoTracking()
                .FirstOrDefaultAsync(pf => pf.Id == id);

            if (paymentForm == null)
                return ResponseWrapper.Fail("Payment form not found.");

            var response = _mapper.Map<PaymentFormResponse>(paymentForm);
            return ResponseWrapper<PaymentFormResponse>.Success(response);
        }

        public async Task<IResponseWrapper> UpdatePaymentForm(UpdatePaymentFormRequest request)
        {
            var paymentForm = await _context.PaymentForms.FirstOrDefaultAsync(pf => pf.Id == request.PaymentFormId && pf.CompanyId == request.CompanyId);
            if (paymentForm == null)
                return ResponseWrapper.Fail("Payment form not found or does not belong to the specified company.");

            _mapper.Map(request, paymentForm);
            paymentForm.UpdatedDate = DateTime.Now;

            _context.PaymentForms.Update(paymentForm);
            var result = await _context.SaveChangesAsync();
            var responsePaymentForm = _mapper.Map<PaymentFormResponse>(paymentForm);

            return result > 0
              ? await ResponseWrapper<PaymentFormResponse>.SuccessAsync(responsePaymentForm)
              : await ResponseWrapper.FailAsync("Failed to update paymentForm.");
        }

    }
}
