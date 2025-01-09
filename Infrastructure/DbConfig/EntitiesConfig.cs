using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbConfig
{

    public class ModelBuilderConfiguration : IEntityTypeConfiguration<Company>, 
                                             IEntityTypeConfiguration<Employee>, 
                                             IEntityTypeConfiguration<Expense>, 
                                             IEntityTypeConfiguration<PaymentForm>, 
                                             IEntityTypeConfiguration<Order>, 
                                             IEntityTypeConfiguration<Payment>, 
                                             IEntityTypeConfiguration<Comission>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);

            // Configuração das propriedades
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CNPJ)
                .HasMaxLength(14);

            // Relacionamentos com outras entidades
            builder.HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId) // Especifica a chave estrangeira em Employee
                .OnDelete(DeleteBehavior.Cascade); // Caso a Company seja excluída, os Employees também serão excluídos

            builder.HasMany(c => c.PaymentForms)
                .WithOne(pf => pf.Company)
                .HasForeignKey(pf => pf.CompanyId) // Chave estrangeira em PaymentForm
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Expenses)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId) // Chave estrangeira em Expense
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Company)
                .HasForeignKey(o => o.CompanyId) // Chave estrangeira em Order
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Comissions)
                .WithOne(c => c.Company)
                .HasForeignKey(c => c.CompanyId) // Chave estrangeira em Comission
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.branches)
                .WithOne(b => b.company) // A chave estrangeira será configurada automaticamente
                .HasForeignKey(b => b.companyId) // Chave estrangeira em Branch
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento 1:1 com ApplicationUser
            builder.HasOne(c => c.User) // Relacionamento com ApplicationUser
                .WithMany() // Não há coleção de Companies no ApplicationUser
                .HasForeignKey(c => c.UserId) // A chave estrangeira em Company para ApplicationUser
                .IsRequired(true); // Define que o relacionamento é obrigatório para Company
        }



        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            // Configuração de propriedades
            builder.Property(e => e.Name)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.CPF)
                .HasMaxLength(11)
                .IsRequired(false);
            builder.Property(e => e.UrlPerfil)
                .HasMaxLength(255);

            builder.Property(e => e.IsActive)
                .IsRequired();

            builder.Property(e => e.RegistrationFinish)
                .IsRequired();

            builder.Property(e => e.CreatedDate)
                .IsRequired();

             

            // Relacionamentos com outras entidades
            builder.HasMany(e => e.Expenses)
                .WithOne(exp => exp.Employee)
                .HasForeignKey(exp => exp.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Caso o Employee seja excluído, os Expenses também serão excluídos

            builder.HasMany(e => e.Orders)
                .WithOne(o => o.Employee)
                .HasForeignKey(o => o.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Caso o Employee seja excluído, os Orders também serão excluídos

            builder.HasMany(e => e.Comissions)
                .WithOne(c => c.Employee)
                .HasForeignKey(c => c.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade); // Caso o Employee seja excluído, as Commissions também serão excluídas

            // Relacionamento 1:1 com ApplicationUser (UserId)
            builder.HasOne(e => e.User) // Relacionamento com ApplicationUser
                .WithMany() // Não há coleção de Employees no ApplicationUser
                .HasForeignKey(e => e.UserId) // A chave estrangeira em Employee
                .IsRequired(true); // O User é obrigatório para o Employee

            // Relacionamento 1:N com Company
            builder.HasOne(e => e.Company) // Relacionamento com Company
                .WithMany(c => c.Employees) // A Company tem muitos Employees
                .HasForeignKey(e => e.CompanyId) // Chave estrangeira em Employee para Company
                .IsRequired(true); // O Company é obrigatório para o Employee
        }

        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.IsActive)
                .IsRequired();
            builder.Property(b => b.CreateDate)
                .IsRequired();
            builder.Property(b => b.FinishDate)
                .IsRequired(false); // Opcional

            builder.HasOne(b => b.company)
                .WithMany(c => c.branches)
                .HasForeignKey(b => b.companyId)
                .IsRequired(); //  obrigatório
        }


        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(exp => exp.Id);
            builder.Property(exp => exp.Value)
                .IsRequired();
            builder.Property(exp => exp.CreationDate)
                .IsRequired();
            builder.Property(exp => exp.UpdatedDate);
            builder.Property(exp => exp.CompletionDate);
            builder.Property(exp => exp.Description)
                .HasMaxLength(255);
            builder.Property(exp => exp.Url)
                .HasMaxLength(255);
            builder.Property(exp => exp.StatusPayment)
                .IsRequired();
            builder.Property(exp => exp.IsActive)
                .IsRequired();
        }

        public void Configure(EntityTypeBuilder<PaymentForm> builder)
        {
            builder.HasKey(pf => pf.Id);
            builder.Property(pf => pf.Tax)
                .IsRequired();
            builder.Property(pf => pf.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(pf => pf.CreationDate)
                .IsRequired();
            builder.Property(pf => pf.UpdatedDate);
            builder.Property(pf => pf.CompletionDate);
            builder.Property(pf => pf.IsActive)
                .IsRequired();
        }

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CustomerName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(o => o.IsActive)
                .IsRequired();
            builder.Property(o => o.CreationDate)
                .IsRequired();
            builder.Property(o => o.UpdatedDate)
                            .IsRequired(false);

            builder.Property(o => o.CompletionDate)
                                            .IsRequired(false);

            builder.Property(o => o.PaymentOrderStatus)
                .IsRequired();
            builder.Property(o => o.CommissionOrderStatus)
                .IsRequired();
            builder.Property(o => o.TotalValue)
                .IsRequired();
            builder.Property(o => o.CommissionPercentage)
                .IsRequired();
            builder.Property(o => o.TotalDiscount)
                .IsRequired();
            builder.Property(o => o.ComissionId)
               .IsRequired(false);

        }

        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CreationDate)
                .IsRequired();

            builder.Property(p => p.PaymentDate);
            builder.Property(p => p.UpdatedDate);
            builder.Property(p => p.CompletionDate);

            builder.Property(p => p.IsActive)
                .IsRequired();

            builder.Property(p => p.Tax)
                .IsRequired();

            builder.Property(p => p.Url)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(p => p.Discount)
                .HasColumnType("decimal(18,2)"); 

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Value)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }


        public void Configure(EntityTypeBuilder<Comission> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.CommissionStartDate)
                .IsRequired();
            builder.Property(c => c.CommissionEndDate)
                .IsRequired();
            builder.Property(c => c.CreationDate)
                .IsRequired();
            builder.Property(c => c.PaymentDate);
            builder.Property(c => c.FinishedDate);
            builder.Property(c => c.UpdatedDate);
            builder.Property(c => c.IsActive)
                .IsRequired();
            builder.Property(c => c.PaymentStatus)
                .IsRequired();
            builder.Property(c => c.ApplyDiscount)
                .IsRequired();
            builder.Property(c => c.TotalValue)
                .IsRequired();
            builder.Property(c => c.TotalFees)
                .IsRequired();
            builder.Property(c => c.TotalDiscounts)
                .IsRequired();
            builder.Property(c => c.Percentage)
                .IsRequired();
            builder.Property(c => c.TotalCommission)
                .IsRequired();
            builder.Property(c => c.Url)
                .HasMaxLength(255);


        }
    }
}


