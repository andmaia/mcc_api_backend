using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.models;
using Infrastructure.Models;
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
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.CNPJ)
                .HasMaxLength(14);
            builder.HasMany(c => c.Employees).WithOne(e => e.Company);
            builder.HasMany(c => c.PaymentForms).WithOne(pf => pf.Company);
            builder.HasMany(c => c.Expenses).WithOne(e => e.Company);
            builder.HasMany(c => c.Orders).WithOne(o => o.Company);
            builder.HasMany(c => c.Comissions).WithOne(c => c.Company);

            builder.HasOne<ApplicationUser>() // Não mapeamos a propriedade User
           .WithMany() // Não há coleção em ApplicationUser
           .HasForeignKey(e => e.UserId) // Usa UserID como chave estrangeira
           .IsRequired(true); // Define se o relacionamento é opcional

        }

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.CPF)
                .HasMaxLength(11);
            builder.Property(e => e.UrlPerfil)
                .HasMaxLength(255);
            builder.Property(e => e.IsActive)
                .IsRequired();
            builder.HasMany(e => e.Expenses).WithOne(exp => exp.Employee);
            builder.HasMany(e => e.Orders).WithOne(o => o.Employee);
            builder.HasMany(e => e.Comissions).WithOne(c => c.Employee);
            builder.HasOne<ApplicationUser>() // Não mapeamos a propriedade User
          .WithMany() // Não há coleção em ApplicationUser
          .HasForeignKey(e => e.UserId) // Usa UserID como chave estrangeira
          .IsRequired(true); // Define se o relacionamento é opcional
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
            builder.Property(o => o.UpdatedDate);
            builder.Property(o => o.CompletionDate);
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
                .HasMaxLength(255);
            builder.Property(p => p.Discount);
            builder.Property(p => p.Amount)
                .IsRequired();
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


