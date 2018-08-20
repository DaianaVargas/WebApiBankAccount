namespace WebApiBankAccount.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccountBalances",
                c => new
                    {
                        id = c.Guid(nullable: false),
                        Operation = c.String(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Creation = c.DateTime(nullable: false),
                        BankAccountID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountID, cascadeDelete: true)
                .Index(t => t.BankAccountID);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Number = c.Int(nullable: false),
                        Creation = c.DateTime(nullable: false),
                        Cpf = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BankAccountBalances", "BankAccountID", "dbo.BankAccounts");
            DropIndex("dbo.BankAccountBalances", new[] { "BankAccountID" });
            DropTable("dbo.BankAccounts");
            DropTable("dbo.BankAccountBalances");
        }
    }
}
