using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Imoveis_Infraestrutura.Entities
{
    public partial class LOCADORA_IMOVEISContext : DbContext
    {
        public LOCADORA_IMOVEISContext() 
        {
        }

        public LOCADORA_IMOVEISContext(DbContextOptions<LOCADORA_IMOVEISContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Imoveis> Imoveis { get; set; }

    }
  
}
