using FluentMigrator;

namespace Swarm.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_GROUPS, "Create table to save the groups information")]
public class Version0000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("Groups")
            .WithColumn("Name").AsString(15).NotNullable()
            .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Group_User_Id", "Users", "Id"); //Relação FK - contém nome, tabela relacionada, coluna relacionada - 113
             //Add Image

        CreateTable("Products")
            .WithColumn("InternalCode").AsInt64().NotNullable()
            .WithColumn("Name").AsString(35).NotNullable()
            .WithColumn("Description").AsString().Nullable()
            .WithColumn("Value").AsDecimal(18, 2).NotNullable()
            .WithColumn("UnitType").AsInt32().NotNullable()
            .WithColumn("GroupId").AsInt64().NotNullable().ForeignKey("FK_Products_Group_Id", "Groups", "Id")
            .OnDelete(System.Data.Rule.Cascade); //Deleção em massa a partir do grupo
    }
}