﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TicketSystem
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ticketEntities : DbContext
    {
        public ticketEntities()
            : base("name=ticketEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<admin> admin { get; set; }
        public virtual DbSet<Append_User> Append_User { get; set; }
        public virtual DbSet<city> city { get; set; }
        public virtual DbSet<order> order { get; set; }
        public virtual DbSet<price_info> price_info { get; set; }
        public virtual DbSet<user> user { get; set; }
        public virtual DbSet<vehicle> vehicle { get; set; }
    }
}