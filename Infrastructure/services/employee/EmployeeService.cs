using Application.services.Employee;
using AutoMapper;
using Common.requests.Employee;
using Common.Responses.Employee;
using Common.Responses.identity;
using Common.Responses.wrappers;
using Domain.enums;
using Domain.models;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.employee
{
    public class EmployeeService:IEmployeService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public EmployeeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> DisableEmployeeById(string Employeeid, string companyId)
        {
            var employee = await _context.Employees
                                  .Include(e => e.User)
                                  .FirstOrDefaultAsync(e => e.Id == Employeeid && e.CompanyId == companyId);
            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Fail to disable employee, because company does not have this employee.");
            }

            if (!employee.IsActive)
            {
                return await ResponseWrapper.FailAsync("Fail to disable employee, because employee alrady disabled");

            }

            if (employee.User is null)
            {
                return await ResponseWrapper.FailAsync("Fail to disable employee, because does'nt have a  user.");
            }

            employee.IsActive = false;
            employee.FinishedDate = DateTime.Now;
            employee.User.IsActive = false;
            _context.Employees.Update(employee);
            var result = await _context.SaveChangesAsync();


            if (result > 0)
            {
                return await ResponseWrapper<string>.SuccessAsync();
            }

            return await ResponseWrapper.FailAsync("Fail to disable employee.");

        }

        public async Task<IResponseWrapper> EnableEmployeeById(string Employeeid, string companyId)
        {
            var employee = await _context.Employees
                                 .Include(e => e.User)
                                 .FirstOrDefaultAsync(e => e.Id == Employeeid && e.CompanyId == companyId);



            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Fail to enable employee, because company does not have this employee.");
            }

            if (employee.IsActive)
            {
                return await ResponseWrapper.FailAsync("Fail to enable employee, because employee alrady enabled");

            }
   
            if (employee.User is null)
            {
                return await ResponseWrapper.FailAsync("Fail to enable employee, because does'nt have a  user.");
            }


            employee.IsActive = true;
            employee.FinishedDate = DateTime.MinValue;
            employee.User.IsActive = true;
            _context.Employees.Update(employee);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return await ResponseWrapper<string>.SuccessAsync();
            }

            return await ResponseWrapper.FailAsync("Fail to enable employee.");
        }

        public async Task<IResponseWrapper> FinishRegisterEmployee(FinishRegisterEmployee request)
        {
            var employee = await _context.Employees
          .Include(e => e.User)
          .FirstOrDefaultAsync(e => e.UserId == request.UserId);

            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Employee not pre-registered with the provided UserId.");
            }

            if (employee.RegistrationFinish)
            {
                return await ResponseWrapper.FailAsync("Employee has already completed registration.");
            }

            if (employee.User is null)
            {
                return await ResponseWrapper.FailAsync("Employee is not associated with a valid user.");
            }

            employee.Name = request.Name;
            employee.CPF = request.CPF;
            employee.RegistrationFinish = true;

             _context.Employees.Update(employee);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return await ResponseWrapper<string>.SuccessAsync();
            }

            return await ResponseWrapper.FailAsync("Fail to finish registration employee.");

        }

        public async Task<IResponseWrapper> GetAllDisableEmployeesFromCompany(string companyId)
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Include(e => e.User) // Inclui os dados do User no carregamento
                .Where(e => e.CompanyId == companyId && !e.IsActive)
                .ToListAsync();

            if (!employees.Any())
            {
                return await ResponseWrapper.FailAsync("No disabled employees found for the company.");
            }

            var response = employees.Select(employee =>
            {
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                employeeResponse.UserResponse = _mapper.Map<UserResponse>(employee.User);
                return employeeResponse;
            }).ToList();

            return await ResponseWrapper<List<EmployeeResponse>>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> GetAllEmployeesFromCompany(string companyId)
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Include(e => e.User) // Inclui os dados do User no carregamento
                .Where(e => e.CompanyId == companyId)
                .ToListAsync();

            if (!employees.Any())
            {
                return await ResponseWrapper.FailAsync("No employees found for the company.");
            }

            var response = employees.Select(employee =>
            {
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                employeeResponse.UserResponse = _mapper.Map<UserResponse>(employee.User);
                return employeeResponse;
            }).ToList();

            return await ResponseWrapper<List<EmployeeResponse>>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> GetAllEnableEmployeesFromCompany(string companyId)
        {
            var employees = await _context.Employees
                .AsNoTracking()
                .Include(e => e.User) // Inclui os dados do User no carregamento
                .Where(e => e.CompanyId == companyId && e.IsActive)
                .ToListAsync();

            if (!employees.Any())
            {
                return await ResponseWrapper.FailAsync("No active employees found for the company.");
            }

            var response = employees.Select(employee =>
            {
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                employeeResponse.UserResponse = _mapper.Map<UserResponse>(employee.User);
                return employeeResponse;
            }).ToList();

            return await ResponseWrapper<List<EmployeeResponse>>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> GetEmployeeByEmail(string email, string companyId)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .Include(e => e.User) // Inclui os dados do User no carregamento
                .FirstOrDefaultAsync(e => e.User.Email == email && e.CompanyId == companyId);

            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Employee with the specified email not found.");
            }

            var response = _mapper.Map<EmployeeResponse>(employee);
            response.UserResponse = _mapper.Map<UserResponse>(employee.User);

            return await ResponseWrapper<EmployeeResponse>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> GetEmployeeById(string id, string companyId)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .Include(e => e.User) // Inclui os dados do User no carregamento
                .FirstOrDefaultAsync(e => e.Id == id && e.CompanyId == companyId);

            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Employee with the specified ID not found.");
            }

            var response = _mapper.Map<EmployeeResponse>(employee);
            response.UserResponse = _mapper.Map<UserResponse>(employee.User);

            return await ResponseWrapper<EmployeeResponse>.SuccessAsync(response);
        }

        public async Task<IResponseWrapper> GetEmployeeByUserId(string userId, string companyId)
        {
            var employee = await _context.Employees
                .AsNoTracking()
                .Include(e => e.User) // Inclui os dados do User no carregamento
                .FirstOrDefaultAsync(e => e.UserId == userId && e.CompanyId == companyId);

            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Employee with the specified UserId not found.");
            }

            var response = _mapper.Map<EmployeeResponse>(employee);
            response.UserResponse = _mapper.Map<UserResponse>(employee.User);

            return await ResponseWrapper<EmployeeResponse>.SuccessAsync(response);
        }



        public async Task<IResponseWrapper> RegisterEmployee(EmployeeRegisterRequest request)
        {
            if (!await _context.Users.AnyAsync(u => u.Id == request.UserId))
            {
                return await ResponseWrapper.FailAsync("UserId does not exist.");
            }

            if (await _context.Employees.AnyAsync(e => e.UserId == request.UserId))
            {
                return await ResponseWrapper.FailAsync("UserId is already associated with an Employee.");
            }

            if (await _context.Companies.AnyAsync(c => c.UserId == request.UserId))
            {
                return await ResponseWrapper.FailAsync("UserId is associated with a Company.");
            }

            var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == request.CompanyId);
            if (company is null)
            {
                return await ResponseWrapper.FailAsync("Company does not exist.");
            }

            if (!Enum.TryParse<Position>(request.Position, true, out var positionEnum))
            {
                return await ResponseWrapper.FailAsync("Invalid Position provided.");
            }

            var employee = new Employee
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                UrlPerfil="",
                CompanyId = company.Id,
                Position = positionEnum,
                ComissionPercentage = request.ComissionPercentage,
                IsActive = true,
                CreatedDate = DateTime.Now,
                RegistrationFinish = false,
                FinishedDate = DateTime.MinValue
                
            };

            await _context.Employees.AddAsync(employee);
            var result = await _context.SaveChangesAsync();

            return result > 0
                ? await ResponseWrapper<string>.SuccessAsync(employee.Id)
                : await ResponseWrapper.FailAsync("Failed to register Employee.");
        }

        public async Task<IResponseWrapper> UpdateEmployee(UpdateEmployee request)
        {

            var employee = await _context.Employees.Include(u => u.User)
                                 .FirstOrDefaultAsync(e => e.Id == request.IdEmployee && e.UserId == request.IdUser);
            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Fail to update employee, because UserId does`t have this employee.");
            };

            var employeeUpdated = _mapper.Map(request, employee);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var employeeToResponse = _mapper.Map<EmployeeResponse>(employeeUpdated);
                var userResponse = _mapper.Map<UserResponse>(employee.User);
                employeeToResponse.UserResponse = userResponse;
                return await ResponseWrapper<string>.SuccessAsync(employee.Id);
            }


            return await ResponseWrapper.FailAsync("Fail to update employee.");
        }

        public async Task<IResponseWrapper> UpdateEmployeeToCompany(UpdateEmployeeToCompany request)
        {

            var employee = await _context.Employees.Include(u=>u.User)
                                  .FirstOrDefaultAsync(e => e.Id == request.EmployeeId && e.UserId == request.UserId);
            if (employee is null)
            {
                return await ResponseWrapper.FailAsync("Fail to update employee, because UserId does not have this employee.");
            }

            if (employee.CompanyId != request.CompanyId)
            {
                return await ResponseWrapper.FailAsync("Fail to update employee, because the employee does not belong to this company.");
            }

            if (!Enum.TryParse<Position>(request.Position, true, out var positionEnum))
            {
                return await ResponseWrapper.FailAsync("Invalid Position provided.");
            }
            request.Position = positionEnum.ToString();

            _mapper.Map(request, employee);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                var employeeResponse = _mapper.Map<EmployeeResponse>(employee);
                employeeResponse.UserResponse = _mapper.Map<UserResponse>(employee.User);
                return await ResponseWrapper<EmployeeResponse>.SuccessAsync(employeeResponse);
            }

            return await ResponseWrapper.FailAsync("Fail to update employee.");
        }

    }
}
