using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChip.Migrations
{
    public partial class Versao4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Categoria_IdCategoria",
                table: "Cliente");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_IdCategoria",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "IdProduto",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "IdCategoria",
                table: "Cliente",
                newName: "IdCliente");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Credito",
                table: "Cliente",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Bairro",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_CEP",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Cidade",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Complemento",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Estado",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Logradouro",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Numero",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco_Referencia",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "IdCliente");

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Preco = table.Column<double>(type: "REAL", nullable: false),
                    IdCategoria = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.IdProduto);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_IdCategoria",
                table: "Produto",
                column: "IdCategoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Credito",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_Bairro",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_CEP",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_Cidade",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_Complemento",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_Estado",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_Logradouro",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_Numero",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Endereco_Referencia",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "IdCliente",
                table: "Cliente",
                newName: "IdCategoria");

            migrationBuilder.AddColumn<int>(
                name: "IdProduto",
                table: "Cliente",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<double>(
                name: "Preco",
                table: "Cliente",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "IdProduto");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Cpf = table.Column<string>(type: "TEXT", nullable: true),
                    Credito = table.Column<double>(type: "REAL", nullable: true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_IdCategoria",
                table: "Cliente",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Categoria_IdCategoria",
                table: "Cliente",
                column: "IdCategoria",
                principalTable: "Categoria",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
