using Application.services.Branch;
using AutoMapper;
using Common.requests.branch;
using Common.Responses.branch;
using Common.Responses.Employee;
using Common.Responses.identity;
using Common.Responses.wrappers;
using Domain.models;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.branch
{
    public class BranchService : IBranchService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BranchService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> CreateBranch(CreateBranchRequest request)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == request.companyId);
            if (company == null)
            {
                return ResponseWrapper.Fail("Company not found.");
            }

            var branch = new Branch
            {
                Id = Guid.NewGuid().ToString(),
                companyId = company.Id,
                CreateDate = DateTime.Now,
                FinishDate = DateTime.MinValue,
                IsActive = true,
                Name = request.Name,
                company = company
            };
            await _context.Branchs.AddAsync(branch);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? await ResponseWrapper<string>.SuccessAsync(branch.Id)
                : await ResponseWrapper.FailAsync("Failed to register Branch.");

        }

        public async Task<IResponseWrapper> DisableBranch(string id)
        {
            var branch = await _context.Branchs.FirstOrDefaultAsync(c => c.Id == id);
            if (branch == null)
            {
                return ResponseWrapper.Fail("Branch not found.");
            }

            if (!branch.IsActive)
            {
                return ResponseWrapper.Fail("Branch already disabled.");
            }

            branch.IsActive = false;
            branch.FinishDate = DateTime.Now;

            _context.Branchs.Update(branch);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? await ResponseWrapper<string>.SuccessAsync(branch.Id)
                : await ResponseWrapper.FailAsync("Failed to disable Branch.");
        }

        public async Task<IResponseWrapper> EnableBranch(string id)
        {
            var branch = await _context.Branchs.FirstOrDefaultAsync(c => c.Id == id);
            if (branch == null)
            {
                return ResponseWrapper.Fail("Branch not found.");
            }

            if (branch.IsActive)
            {
                return ResponseWrapper.Fail("Branch already enable.");
            }

            branch.IsActive = true;
            branch.FinishDate = DateTime.MinValue;

            _context.Branchs.Update(branch);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? await ResponseWrapper<string>.SuccessAsync(branch.Id)
                : await ResponseWrapper.FailAsync("Failed to enable Branch.");
        }

        public async Task<IResponseWrapper> GetBranchById(string companyId)
        {
            var branch = await _context.Branchs
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == companyId);

            if (branch is null)
            {
                return await ResponseWrapper.FailAsync("Branch with the specified ID not found.");
            }

            var response = _mapper.Map<ResponseBranch>(branch);

            return await ResponseWrapper<ResponseBranch>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> GetBranchsByCompany(string id)
        {
            var branches = await _context.Branchs
                .AsNoTracking()
                .Where(e => e.companyId == id && e.IsActive)
                .ToListAsync();

            if (!branches.Any())
            {
                return await ResponseWrapper.FailAsync("No active branchs found for the company.");
            }

            var response = branches.Select(branch =>
            {
                var employeeResponse = _mapper.Map<ResponseBranch>(branch);
                return employeeResponse;
            }).ToList();

            return await ResponseWrapper<List<ResponseBranch>>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> GetOrdersByBranch(string id)
        {
            var branches = await _context.Orders
                .AsNoTracking()
                .Where(e => e.branchId == id && e.IsActive)
                .ToListAsync();

            if (!branches.Any())
            {
                return await ResponseWrapper.FailAsync("No active branchs found for the company.");
            }

            var response = branches.Select(branch =>
            {
                var employeeResponse = _mapper.Map<ResponseBranch>(branch);
                return employeeResponse;
            }).ToList();

            return await ResponseWrapper<List<ResponseBranch>>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> UpdateBranch(UpdateBranchRequest request)
        {
            var branch = await _context.Branchs.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (branch == null)
            {
                return ResponseWrapper.Fail("Branch not found.");
            }

            branch.Name = request.Name;
             _context.Branchs.Update(branch);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? await ResponseWrapper<string>.SuccessAsync(branch.Id)
                : await ResponseWrapper.FailAsync("Failed to update Branch.");

        }
    }
}
