﻿using E_Commerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Config
{
	internal class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
	{
		public void Configure(EntityTypeBuilder<ProductCategory> builder)
		{
			builder.Property(c => c.Name).IsRequired()
				.HasMaxLength(100);
		}
	}
}
