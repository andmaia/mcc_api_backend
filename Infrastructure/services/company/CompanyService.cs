using Application.services.Company;
using Application.services.identity;
using AutoMapper;
using Common.requests.company;
using Common.Responses.Company;
using Common.Responses.wrappers;
using Domain.models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.company
{
    public class CompanyService : ICompanyService
    {


        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> CreateCompany(CompanyRequest companyRequest)
        {
            var userCompanyId = _context.Users.FirstOrDefault(x => x.Id == companyRequest.UserId);
            if (userCompanyId is null)
            {
                return await ResponseWrapper.FailAsync("Fail to register company, because UserId Does't exists.");
            };

            var cnpjCompanyAlreadyExists = _context.Companies.FirstOrDefault(x => x.CNPJ == companyRequest.CNPJ);
            if (cnpjCompanyAlreadyExists is not null)
            {
                return await ResponseWrapper.FailAsync("Fail to register company, because CNPJ already exists.");
            };

            var company = _mapper.Map<Company>(companyRequest);
            company.Id = Guid.NewGuid().ToString(); 
            await _context.Companies.AddAsync(company);
            var result = await _context.SaveChangesAsync();

            if (result>0)
            {
                return await ResponseWrapper<string>.SuccessAsync(company.Id);
            }

            return await ResponseWrapper.FailAsync("Fail to create company.");
        }

        public async Task<IResponseWrapper> GetCompanyByEmployeeUd(string id)
        {
            var company = _context.Companies
      .FirstOrDefault(x => x.Employees.Any(e => e.Id == id));

            if (company is null)
            {
                return await ResponseWrapper.FailAsync("Fail to get company, because user Does't exists.");

            }

            var companyResponse = _mapper.Map<CompanyResponse>(company);

            return await ResponseWrapper<CompanyResponse>.SuccessAsync(companyResponse);
        }

        public async Task<IResponseWrapper> GetCompanyById(string companyId)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Id == companyId);
            if (company is null)
            {
                return await ResponseWrapper.FailAsync("Fail to get company, because company Does't exists.");

            }

            var companyResponse = _mapper.Map<CompanyResponse>(company);

            return await ResponseWrapper<CompanyResponse>.SuccessAsync(companyResponse);

        }

        public async Task<IResponseWrapper> GetCompanyByUserId(string id)
        {
            var company = _context.Companies.FirstOrDefault(x => x.UserId == id);
            if (company is null)
            {
                return await ResponseWrapper.FailAsync("Fail to get company, because user Does't exists.");

            }

            var companyResponse = _mapper.Map<CompanyResponse>(company);

            return await ResponseWrapper<CompanyResponse>.SuccessAsync(companyResponse);
        }

        public async Task<IResponseWrapper> UpdateCompany(CompanyUpdate companyUpdate)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Id == companyUpdate.Id);

            if (company is null)
            {
                return await ResponseWrapper.FailAsync("Fail to update company, because company Does't exists.");
            }

            if (company.CNPJ != companyUpdate.CNPJ)
            {
                var cnpjAlreadyExists = _context.Companies.FirstOrDefault(x => x.CNPJ == companyUpdate.CNPJ);

                if (cnpjAlreadyExists is not null)
                {
                    return await ResponseWrapper.FailAsync("Fail to update company, because cnpj to update already exists.");
                }
            }

            company.CNPJ = companyUpdate.CNPJ;
            company.Name = companyUpdate.Name;

             _context.Companies.Update(company);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return await ResponseWrapper.SuccessAsync();
            }

            return await ResponseWrapper.FailAsync("Fail to update company.");

        }
    }
}
